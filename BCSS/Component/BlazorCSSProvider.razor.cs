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
