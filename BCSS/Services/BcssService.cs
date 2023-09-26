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
                bool isDuplicated = Provider.CheckDuplicate(val);
                if (isDuplicated)
                {
                    continue;
                }

                string? result = BlazorCssConverter.Convert(val);

                BcssInfo info = new();
                info.Prefixes = BlazorCssConverter.GetPrefixes(val);
                info.Key = Decode(val);
                info.Value = result;
                Provider.AddInfo(info);
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

        protected string Decode(string value)
        {
            return value.Replace(":", "_1").Replace("/", "_2").Replace("*", "_3").Replace("#", "_4").Replace(",", "_5").Replace("+", "_6").Replace("%", "_7").Replace(".", "_8").Replace("[", null).Replace("]", null);
        }

        public void ChangeBreakpoints(int xs = 0, int sm = 600, int md = 960, int lg = 1280, int xl = 1920)
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

    }
}
