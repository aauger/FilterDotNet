# AAImageFilter
.NET 6.0 rendition of an old image filter project I had started. Porting work continues.

Filters are built in a separate class library, and intended to be used with configurators when necessary.
The contrived example given here is for Winforms, but no doubt you could conceive of a configurator that strips out an HTTP request, &c.

```csharp
var filter = new Sharpen(new WinformIntConfigurator("Sharpening level: "));
if (filter is IConfigurableFilter)
  filter.Initialize();
filter.Apply(inputImage);
```

For a console program, you might do:

```csharp
new Sharpen(new ConsoleIntConfigurator(args[1])).Initialize().Apply(Image.FromFile(args[0]).Save(...)
```

and so on.
