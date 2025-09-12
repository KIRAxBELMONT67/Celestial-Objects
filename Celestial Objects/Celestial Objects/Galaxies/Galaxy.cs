using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Celestial_Objects.Celestial_Objects.Galaxies
{
    public class Galaxy : ICelestialObject
    {
        public string Name { get; private set; }
        public string CelestialType { get; private set; }
        public double Age { get; private set; }

        public double Mass { get; private set; }

        public double Radius { get; private set; }

        public double Gravity { get; private set; }
        [JsonConstructor]
        public Galaxy(string name, string celestialType, double age, double mass, double radius, double gravity)
        {
            Name = name;
            CelestialType = celestialType;
            Age = age;
            Mass = mass;
            Radius = radius;
            Gravity = gravity;
        }
        public override string ToString()
        {
            return $"Galaxy Name: {Name}\nGalaxy Type: {CelestialType}\nAge: {Age} Billion Years\nMass: {Mass}Kg\nRadius: {Radius} Light Years\nGravity: {Gravity}m/s^2";
        }

    }
}
