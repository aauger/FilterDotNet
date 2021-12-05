using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Common
{
    public class FastImage
    {
        public FastImageColor[,] Pixels { get; }
        public int Width { get; }
        public int Height { get; }

        public FastImage(Image a)
        {
            Pixels = new FastImageColor[a.Width, a.Height];
            Bitmap b = (Bitmap)a;

            Width = b.Width;
            Height = b.Height;

            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    Color c = b.GetPixel(x, y);
                    Pixels[x, y] = new FastImageColor(c.R, c.G, c.B);
                }
            }
        }

        public FastImage(FastImage c)
        {
            Pixels = new FastImageColor[c.Width, c.Height];

            Width = c.Width;
            Height = c.Height;

            for (int x = 0; x < c.Width; x++)
            {
                for (int y = 0; y < c.Height; y++)
                {
                    Pixels[x, y] = c.GetPixel(x, y);
                }
            }
        }

        public FastImage(int w, int h)
        {
            Pixels = new FastImageColor[w, h];
            Width = w;
            Height = h;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Pixels[x, y] = new FastImageColor(0, 0, 0, 0);
                }
            }
        }

        public FastImageColor GetPixel(int x, int y)
        {
            return Pixels[x, y];
        }

        public void SetPixel(int x, int y, FastImageColor c)
        {
            Pixels[x, y].Set(c.GetR(), c.GetG(), c.GetB(), c.GetA());
        }

        public void Clear()
        {
            Parallel.For(0, Width, x =>
            {
                Parallel.For(0, Height, y =>
                {
                    Pixels[x, y].Set(0, 0, 0, 0);
                });
            });
        }

        public void SetPixel(int x, int y, Color c)
        {
            Pixels[x, y] = FastImageColor.FromColor(c);
        }

        public Bitmap ToBitmap()
        {
            Bitmap ret = new Bitmap(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ret.SetPixel(x, y, Pixels[x, y].ToColor());
                }
            }

            return ret;
        }

        //Bresenham's algorithm. Probably does not belong here.
        public void DrawLine(int x, int y, int x2, int y2, FastImageColor c)
        {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                if (x >= 0 && x < Width && y >= 0 && y < Height)
                    SetPixel(x, y, c);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        //lots of magic here that ought to be fixed.
        public void WriteBitmap(string filename)
        {
            byte[] scrap;
            List<byte> data = new List<byte>();

            data.Add(0x42);
            data.Add(0x4D);
            int filesizeOffset = data.Count;
            data.AddRange(BitConverter.GetBytes(0xFFFFFFFF));
            data.Add(0x00);
            data.Add(0x00);
            data.Add(0x00);
            data.Add(0x00);
            int pixelInfoOffset = data.Count;
            data.AddRange(BitConverter.GetBytes(0));
            data.AddRange(BitConverter.GetBytes(40));
            data.AddRange(BitConverter.GetBytes(Width));
            data.AddRange(BitConverter.GetBytes(Height));
            data.AddRange(BitConverter.GetBytes((short)1));
            data.AddRange(BitConverter.GetBytes((short)24));
            data.AddRange(BitConverter.GetBytes(0));
            int rawPixelArraySizeOffset = data.Count;
            data.AddRange(BitConverter.GetBytes(0));
            data.AddRange(BitConverter.GetBytes(96));
            data.AddRange(BitConverter.GetBytes(96));
            data.AddRange(BitConverter.GetBytes(0));
            data.AddRange(BitConverter.GetBytes(0));
            scrap = BitConverter.GetBytes(data.Count);
            for (int i = pixelInfoOffset; i < pixelInfoOffset + 4; i++)
            {
                data[i] = scrap[i - pixelInfoOffset];
            }
            int sizeOfHeader = data.Count;
            for (int y = Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    FastImageColor c = GetPixel(x, y);
                    data.Add((byte)c.GetB());
                    data.Add((byte)c.GetG());
                    data.Add((byte)c.GetR());
                }
                //pad to mult of 4?
                while ((data.Count - sizeOfHeader) % 4 != 0)
                {
                    data.Add(0x0);
                }
            }
            scrap = BitConverter.GetBytes(data.Count);
            for (int i = filesizeOffset; i < filesizeOffset + 4; i++)
            {
                data[i] = scrap[i - filesizeOffset];
            }
            scrap = BitConverter.GetBytes(data.Count - sizeOfHeader);
            for (int i = rawPixelArraySizeOffset; i < rawPixelArraySizeOffset + 4; i++)
            {
                data[i] = scrap[rawPixelArraySizeOffset - i];
            }

            using (BinaryWriter bw = new BinaryWriter(new FileStream(filename, FileMode.CreateNew)))
            {
                bw.Write(data.ToArray());
            }
        }
    }
}
