using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Codebase.Utils
{
    public static class AbbrevationUtility
    {
        private static readonly SortedDictionary<int, string> abbrevations = new SortedDictionary<int, string>
        {
            {1000,"k"},
            {1000000, "m" },
            {1000000000, "b" }
        };

        public static string AbbreviateNumber(float number)
        {
            for (int i = abbrevations.Count - 1; i >= 0; i--)
            {
                KeyValuePair<int, string> pair = abbrevations.ElementAt(i);
                if (Mathf.Abs(number) >= pair.Key)
                {
                    string roundedNumber = (number / pair.Key).ToString("0.0");
                    return roundedNumber + pair.Value;
                }
            }
            return number.ToString();
        }
    }
}
