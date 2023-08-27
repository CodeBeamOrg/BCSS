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
                Provider.AddToDict(Decode(val), result);
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
