using System;

namespace DG.Color.Colorblindness
{
    public class SimulationShift
    {
        private readonly double _x;
        private readonly double _y;
        private readonly double _m;
        private readonly double _yi;

        /// <summary>
        /// Coordinate
        /// </summary>
        public double X => _x;
        /// <summary>
        /// Coordinate
        /// </summary>
        public double Y => _y;
        /// <summary>
        /// Slope
        /// </summary>
        public double M => _m;
        /// <summary>
        /// Y-intercept
        /// </summary>
        public double Yi => _yi;

        public SimulationShift(double x, double y, double m, double yi)
        {
            _x = x;
            _y = y;
            _m = m;
            _yi = yi;
        }

        private static Lazy<SimulationShift> _protan = new Lazy<SimulationShift>(() => new SimulationShift(0.7465, 0.2535, 1.273463, -0.073894));
        private static Lazy<SimulationShift> _deutan = new Lazy<SimulationShift>(() => new SimulationShift(1.4, -0.4, 0.968437, 0.003331));
        private static Lazy<SimulationShift> _tritan = new Lazy<SimulationShift>(() => new SimulationShift(0.1748, 0, 0.062921, 0.292119));
        private static Lazy<SimulationShift> _custom = new Lazy<SimulationShift>(() => new SimulationShift(0.735, 0.265, -1.059259, 1.026914));

        public static SimulationShift Protan => _protan.Value;
        public static SimulationShift Deutan => _deutan.Value;
        public static SimulationShift Tritan => _tritan.Value;
        public static SimulationShift Custom => _custom.Value;

        public static SimulationShift For(ColorVisionDeficiency deficiency)
        {
            switch (deficiency)
            {
                case ColorVisionDeficiency.Protanomaly:
                case ColorVisionDeficiency.Protanopia:
                    return Protan;
                case ColorVisionDeficiency.Deuteranomaly:
                case ColorVisionDeficiency.Deuteranopia:
                    return Deutan;
                case ColorVisionDeficiency.Tritanomaly:
                case ColorVisionDeficiency.Tritanopia:
                    return Tritan;
            }
            throw new NotImplementedException($"{nameof(SimulationShift)} is not implemented for deviciency type {deficiency}.");
        }
    }
}
