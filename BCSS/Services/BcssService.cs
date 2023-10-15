using BCSS.Services;

namespace BCSS
{
    public class BcssService
    {
        public BlazorCssProvider? Provider { get; set; }

        public void Attach(BlazorCssProvider provider)
        {
            Provider = provider;
        }

        //This is the method where we need maximum performance.
        public string? Add(string value)
        {
            if (Provider == null)
            {
                return null;
            }

            List<string> decodedValue = new();

            string[] values = value.Split(' ');
            foreach (var val in values)
            {
                List<string> prefixes = BlazorCssConverter.GetPrefixes(val);
                if (prefixes.Contains("c"))
                {
                    decodedValue.Add(val);
                    continue;
                }

                BcssInfo? duplicatedInfo = Provider.CheckDuplicate(val);
                if (duplicatedInfo != null)
                {
                    decodedValue.Add(duplicatedInfo.Key ?? string.Empty);
                    continue;
                }

                if (Provider.UnifiedClasses != null)
                {
                    if (Provider.UnifiedClasses.ContainsKey(val))
                    {
                        decodedValue.Add(Provider.UnifiedClasses[val]);
                        continue;
                    }
                }

                string? result = BlazorCssConverter.Convert(val, Provider);
                string key = Decode(val);
                BcssInfo info = new();
                info.Prefixes = prefixes;
                info.Key = key;
                info.Value = result;
                _ = Provider.AddInfo(info);
                decodedValue.Add(key);
            }

            Provider.Update();
            return string.Join(" ", decodedValue);
        }

        /// <summary>
        /// Clears regular BCSS classes.
        /// </summary>
        /// <param name="update"></param>
        public void Clear(bool update = true)
        {
            if (Provider == null)
            {
                return;
            }
            Provider.Clear();
            foreach (var item in Provider.UnifiedClasses ?? new Dictionary<string, string>())
            {
                AddUnifiedClass(item.Key, item.Value);
            }
            if (update == true)
            {
                Provider.Update();
            }
        }

        /// <summary>
        /// Clears unified classes.
        /// </summary>
        /// <param name="update"></param>
        public void ClearUnifiedClasses(bool update = true)
        { 
            if (Provider == null) 
            { 
                return;
            }
            Provider.UnifiedClasses = null;
            if (update == true)
            {
                Provider.Update();
            }
        }

        /// <summary>
        /// Clears both Unified Classes and regular BCSS classes.
        /// </summary>
        public void Reset()
        {
            if (Provider == null)
            {
                return;
            }

            ClearUnifiedClasses(false);
            Clear(false);
            Provider.Update();
        }

        public void AddUnifiedClass(string unifiedName, string value)
        {
            if (Provider == null)
            {
                return;
            }

            if (Provider.UnifiedClasses == null)
            {
                Provider.UnifiedClasses = new();
            }

            Add(value);
            Provider.UnifiedClasses.TryAdd(unifiedName, value);
        }

        public string? this[string key]
        {
            get => Add(key);
        }

        protected internal string Decode(string value)
        {
            string result = value.ToLower();
            if (result.StartsWith("c:"))
            {
                result = result.Substring(2);
            }
            return result.Replace(':', 'q').Replace('/', 'w').Replace('*', 'e').Replace('#', 'r').Replace(',', 't').Replace('+', 'y').Replace('%', 'a').Replace('.', 's').Replace("[", null).Replace("]", null);
        }

        public void SetBreakpoints(int xs = 0, int sm = 600, int md = 960, int lg = 1280, int xl = 1920)
        {
            if (Provider == null)
            {
                return;
            }
#pragma warning disable BL0005
            Provider.Sm = sm;
            Provider.Md = md;
            Provider.Lg = lg;
            Provider.Xl = xl;
#pragma warning restore BL0005
        }

        public void SetSpacing(int value)
        {
            if (Provider == null)
            {
                return;
            }
#pragma warning disable BL0005
            Provider.Spacing = value;
#pragma warning restore BL0005
        }

        public void SetPerformanceMode(bool value)
        {
            if (Provider == null)
            {
                return;
            }
#pragma warning disable BL0005
            Provider.PerformanceMode = value;
#pragma warning restore BL0005
        }

        public async Task CheckValuesIsValid()
        {
            if (Provider == null)
            {
                return;
            }
            await Provider.CheckAllValues();
        }

    }
}
