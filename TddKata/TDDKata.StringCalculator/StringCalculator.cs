using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace TDDKata.StringCalculator
{

    #region class: StringCalculator

    public class StringCalculator
    {

        #region const: Private

        private const string RegExPattern = @"\/\/\[?.+\]?\n";

        #endregion

        #region methods: Private

        private IEnumerable<int> ConvertToIntArray(IEnumerable<string> stringsArray)
        {
            return stringsArray.Select(int.Parse);
        }

        private IEnumerable<string> GetNumbersStringArrays(string numbers)
        {
            var delimiters = GetDefDelimiters();
            if (AddUserDelimiters(delimiters, numbers))
            {
                numbers = numbers.Remove(0, numbers.IndexOf("\n") + 1);
            }
            return numbers.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private ICollection<string> GetDefDelimiters()
        {
            return new Collection<string> { ",", "\n" };
        }

        private bool AddUserDelimiters(ICollection<string> delimiters, string numbers)
        {
            MatchCollection matches = Regex.Matches(numbers, RegExPattern, RegexOptions.Singleline);
            if (matches.Count > 0)
            {
                var delimitersArray = GetDelimiters(matches[0]).Split(new[] { "][" }, StringSplitOptions.None);
                delimitersArray.ToList().ForEach(delimiters.Add);
                return true;
            }
            return false;
        }

        private string GetDelimiters(object match)
        {
            string delimiter = match.ToString().Remove(0, 2);
            delimiter = delimiter.Remove(delimiter.Length - 1, 1);
            delimiter = delimiter.Trim(new[] { '[', ']' });
            return delimiter;
        }

        private void CheckForNegativeNumbers(IEnumerable<int> numbers)
        {
            IEnumerable<int> negativeNumbers = numbers.Where(el => el < 0);
            if (negativeNumbers.Any())
            {
                throw new NegativesNotAllowedException(negativeNumbers);
            }
        }

        private IEnumerable<int> GetIntNumbersWithoutBigNumbers(IEnumerable<int> intNumbersArray)
        {
            return intNumbersArray.Where(el => el < 1000);
        }

        #endregion

        #region methods: Public 

        public int Add(string numbers)
        {
            if (numbers == string.Empty)
            {
                return 0;
            }
            IEnumerable<string> stringNumbersArray = GetNumbersStringArrays(numbers);
            IEnumerable<int> intNumbersArray = ConvertToIntArray(stringNumbersArray);
            intNumbersArray = GetIntNumbersWithoutBigNumbers(intNumbersArray);
            CheckForNegativeNumbers(intNumbersArray);
            return intNumbersArray.Sum();
        }

        #endregion
    }

    #endregion

    #region class: NegativesNotAllowedException

    public class NegativesNotAllowedException : Exception
    {
        #region properties: Public

        private readonly string _message;
        public override string Message
        {
            get
            {
                return _message;
            }
        }

        #endregion

        #region constructor:

        public NegativesNotAllowedException(IEnumerable<int> param)
        {
            _message = string.Format("Negatives not allowed. There are negative numbers: {0}",
                    string.Join(", ", param).TrimEnd());
        }

        #endregion
    }

    #endregion
}