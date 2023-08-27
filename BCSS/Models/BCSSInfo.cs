using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSS.Models
{
    public class BCSSInfo
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
