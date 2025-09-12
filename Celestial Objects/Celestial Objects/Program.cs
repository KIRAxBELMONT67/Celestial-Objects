using Celestial_Objects.Celestial_Objects;
using Celestial_Objects.Celestial_Objects.Galaxies;
using Celestial_Objects.Celestial_Objects.Planets;
using Celestial_Objects.Celestial_Objects.Stars;
using Celestial_Objects.Data_Saved;
using System.Security.AccessControl;

namespace Celestial_Objects
{
    public class Program
    {
        static void Main(string[] args)
        {
            CelestialObjectsMethods celestialObjectsMethods = new CelestialObjectsMethods();
            Console.WriteLine("Welcome, Space Citizen!");
            Console.WriteLine("Choose What You Would Like To Do:");
            celestialObjectsMethods.AskWhatUserWantToDo();
            celestialObjectsMethods.DisplayAllCelestialObjectSaved<Planet>();
            celestialObjectsMethods.DisplayAllCelestialObjectSaved<Star>();
            celestialObjectsMethods.DisplayAllCelestialObjectSaved<Galaxy>();
            Console.ReadLine();
        }
    }
}
