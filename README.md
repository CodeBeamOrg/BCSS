# BCSS
### Runtime CSS Generator for Blazor

## Installation
1. Install CodeBeam.BCSS Nuget package

2. Add the following to `_Imports.razor`
```razor
@using BCSS
```
3. Inject the service and place the component in the page that you want to use BCSS
```razor
@inject BCSSService BC
<BlazorCssProvider />
```

## Usage
Add the BCSS class into a class
```razor
<div class="@BC.Add("w-200 h-100 r-20")" />
//This line adds width: 200px height: 100px and border-radius: 20
```
