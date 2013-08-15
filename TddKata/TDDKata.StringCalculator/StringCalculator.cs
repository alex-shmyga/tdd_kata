using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TDDKata.StringCalculator
{
    public class StringCalculator
    {
        private int[] ConvertToIntArray(string[] stringsArray)
        {
            IList<int> result = new List<int>();
            foreach (var number in stringsArray)
            {
                result.Add(int.Parse(number));
            }
            return result.ToArray();
        }

        public int Add(string numbers)
        {         
            if (numbers == string.Empty)
            {
                return 0;
            }
            var pattern = @"\/\/.\n";            
            var delimiters = new Collection<char>{',','\n'};
            MatchCollection matches = Regex.Matches(numbers, pattern, RegexOptions.Singleline);
            if (matches.Count > 0) {
                delimiters.Add(matches[0].ToString()[2]);
                numbers = numbers.Remove(0, 4);
            }
            int[] numbersArray = ConvertToIntArray(numbers.Split(delimiters.ToArray()));
            return numbersArray.Sum();
        }
    }
}
