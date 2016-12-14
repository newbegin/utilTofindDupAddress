using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddresDuplicate
{
    public static class StringExtensions
    {
        public static string MakeMyFormat(this string input)
        {
            //your code to sanitize your string, for example
            if (input == null) return "";
            var trimmed = input.Trim();
            return trimmed.ToUpper();
        }
    }
}
