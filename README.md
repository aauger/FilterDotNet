
<p align="center">
  <img src="https://raw.githubusercontent.com/aauger/FilterDotNet/master/FilterDotNet.png">
</p>
  
<p align="center" style="font-weight:bold">A modern image filtering/processing library for .NET 6.0, on Windows.<p>

----

Features:
- Image filters, including convolution, sobel, inversion, statistical, &c.
- Generators, including Mandelbrot set, and Perlin noise.
- Designed to be composable, and image lib agnostic*.

##### How to use:

TODO



\* Sole caveat: the providing library must support multi-read/multi-write, or you can provide an intermediate layer to do so, a-la `FastImageLibrary` for GDI images.


