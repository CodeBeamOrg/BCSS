# BCSS
### Runtime CSS Generator for Blazor

## Installation
1. Install CodeBeam.BCSS Nuget package
2. Add the following to `_Imports.razor`
```razor
@using BCSS
```
3. Add the following to your HTML **body** section, it's either `index.html` or `_Layout.cshtml`/`_Host.cshtml` depending on whether you're running Server-Side or WASM.
```html
<script src="_content/CodeBeam.BCSS/Bcss.js"></script>
```
4. Add the following to`App.razor`
```razor
<BlazorCssProvider />
```
5. Inject the service and place the component in the page that you want to use BCSS
```razor
@inject BCSSService Bc
```

## Usage
Add the BCSS class into a class
```razor
<div class="@Bc.Add("w-200 h-100 r-20")" />
//This line adds width: 200px height: 100px and border-radius: 20
```
