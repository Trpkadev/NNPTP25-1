namespace NNPTPZ1 {
    using Mathematics;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    ///     This program should produce Newton fractals.
    ///     See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    public static class Program {
        private const double PixelRoundingThreshold = 0.0001;
        private const int MaxNewtonIterations = 30;
        private const double ConvergenceThreshold = 0.5;
        private const double RootMatchThreshold = 0.01;
        private const int ColorDarkeningFactor = 2;
        private static Color[] Colors { get; } = { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta };

        public static void Main(string[] args)
        {
            var arguments = new Arguments(args);

            using (var bmp = new Bitmap(arguments.SizeX, arguments.SizeY))
            {
                double xStep = (arguments.XMax - arguments.XMin) / arguments.SizeX;
                double yStep = (arguments.YMax - arguments.YMin) / arguments.SizeY;

                var roots = new List<ComplexNumber>();
                var polynome = CreateDefaultPolynomial();
                var derivedPolynome = polynome.Derive();

                Console.WriteLine(polynome);
                Console.WriteLine(derivedPolynome);

                // for every pixel in the image...
                for (int i = 0; i < arguments.SizeX; i++)
                    for (int j = 0; j < arguments.SizeY; j++)
                    {
                        var pixelCoordinate = MapPixelToComplexPlane(i, j, arguments, xStep, yStep);
                        var convergedValue = ApplyNewtonIteration(pixelCoordinate, polynome, derivedPolynome, out int iterations);
                        int rootIndex = FindOrAddRoot(convergedValue, roots);
                        var pixelColor = CalculatePixelColor(rootIndex, iterations);

                        bmp.SetPixel(j, i, pixelColor);
                    }

                bmp.Save(arguments.Output);
            }
        }

        private static Polynome CreateDefaultPolynomial()
        {
            var polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber { RealPart = 1 });
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(new ComplexNumber { RealPart = 1 });
            return polynome;
        }

        private static ComplexNumber MapPixelToComplexPlane(int i, int j, Arguments arguments, double xStep, double yStep)
        {
            double y = arguments.YMin + i * yStep;
            double x = arguments.XMin + j * xStep;

            return new ComplexNumber {
                RealPart = Math.Abs(x) < double.Epsilon ? PixelRoundingThreshold : x,
                ImaginaryPart = Math.Abs(y) < double.Epsilon ? PixelRoundingThreshold : y
            };
        }

        private static ComplexNumber ApplyNewtonIteration(ComplexNumber startValue, Polynome polynome, Polynome derivedPolynome, out int iterations)
        {
            var current = startValue;
            iterations = 0;
            int validIterations = 0;

            while (validIterations < MaxNewtonIterations)
            {
                var diff = polynome.Evaluate(current) / derivedPolynome.Evaluate(current);
                current -= diff;

                double diffMagnitudeSquared = diff.RealPart * diff.RealPart + diff.ImaginaryPart * diff.ImaginaryPart;

                if (diffMagnitudeSquared < ConvergenceThreshold)
                    validIterations++;

                iterations++;
            }

            return current;
        }

        private static int FindOrAddRoot(ComplexNumber value, List<ComplexNumber> roots)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                double distanceSquared = Math.Pow(value.RealPart - roots[i].RealPart, 2) +
                                         Math.Pow(value.ImaginaryPart - roots[i].ImaginaryPart, 2);

                if (distanceSquared <= RootMatchThreshold)
                    return i;
            }

            roots.Add(value);
            return roots.Count - 1;
        }

        private static Color CalculatePixelColor(int rootIndex, int iterations)
        {
            var baseColor = Colors[rootIndex % Colors.Length];

            int darkenFactor = iterations * ColorDarkeningFactor;
            int red = ClampColorComponent(baseColor.R - darkenFactor);
            int green = ClampColorComponent(baseColor.G - darkenFactor);
            int blue = ClampColorComponent(baseColor.B - darkenFactor);

            return Color.FromArgb(red, green, blue);
        }

        private static int ClampColorComponent(int value) => Math.Min(Math.Max(0, value), 255);
    }
}
