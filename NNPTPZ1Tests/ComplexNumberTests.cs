namespace NNPTPZ1Tests {
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NNPTPZ1.Mathematics;

    [TestClass]
    public class ComplexNumberTests {
        [TestMethod]
        public void AddTest_BasicAddition_ReturnsCorrectSum()
        {
            var a = new ComplexNumber {
                RealPart = 10,
                ImaginaryPart = 20
            };
            var b = new ComplexNumber {
                RealPart = 1,
                ImaginaryPart = 2
            };
            var expected = new ComplexNumber {
                RealPart = 11,
                ImaginaryPart = 22
            };

            var actual = a + b;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddTest_AddingZero_ReturnsOriginalNumber()
        {
            var a = new ComplexNumber {
                RealPart = 1,
                ImaginaryPart = -1
            };
            var zero = new ComplexNumber {
                RealPart = 0,
                ImaginaryPart = 0
            };
            var expected = new ComplexNumber {
                RealPart = 1,
                ImaginaryPart = -1
            };

            var actual = a + zero;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_PositiveImaginaryPart_ReturnsCorrectFormat()
        {
            var number = new ComplexNumber {
                RealPart = 10,
                ImaginaryPart = 20
            };

            string result = number.ToString();

            Assert.AreEqual("(10 + 20i)", result);
        }

        [TestMethod]
        public void ToString_NegativeImaginaryPart_ReturnsCorrectFormat()
        {
            var number = new ComplexNumber {
                RealPart = 1,
                ImaginaryPart = -1
            };

            string result = number.ToString();

            Assert.AreEqual("(1 + -1i)", result);
        }

        [TestMethod]
        public void ToString_ZeroNumber_ReturnsCorrectFormat()
        {

            var zero = ComplexNumber.Zero;

            string result = zero.ToString();

            Assert.AreEqual("(0 + 0i)", result);
        }

        [TestMethod]
        public void Polynome_EvaluateAtZero_ReturnsConstantTerm()
        {
            var poly = CreatePolynomial(1, 0, 1);// 1 + 0x + 1x²
            var x = new ComplexNumber { RealPart = 0, ImaginaryPart = 0 };
            var expected = new ComplexNumber { RealPart = 1, ImaginaryPart = 0 };

            var result = poly.Evaluate(x);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Polynome_EvaluateAtOne_ReturnsCorrectResult()
        {
            var poly = CreatePolynomial(1, 0, 1);// 1 + 0x + 1x²
            var x = new ComplexNumber { RealPart = 1, ImaginaryPart = 0 };
            var expected = new ComplexNumber { RealPart = 2, ImaginaryPart = 0 };

            var result = poly.Evaluate(x);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Polynome_EvaluateAtTwo_ReturnsCorrectResult()
        {
            var poly = CreatePolynomial(1, 0, 1);// 1 + 0x + 1x²
            var x = new ComplexNumber { RealPart = 2, ImaginaryPart = 0 };
            var expected = new ComplexNumber { RealPart = 5, ImaginaryPart = 0 };

            var result = poly.Evaluate(x);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Polynome_ToString_ReturnsCorrectRepresentation()
        {
            var poly = CreatePolynomial(1, 0, 1);

            string result = poly.ToString();

            Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", result);
        }

        private static Polynome CreatePolynomial(params double[] realCoefficients)
        {
            var poly = new Polynome();
            foreach (double coefficient in realCoefficients)
            {
                poly.Coefficients.Add(new ComplexNumber {
                    RealPart = coefficient,
                    ImaginaryPart = 0
                });
            }
            return poly;
        }
    }
}
