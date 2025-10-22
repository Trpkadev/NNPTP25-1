namespace NNPTPZ1.Mathematics {
    using System;

    public class ComplexNumber {
        private const double Tolerance = double.Epsilon;

        public static readonly ComplexNumber Zero = new ComplexNumber {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
                return AreEqual(RealPart, other.RealPart) && AreEqual(ImaginaryPart, other.ImaginaryPart);

            return base.Equals(obj);


            bool AreEqual(double a, double b)
            {
                return Math.Abs(a - b) < Tolerance;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return RealPart.GetHashCode() * 397 ^ ImaginaryPart.GetHashCode();
            }
        }

        private ComplexNumber Add(ComplexNumber other) => new ComplexNumber {
            RealPart = RealPart + other.RealPart,
            ImaginaryPart = ImaginaryPart + other.ImaginaryPart
        };

        private ComplexNumber Subtract(ComplexNumber other) => new ComplexNumber {
            RealPart = RealPart - other.RealPart,
            ImaginaryPart = ImaginaryPart - other.ImaginaryPart
        };

        private ComplexNumber Multiply(ComplexNumber other) => new ComplexNumber {
            RealPart = RealPart * other.RealPart - ImaginaryPart * other.ImaginaryPart,
            ImaginaryPart = RealPart * other.ImaginaryPart + ImaginaryPart * other.RealPart
        };

        private ComplexNumber Divide(ComplexNumber other)
        {
            var conjugate = new ComplexNumber {
                RealPart = other.RealPart,
                ImaginaryPart = -other.ImaginaryPart
            };

            var numerator = Multiply(conjugate);
            double denominator = other.RealPart * other.RealPart + other.ImaginaryPart * other.ImaginaryPart;

            return new ComplexNumber {
                RealPart = numerator.RealPart / denominator,
                ImaginaryPart = numerator.ImaginaryPart / denominator
            };
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) => a.Add(b);
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b) => a.Subtract(b);
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b) => a.Multiply(b);
        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b) => a.Divide(b);
        public static bool operator ==(ComplexNumber a, ComplexNumber b) => a != null && a.Equals(b);
        public static bool operator !=(ComplexNumber a, ComplexNumber b) => !(a == b);

        public double GetAbs() => Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        public double GetAngleInRadians() => Math.Atan(ImaginaryPart / RealPart);
        public override string ToString() => $"({RealPart} + {ImaginaryPart}i)";
    }
}
