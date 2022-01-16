using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastImageLibrary
{
    public class FastImageColor
    {
        int R, G, B, A;

        public FastImageColor(int R, int G, int B)
        {
            Debug.Assert(R >= 0 && R <= 255 &&
                            G >= 0 && G <= 255 &&
                            B >= 0 && B <= 255);
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = 255;
        }

        public FastImageColor(int R, int G, int B, int A)
        {
            Debug.Assert(R >= 0 && R <= 255 &&
                            G >= 0 && G <= 255 &&
                            B >= 0 && B <= 255 &&
                            A >= 0 && A <= 255);
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }

        public void Set(int R, int G, int B)
        {
            Debug.Assert(R >= 0 && R <= 255 &&
                            G >= 0 && G <= 255 &&
                            B >= 0 && B <= 255);
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = 255;
        }

        private float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * by + secondFloat * (1 - by);
        }

        public static float Map(float x, float olds, float olde, float news, float newe)
        {
            return (x - olds) / (olde - olds) * (newe - news) + news;
        }

        public static int RGBClamp(int x)
        {
            return Clamp(x, 255);
        }

        public static int Clamp(int x, int max)
        {
            if (x < 0)
                return 0;
            else if (x > max)
                return max;
            else
                return x;
        }

        public void Set(int R, int G, int B, int A)
        {
            Debug.Assert(R >= 0 && R <= 255 &&
                            G >= 0 && G <= 255 &&
                            B >= 0 && B <= 255 &&
                            A >= 0 && A <= 255);
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }

        public void SetR(int R)
        {
            Debug.Assert(R >= 0 && R <= 255);
            this.R = R;
        }

        public void SetG(int G)
        {
            Debug.Assert(G >= 0 && G <= 255);
            this.G = G;
        }

        public void SetB(int B)
        {
            Debug.Assert(B >= 0 && B <= 255);
            this.B = B;
        }

        public void SetA(int A)
        {
            Debug.Assert(A >= 0 && A <= 255);
            this.A = A;
        }

        public int GetR()
        {
            return R;
        }

        public int GetG()
        {
            return G;
        }

        public int GetB()
        {
            return B;
        }

        public int GetA()
        {
            return A;
        }

        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }

        public static FastImageColor FromColor(Color c)
        {
            return new FastImageColor(c.R, c.G, c.B, c.A);
        }

        public override bool Equals(object? obj)
        {
            FastImageColor? other = obj as FastImageColor;

            if (other == null)
                return false;

            return (R == other.R && B == other.B && G == other.G);
        }

        public int Sum()
        {
            return R + G + B;
        }

        public int Diff(FastImageColor b)
        {
            return Math.Abs(this.Sum() - b.Sum());
        }
    }
}
