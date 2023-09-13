﻿namespace BCSS
{
    public class BcssInfo
    {
        public List<string> Suffixes { get; set; } = new List<string>();
        public string? Key { get; set; }
        public string? Value { get; set; }

        public bool ContainsKey(string key)
        {
            if (key == Key)
            {
                return true;
            }
            return false;
        }
    }
}