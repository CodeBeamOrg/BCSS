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

        public string? Add(string value)
        {
            if (Provider == null)
            {
                return null;
            }

            string[] values = value.Split(' ');

            foreach (var val in values)
            {
                string key = Decode(val);
                bool isDuplicated = Provider.CheckDuplicate(val);
                if (isDuplicated)
                {
                    continue;
                }

                string? result = BlazorCssConverter.Convert(val, Provider);

                BcssInfo info = new();
                info.Prefixes = BlazorCssConverter.GetPrefixes(val);
                info.Key = key;
                info.Value = result;
                _ = Provider.AddInfo(info);
            }

            Provider.Update();
            return Decode(value);
        }

        public void Clear()
        {
            Provider?.Clear();
            Provider?.Update();
        }

        public string? this[string key]
        {
            get => Add(key);
        }

        protected internal string Decode(string value)
        {
            return value.ToLower().Replace(":", "_1").Replace("/", "_2").Replace("*", "_3").Replace("#", "_4").Replace(",", "_5").Replace("+", "_6").Replace("%", "_7").Replace(".", "_8").Replace("[", null).Replace("]", null);
        }

        public void SetBreakpoints(int xs = 0, int sm = 600, int md = 960, int lg = 1280, int xl = 1920)
        {
            if (Provider == null)
            {
                return;
            }
#pragma warning disable BL0005
            Provider.Xs = xs;
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
