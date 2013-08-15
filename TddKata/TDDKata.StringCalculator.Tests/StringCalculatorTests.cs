using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TDDKata.StringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private StringCalculator calculator;

        [SetUp]
        public void Init()
        {
            calculator = new StringCalculator();
        }

        [Test]
        public void Sum_EmpthyNumbers_ReturnZero()
        {
            Assert.AreEqual(0, calculator.Add(string.Empty));
        }

        [Test]
        public void Sum_OneNumber_ReturnSameNumber()
        {
            Assert.AreEqual(1, calculator.Add("1"));
        }

        [Test]
        public void Sum_TwoNumbers_ReturnSumNumers()
        {
            Assert.AreEqual(3, calculator.Add("1,2"));
        }

        [Test]
        public void Sum_UnknownCountNumbers_ReturnSumNumers()
        {
            Assert.AreEqual(15, calculator.Add("1,2,3,4,5"));
        }

        [Test]
        public void Sum_NewLineBetweenNumbers_ReturnSumNumers()
        {
            Assert.AreEqual(6, calculator.Add("1\n2,3"));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Sum_WrongInputNumbers_FormatExceptionThrown()
        {
            Assert.AreEqual(6, calculator.Add(@"1, \ N"));
        }

        [Test]
        public void Sum_DifferentDelimiters_ReturnSumNumers()
        {
            Assert.AreEqual(3, calculator.Add("//;\n1;2"));
        }

        [Test]
        public void Sum_NegativeInputNumbers_NegativesNotAllowedExceptionThrown()
        {
            NegativesNotAllowedException ex =
                Assert.Throws<NegativesNotAllowedException>(() => calculator.Add(@"1,-2,3,4,-5"));
            Assert.That(ex.Message, Is.EqualTo("Negatives not allowed. There are negative numbers: -2, -5"));
        }


    }
}
