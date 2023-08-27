using BCSS.Models;
using Microsoft.AspNetCore.Components;

namespace BCSS
{
    public partial class BlazorCSSProvider : ComponentBase
    {
        protected List<BCSSInfo> _bcssInfos = new();

        [Parameter]
        public int Xs { get; set; } = 0;

        [Parameter]
        public int Sm { get; set; } = 600;

        [Parameter]
        public int Md { get; set; } = 960;

        [Parameter]
        public int Lg { get; set; } = 1280;

        [Parameter]
        public int Xl { get; set; } = 1920;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            BCSSService.Attach(this);
        }

        protected internal void AddInfo(BCSSInfo info)
        {
            if (string.IsNullOrEmpty(info.Key) || string.IsNullOrEmpty(info.Value))
            {
                return;
            }
            if (_bcssInfos.Any(x => x.Key == info.Key && x.Value == info.Value))
            {
                return;
            }
            _bcssInfos.Add(info);
        }

        protected internal bool CheckDuplicate(string value) 
        {
            return _bcssInfos.Any(x => x.Value == value);
        }

        protected string GetMediaString(string breakpoint)
        {
            string result = string.Empty;
            foreach (var info in _bcssInfos.Where(x => x.Suffixes.Contains(breakpoint)))
            {
                var processedValue = info.Value?.Split(' ') ?? new string[0];
                result += $".{info.Key}{(info.Suffixes.Contains("hover") ? ":hover" : null)} {{ {string.Join("!important;", processedValue) + "!important;"}}} ";
            }
            return result;
        }

        public void Update()
        {
            StateHasChanged();
        }

        public void Clear()
        {
            _bcssInfos.Clear();
            StateHasChanged();
        }

        private List<string> _breakpoints = new List<string>() { "xs", "sm", "md", "lg", "xl" };
    }
}
