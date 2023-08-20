using Microsoft.AspNetCore.Components;

namespace BCSS
{
    public partial class BlazorCSSProvider : ComponentBase
    {
        protected Dictionary<string, string?> _keyValuePairs = new Dictionary<string, string?>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            BCSSService.Attach(this);
        }

        //protected internal string? Decode(string value)
        //{
        //    if (value == "w-400")
        //    {
        //        _keyValuePairs.Add(value, "width: 400px !important;");
        //        return "width: 400px";
        //    }
        //    if (value == "w-300")
        //    {
        //        _keyValuePairs.Add(value, "width: 300px !important;");
        //        return "width: 300px";
        //    }
        //    return null;
        //}

        protected internal void AddToDict(string key, string value)
        {
            if (_keyValuePairs.ContainsKey(key))
            {
                return;
            }
            _keyValuePairs.Add(key, value);
        }

        protected internal bool CheckDuplicate(string value) 
        {
            return _keyValuePairs.ContainsKey(value);
        }

        protected internal void Update()
        {
            StateHasChanged();
        }
    }
}
