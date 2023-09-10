using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BCSS
{
    public partial class BlazorCssProvider : ComponentBase
    {
        [Inject] IJSRuntime JSRuntime { get; set; }

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

        bool _firstRendered;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CheckAllValues();
                _firstRendered = true;
                StateHasChanged();
            }
        }

        string? _isValidResult;
        protected internal async Task AddInfo(BCSSInfo info)
        {
            if (string.IsNullOrEmpty(info.Key) || string.IsNullOrEmpty(info.Value))
            {
                return;
            }
            if (_bcssInfos.Any(x => x.Key == info.Key && x.Value == info.Value))
            {
                return;
            }

            bool isValid = true;
            string[] definition = info.Value.Split(':');
            if (definition.Length > 1)
            {
                isValid = await IsValid(definition[0], definition[1]);
                _isValidResult = "Is Valid" + isValid.ToString();
            }

            if (isValid == true)
            {
                if (KeepSingleValue)
                {
                    Clear(info.Key);
                }


                _bcssInfos.Add(info);
            }
            StateHasChanged();
        }

        protected internal async Task<bool> IsValid(string propName, string propValue, bool force = false)
        {
            object[] parameters = new object[] { propName, propValue };
            if (_firstRendered || force == true)
            {
                try
                {
                    bool result = await JSRuntime.InvokeAsync<bool>("checkcss", parameters);
                    return result;
                }
                catch
                {
                    
                }
                
            }
            return true;
        }

        public async Task CheckAllValues()
        {
            List<BCSSInfo> toBeDeletedInfos = new();
            foreach (var item in _bcssInfos)
            {
                if (await CheckValueIsValid(item, true) == false)
                {
                    toBeDeletedInfos.Add(item);
                }
            }
            _bcssInfos.RemoveAll(x => toBeDeletedInfos.Select(y => y.Key).Contains(x.Key));
        }

        protected async Task<bool> CheckValueIsValid(BCSSInfo? bcssInfo, bool force = false)
        {
            if (bcssInfo == null || bcssInfo.Value == null)
            {
                return false;
            }
            
            string[] definition = bcssInfo.Value.Split(':');
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
            foreach (var info in _bcssInfos.Where(x => x.Suffixes.Contains(breakpoint)))
            {
                var processedValue = info.Value?.Split(' ') ?? new string[0];
                result += $".{info.Key}{(info.Suffixes.Contains("hover") ? ":hover" : null)}{(info.Suffixes.Contains("focus") ? ":focus" : null)} {{ {string.Join("!important;", processedValue) + "!important;"}}} ";
            }
            return result;
        }

        protected string GetSuffixString(List<string> suffixes)
        {
            var result = string.Empty;
            List<string> match = _suffixes.Intersect(suffixes).ToList();
            if (match.Any())
            {
                result = $":{match.First()}";
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

        private readonly List<string> _breakpoints = new List<string>() { "xs", "sm", "md", "lg", "xl" };
        private readonly List<string> _suffixes = new List<string>() 
        { 
            "active",
            "checked",
            "disabled",
            "empty",
            "enabled",
            "hover", 
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
