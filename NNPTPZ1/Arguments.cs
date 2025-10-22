namespace NNPTPZ1 {
    using System;

    internal struct Arguments {
        internal double XMin { get; }
        internal double XMax { get; }
        internal double YMin { get; }
        internal double YMax { get; }
        internal int SizeX { get; }
        internal int SizeY { get; }
        internal string Output { get; }

        internal Arguments(string[] args)
        {
            if (args.Length < 6 || args.Length > 7)
            {
                Console.WriteLine("Incorrect amount of arguments provided, exiting...");
                Environment.Exit(1);
            }

            SizeX = int.Parse(args[0]);
            SizeY = int.Parse(args[1]);
            XMin = double.Parse(args[2]);
            XMax = double.Parse(args[3]);
            YMin = double.Parse(args[4]);
            YMax = double.Parse(args[5]);
            Output = args.Length == 7 ? args[6] : "../../../out.png";
        }
    }
}
