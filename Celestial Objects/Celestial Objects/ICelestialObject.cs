using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Celestial_Objects.Celestial_Objects
{
    public interface ICelestialObject
    {
        public const double UNIVERSAL_GRAVITATIONAL_CONSTANT = 6.6743e-11;
        public string Name { get; }
        public string CelestialType { get; }
        public double Age { get; }
        public double Mass { get; }
        public double Radius { get; }
        public double Gravity { get; }
    }
}
