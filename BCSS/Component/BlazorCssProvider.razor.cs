using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BCSS
{
    public partial class BlazorCssProvider : ComponentBase
    {
        [Inject] IJSRuntime JSRuntime { get; set; }

        protected List<BcssInfo> _bcssInfos = new();

        /// <summary>
        /// If true, BCSS skips the validation checks and increase performance.
        /// </summary>
        [Parameter]
        public bool PerformanceMode { get; set; }

        /// <summary>
        /// If true, deletes and overrides all other same CSS properties when new value is added. Default is false.
        /// </summary>
        [Parameter]
        public bool KeepSingleValue { get; set; }

        [Parameter]
        public int Spacing { get; set; } = 1;

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
            BcssService.Attach(this);
        }

        bool _firstRendered;
        //int _renderCount;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CheckAllValues();
                _firstRendered = true;
                StateHasChanged();
            }
            //_renderCount++;
        }

        private bool _shouldRender = false;
        protected override bool ShouldRender()
        {
            return _shouldRender;
        }

        //string? _isValidResult;
        protected internal async Task AddInfo(BcssInfo info)
        {
            if (string.IsNullOrEmpty(info.Key) || string.IsNullOrEmpty(info.Value))
            {
                return;
            }
            if (_bcssInfos.Any(x => x.Key == info.Key && x.Value == info.Value))
            {
                return;
            }

            if (PerformanceMode)
            {
                if (KeepSingleValue)
                {
                    Clear(info.Key);
                }
                _bcssInfos.Add(info);
                _shouldRender = true;
                StateHasChanged();
                _shouldRender = false;
                return;
            }

            List<string> splittedValue = info.Value.Split(' ').ToList();
            bool isValid = true;
            foreach (var v in splittedValue)
            {
                string[] definition = v.Replace('*', ' ').Replace('+', '-').Split(':');
                if (definition.Length > 1)
                {
                    isValid = await IsValid(definition[0], definition[1]);
                    //_isValidResult = "Is Valid" + isValid.ToString();
                }
                if (isValid == false)
                {
                    break;
                }
            }

            if (isValid == true)
            {
                if (KeepSingleValue)
                {
                    Clear(info.Key);
                }

                _bcssInfos.Add(info);
            }
            _shouldRender = true;
            StateHasChanged();
            _shouldRender = false;
        }

        protected internal async Task<bool> IsValid(string propName, string propValue, bool force = false)
        {
            object[] parameters = new object[] { propName, propValue };
            if (_firstRendered || force == true)
            {
                bool result = await JSRuntime.InvokeAsync<bool>("checkcss", parameters);
                return result;
            }
            return true;
        }

        public async Task CheckAllValues()
        {
            List<BcssInfo> toBeDeletedInfos = new();
            foreach (var item in _bcssInfos)
            {
                if (await CheckValueIsValid(item, true) == false)
                {
                    toBeDeletedInfos.Add(item);
                }
            }
            _bcssInfos.RemoveAll(x => toBeDeletedInfos.Select(y => y.Key).Contains(x.Key));
        }

        protected async Task<bool> CheckValueIsValid(BcssInfo? bcssInfo, bool force = false)
        {
            if (bcssInfo == null || bcssInfo.Value == null)
            {
                return false;
            }
            
            string[] definition = bcssInfo.Value.Replace('*', ' ').Split(':');
            if (definition.Length > 1)
            {
                bool result = await IsValid(definition[0], definition[1], force);
                return result;
            }
            return false;
        }

        protected internal bool CheckDuplicate(string value) 
        {
            return _bcssInfos.Any(x => x.Value == value);
        }

        protected string GetMediaString(string breakpoint)
        {
            string result = string.Empty;
            foreach (var info in _bcssInfos.Where(x => x.Prefixes.Contains(breakpoint)))
            {
                var processedValue = info.Value?.Split(' ') ?? new string[0];
                result += $".{info.Key}{GetPrefixString(info.Prefixes)} {{ {string.Join("!important;", processedValue) + "!important;"} }}";
            }
            return result;
        }

        protected string GetPrefixString(List<string> prefixes)
        {
            var result = string.Empty;
            List<string> match = _prefixes.Intersect(prefixes).ToList();
            if (match.Any())
            {
                result = $":{match.First()}";
            }

            if (result == ":h")
            {
                return ":hover";
            }
            if (result == ":f")
            {
                return ":focus";
            }
            
            return result;
        }

        public void Update()
        {
            _shouldRender = true;
            StateHasChanged();
            _shouldRender = false;
        }

        public void Clear(string key)
        {
            _bcssInfos.RemoveAll(x => x.Key?.Split("-").First() == key.Split('-').First());
            StateHasChanged();
        }

        public void Clear()
        {
            _bcssInfos.Clear();
            StateHasChanged();
        }

        public void ClearLast()
        {
            _bcssInfos.Remove(_bcssInfos.Last());
            StateHasChanged();
        }

        private readonly List<string> _breakpoints = new List<string>() { "xs", "sm", "md", "lg", "xl" };
        private readonly List<string> _prefixes = new List<string>() 
        { 
            "active",
            "checked",
            "disabled",
            "empty",
            "enabled",
            "h",
            "hover",
            "f",
            "focus",
            "focus-visible",
            "focus-within", 
            "first-child", 
            "last-child",
            "link",
            "optional",
            "out-of-range",
            "read-only",
            "read-write",
            "required",
            "root",
            "target",
            "valid",
            "invalid",
            "visited"
        };

    }
}
