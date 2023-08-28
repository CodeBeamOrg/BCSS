using Microsoft.AspNetCore.Components;

namespace BCSS
{
    public partial class BlazorCssProvider : ComponentBase
    {
        protected List<BCSSInfo> _bcssInfos = new();

        /// <summary>
        /// If true, deletes and overrides all other same CSS properties when new value is added. Default is false.
        /// </summary>
        [Parameter]
        public bool KeepSingleValue { get; set; }

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

            if (KeepSingleValue)
            {
                Clear(info.Key);
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
                result += $".{info.Key}{(info.Suffixes.Contains("hover") ? ":hover" : null)}{(info.Suffixes.Contains("focus") ? ":focus" : null)} {{ {string.Join("!important;", processedValue) + "!important;"}}} ";
            }
            return result;
        }

        public void Update()
        {
            StateHasChanged();
        }

        public void Clear(string key)
        {
            _bcssInfos.RemoveAll(x => x.Key?.Split("-").First() == key.Split('-').First());
            StateHasChanged();
        }

        public void ClearAll()
        {
            _bcssInfos.Clear();
            StateHasChanged();
        }

        public void ClearLast()
        {
            _bcssInfos.Remove(_bcssInfos.Last());
            StateHasChanged();
        }

        private List<string> _breakpoints = new List<string>() { "xs", "sm", "md", "lg", "xl" };
    }
}
