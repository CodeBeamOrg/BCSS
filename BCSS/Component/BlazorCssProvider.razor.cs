using BCSS.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BCSS
{
    public partial class BlazorCssProvider : ComponentBase
    {
        [Inject] IJSRuntime JSRuntime { get; set; }

        protected List<BcssInfo> _bcssInfos = new();

        protected internal Dictionary<string, string>? UnifiedClasses { get; set; }

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

        /// <summary>
        /// The spacing multiplier for px measured CSS properties like padding, margin, top, left, right, bottom.
        /// </summary>
        [Parameter]
        public int Spacing { get; set; } = 1;

        /// <summary>
        /// The small size measured by pixels.
        /// </summary>
        [Parameter]
        public int Sm { get; set; } = 600;

        /// <summary>
        /// The medium size measured by pixels.
        /// </summary>
        [Parameter]
        public int Md { get; set; } = 960;

        /// <summary>
        /// The large size measured by pixels.
        /// </summary>
        [Parameter]
        public int Lg { get; set; } = 1280;

        /// <summary>
        /// The extra large size measured by pixels.
        /// </summary>
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
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
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
                Update();
                return;
            }

            List<string> splittedValue = info.Value.Split(' ').ToList();
            bool isValid = true;
            foreach (var v in splittedValue)
            {
                string[] definition = BlazorCssConverter.PostProcess(v).Split(':');
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
            Update();
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

        /// <summary>
        /// Check all BCSS classes if they are valid or not. The method will automatically removes stored and invalid BCSS classes.
        /// </summary>
        /// <returns></returns>
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
            
            string[] definition = BlazorCssConverter.PostProcess(bcssInfo.Value).Split(':');
            if (definition.Length > 1)
            {
                bool result = await IsValid(definition[0], definition[1], force);
                return result;
            }
            return false;
        }

        protected internal BcssInfo? CheckDuplicate(string key) 
        {
            return _bcssInfos.FirstOrDefault(x => x.Key == key);
        }

        protected string GetMediaString(string breakpoint)
        {
            string result = string.Empty;
            foreach (var info in _bcssInfos.Where(x => x.Prefixes.Contains(breakpoint)))
            {
                var listedValue = info.Value?.Split(' ') ?? new string[0];
                List<string> processedValue = new();
                foreach (var s in listedValue)
                {
                    processedValue.Add(GetWebkitString(info.Prefixes) + BlazorCssConverter.PostProcess(s));
                }
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

        protected string GetWebkitString(List<string> prefixes)
        {
            var result = string.Empty;
            if (prefixes.Contains("w"))
            {
                return "-webkit-";
            }
            if (prefixes.Contains("m"))
            {
                return "-moz-";
            }
            if (prefixes.Contains("o"))
            {
                return "-o-";
            }
            if (prefixes.Contains("ms"))
            {
                return "-ms-";
            }

            return result;
        }

        public void Update()
        {
            _shouldRender = true;
            StateHasChanged();
            _shouldRender = false;
        }

        /// <summary>
        /// Removes all BCSS classes (if key is null or empty) or matched classes (if key specified). If a BCSS class is currently using in the page, it will automatically add again.
        /// </summary>
        /// <param name="key"></param>
        public void Clear(string? key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                _bcssInfos.Clear();
                return;
            }
            _bcssInfos.RemoveAll(x => x.Key?.Split("-").First() == key.Split('-').First());
        }

        /// <summary>
        /// Removes last added BcssInfo.
        /// </summary>
        public void ClearLast()
        {
            _bcssInfos.Remove(_bcssInfos.Last());
        }

        private readonly List<string> _breakpoints = new List<string>() { "xs", "sm", "md", "lg", "xl", "mobile" };
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
