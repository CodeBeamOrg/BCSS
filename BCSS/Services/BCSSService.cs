using BCSS.Models;
using BCSS.Services;

namespace BCSS
{
    public class BCSSService
    {
        public BlazorCSSProvider? Provider { get; set; }

        public void Attach(BlazorCSSProvider provider)
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

                BCSSInfo info = new BCSSInfo();
                info.Suffixes = BlazorCssConverter.GetSuffixes(val);
                info.Key = Decode(val);
                info.Value = result;
                Provider.AddInfo(info);
            }
            
            Provider.Update();
            return Decode(value);
        }

        protected string Decode(string value)
        {
            return value.Replace("%", "--").Replace(".", "_-").Replace(":", "_1");
        }
         

    }
}
