using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AddresDuplicate
{
    public static class Util
    {
        public static object GetPropertyValue(object source, string property)
        {
            return source.GetType().GetProperty(property).GetValue(source, null);
        }
        public static void SetPropertyValue(object source, string property, object strvalue)
        {
            source.GetType().GetProperty(property).SetValue(source, strvalue, null);
        }
        public static string[] GetArryFromString(string input)
        {
            return input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string GetStringFromArray(string[] input)
        {
            return string.Join("", input);
        }
        public static bool IsEqual(string input1, string input2)
        {
            input1 = string.IsNullOrEmpty(input1) ? "" : input1;
            input2 = string.IsNullOrEmpty(input2) ? "" : input2;
            return string.CompareOrdinal(input1.ToUpper(), input2.ToUpper()) == 0 ? true : false;
        }

       

       

       
    }
}
