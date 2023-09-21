﻿using BCSS.Services;

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

                BcssInfo info = new BcssInfo();
                info.Suffixes = BlazorCssConverter.GetSuffixes(val);
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

        public string this[string key]
        {
            get => Add(key);
        }

        protected string Decode(string value)
        {
            return value.Replace(":", "_1").Replace("/", "_2").Replace("*", "_3").Replace("#", "_4").Replace(",", "_5").Replace("+", "_6").Replace("%", "_7").Replace(".", "_8").Replace("[", null).Replace("]", null);
        }
         

    }
}