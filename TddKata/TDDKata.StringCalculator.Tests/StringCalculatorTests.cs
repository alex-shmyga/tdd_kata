using NUnit.Framework;
using System;

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
        public void Add_EmpthyNumbers_ReturnZero()
        {
            Assert.AreEqual(0, calculator.Add(string.Empty));
        }

        [Test]
        public void Add_OneNumber_ReturnSameNumber()
        {
            Assert.AreEqual(1, calculator.Add("1"));
        }

        [Test]
        public void Add_TwoNumbers_ReturnSumNumers()
        {
            Assert.AreEqual(3, calculator.Add("1,2"));
        }

        [Test]
        public void Add_UnknownCountNumbers_ReturnSumNumers()
        {
            Assert.AreEqual(15, calculator.Add("1,2,3,4,5"));
        }

        [Test]
        public void Add_NewLineBetweenNumbers_ReturnSumNumers()
        {
            Assert.AreEqual(6, calculator.Add("1\n2,3"));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Add_WrongInputNumbers_FormatExceptionThrown()
        {
            Assert.AreEqual(6, calculator.Add(@"1, \ N"));
        }

        [Test]
        public void Add_DifferentDelimiters_ReturnSumNumers()
        {
            Assert.AreEqual(3, calculator.Add("//;\n1;2"));
        }

        [Test]
        public void Add_NegativeInputNumbers_NegativesNotAllowedExceptionThrown()
        {
            NegativesNotAllowedException ex =
                Assert.Throws<NegativesNotAllowedException>(() => calculator.Add("1,-2,3,4,-5"));
            Assert.That(ex.Message, Is.EqualTo("Negatives not allowed. There are negative numbers: -2, -5"));
        }

        [Test]
        public void Add_NumbersBiggerThan1000Ignored_ReturnSumNumers()
        {
            Assert.AreEqual(2, calculator.Add("2, 1001"));
        }
        
        [Test]
        public void Add_DelimitersCanBeOfAnyLength_ReturnSumNumers()
        {
            Assert.AreEqual(6, calculator.Add("//[***]\n1***2***3"));
        }

        [Test]
        public void Add_MultipleDelimiters_ReturnSumNumers()
        {
            Assert.AreEqual(6, calculator.Add("//[*][%]\n1*2%3"));
        }
    }
}
