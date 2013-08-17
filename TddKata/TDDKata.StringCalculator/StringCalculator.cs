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

        private IEnumerable<string> GetNumbersStringArrays(string numbers)
        {
            var pattern = @"\/\/\[?.+\]?\n";
            var delimiters = new Collection<string> { ",", "\n" };
            MatchCollection matches = Regex.Matches(numbers, pattern, RegexOptions.Singleline);
            if (matches.Count > 0)
            {
                string delimiter = GetDelimiter(matches[0]);
                delimiters.Add(delimiter);
                numbers = numbers.Remove(0, numbers.IndexOf("\n") + 1);
            }
            return numbers.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private string GetDelimiter(object match)
        {
            string delimiter = match.ToString().Remove(0, 2);
            delimiter = delimiter.Remove(delimiter.Length - 1, 1);
            delimiter = delimiter.Trim(new[] { '[', ']' });
            return delimiter;
        }

        private void CheckForNegativeNumbers(IEnumerable<int> numbers) {
            IEnumerable<int> negativeNumbers = numbers.Where(el => el < 0);
            if (negativeNumbers.Any())
            {
                throw new NegativesNotAllowedException(negativeNumbers);
            }
        }

        public int Add(string numbers)
        {
            if (numbers == string.Empty)
            {
                return 0;
            }
            IEnumerable<string> stringNumbersArray = GetNumbersStringArrays(numbers);
            IEnumerable<int> intNumbersArray = ConvertToIntArray(stringNumbersArray);
            intNumbersArray = intNumbersArray.Where(el => el < 1000);
            CheckForNegativeNumbers(intNumbersArray);
            return intNumbersArray.Sum();
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
