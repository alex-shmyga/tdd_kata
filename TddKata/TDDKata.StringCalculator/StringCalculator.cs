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
        private IEnumerable<int> ConvertToIntArray(IEnumerable<string> stringsArray)
        {
            var result = new Collection<int>();
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
            var delimiters = new Collection<char> { ',', '\n' };
            MatchCollection matches = Regex.Matches(numbers, pattern, RegexOptions.Singleline);
            if (matches.Count > 0)
            {
                delimiters.Add(matches[0].ToString()[2]);
                numbers = numbers.Remove(0, 4);
            }
            var numbersArray = ConvertToIntArray(numbers.Split(delimiters.ToArray()));
            numbersArray = numbersArray.Where(el => el < 1000);
            var negativeNumbers = numbersArray.Where(el => el < 0);
            if (negativeNumbers.Any())
            {
                throw new NegativesNotAllowedException(negativeNumbers);
            }
            return numbersArray.Sum();
        }
    }

    public class NegativesNotAllowedException : Exception
    {
        public NegativesNotAllowedException(IEnumerable<int> param)
        {
            _message = string.Format("Negatives not allowed. There are negative numbers: {0}",
                    string.Join(", ", param).TrimEnd());
        }

        private readonly string _message;
        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
