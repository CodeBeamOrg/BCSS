using BCSS.Services;

namespace BCSS
{
    public class BCSSService
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

                BCSSInfo info = new BCSSInfo();
                info.Suffixes = BlazorCssConverter.GetSuffixes(val);
                info.Key = Decode(val);
                info.Value = result;
                Provider.AddInfo(info);
            }
            
            Provider.Update();
            return Decode(value);
        }

        public string this[string key]
        {
            get => Add(key);
        }

        protected string Decode(string value)
        {
            return value.Replace("%", "--").Replace(".", "_-").Replace(":", "_1").Replace("/", "_2").Replace("*", "_3");
        }
         

    }
}
