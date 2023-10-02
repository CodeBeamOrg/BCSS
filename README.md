# BCSS
### Revolutionary Runtime CSS Generator for Blazor

[![GitHub Repo stars](https://img.shields.io/github/stars/codebeamorg/bcss?color=594ae2&style=flat-square&logo=github)](https://github.com/codebeamorg/bcss/stargazers)
[![GitHub last commit](https://img.shields.io/github/last-commit/codebeamorg/bcss?color=594ae2&style=flat-square&logo=github)](https://github.com/codebeamorg/bcss)
[![Contributors](https://img.shields.io/github/contributors/codebeamorg/bcss?color=594ae2&style=flat-square&logo=github)](https://github.com/codebeamorg/bcss/graphs/contributors)
[![NuGet version](https://img.shields.io/nuget/v/CodeBeam.bcss?color=ff4081&label=nuget%20version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/CodeBeam.bcss)
[![NuGet downloads](https://img.shields.io/nuget/dt/CodeBeam.bcss?color=ff4081&label=nuget%20downloads&logo=nuget&style=flat-square)](https://www.nuget.org/packages/CodeBeam.bcss)

## Installation
1. Install CodeBeam.BCSS Nuget package
```razor
dotnet add package CodeBeam.BCSS
```   
2. Add the following to `_Imports.razor`
```razor
@using BCSS
```
3. Add the following to your HTML **body** section, it's either `index.html` or `_Layout.cshtml`/`_Host.cshtml` depending on whether you're running Server-Side or WASM.
```html
<script src="_content/CodeBeam.BCSS/Bcss.min.js"></script>
```
4. Add the following to the relevant sections of `Program.cs`
```c#
using Bcss.Services;
builder.Services.AddBcss();
```
5. Add the following to`App.razor`
```razor
<BlazorCssProvider />
```
6. Inject the service and place the component in the page that you want to use BCSS
```razor
@inject BcssService Bc
```

## Usage
Add the BCSS class into a class
```razor
<div class="@Bc["w-200 h-100 r-20"]" />
//This line adds width: 200px height: 100px and border-radius: 20
```
## Related Links
- [Docs - bcss.codebeam.org](https://bcss.codebeam.org)
- [Playground](https://bcss.codebeam.org#playground)
- [Showcase](https://bcss.codebeam.org#showcase)
- [QuickDebug](https://bcss.codebeam.org/devtools/quickdebug)
- [Benchmark](https://bcss.codebeam.org/devtools/benchmark)
