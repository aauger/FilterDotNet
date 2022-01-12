using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Generators
{
    internal class Matrix<T>
    {
        private T[,] arr;
        public int Width { get; }
        public int Height { get; }

        public Matrix(int w, int h)
        {
            this.arr = new T[w, h];
            Width = w;
            Height = h;
        }

        public void Set(int x, int y, T val)
        {
            this.arr[x, y] = val;
        }

        public T Get(int x, int y)
        {
            return this.arr[x, y];
        }

        public void Clear(int x, int y)
        {
            arr[x, y] = default;
        }
    }
}
