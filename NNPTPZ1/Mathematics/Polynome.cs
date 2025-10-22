namespace NNPTPZ1.Mathematics {
    using System.Collections.Generic;
    using System.Text;

    public class Polynome {
        /// <summary>
        ///     Constructor
        /// </summary>
        public Polynome()
        {
            Coefficients = new List<ComplexNumber>();
        }

        /// <summary>
        ///     Coefficients
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        ///     Adds a coefficient to the polynomial
        /// </summary>
        /// <param name="coefficient">The coefficient to add</param>
        public void Add(ComplexNumber coefficient) => Coefficients.Add(coefficient);

        /// <summary>
        ///     Derives this polynomial and creates a new one
        /// </summary>
        /// <returns>Derived polynomial</returns>
        public Polynome Derive()
        {
            var derivedPolynome = new Polynome();

            for (int i = 1; i < Coefficients.Count; i++)
            {
                var derivedCoefficient = CalculateDerivativeCoefficient(i);
                derivedPolynome.Coefficients.Add(derivedCoefficient);
            }

            return derivedPolynome;
        }

        /// <summary>
        ///     Evaluates polynomial at a given point
        /// </summary>
        /// <param name="x">Point of evaluation</param>
        /// <returns>Result of polynomial evaluation</returns>
        public ComplexNumber Evaluate(double x) => Evaluate(new ComplexNumber { RealPart = x, ImaginaryPart = 0 });

        /// <summary>
        ///     Evaluates polynomial at a given point
        /// </summary>
        /// <param name="x">Point of evaluation</param>
        /// <returns>Result of polynomial evaluation</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            var sum = ComplexNumber.Zero;

            for (int i = 0; i < Coefficients.Count; i++)
                sum += CalculateTerm(x, i);

            return sum;
        }

        private ComplexNumber CalculateDerivativeCoefficient(int power)
        {
            var powerCoefficient = new ComplexNumber { RealPart = power };
            return Coefficients[power] * powerCoefficient;
        }

        private static ComplexNumber CalculatePower(ComplexNumber x, int power)
        {
            if (power == 0)
                return new ComplexNumber { RealPart = 1, ImaginaryPart = 0 };

            var result = x;
            for (int i = 1; i < power; i++)
                result *= x;

            return result;
        }

        private ComplexNumber CalculateTerm(ComplexNumber x, int power)
        {
            var coefficient = Coefficients[power];
            var xPower = CalculatePower(x, power);
            return coefficient * xPower;
        }

        private void AppendTerm(StringBuilder stringBuilder, int power)
        {
            stringBuilder.Append(Coefficients[power]);

            for (int i = 0; i < power; i++)
                stringBuilder.Append("x");
        }

        /// <summary>
        ///     Returns string representation of polynomial
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < Coefficients.Count; i++)
            {
                AppendTerm(stringBuilder, i);

                if (IsNotLastTerm(i))
                    stringBuilder.Append(" + ");
            }

            return stringBuilder.ToString();


            bool IsNotLastTerm(int index)
            {
                return index + 1 < Coefficients.Count;
            }
        }
    }
}
