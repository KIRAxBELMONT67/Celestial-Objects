using Celestial_Objects.Celestial_Objects.Galaxies;
using Celestial_Objects.Celestial_Objects.Planets;
using Celestial_Objects.Data_Saved;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Celestial_Objects.Celestial_Objects.CelestialObjectMerger
{
    public class CelestialsMerger
    {
        //This CelestialObjectsMethods property is important so we can use the same data from the first object we created, so we need this ctor so we can pass "this" in it
        CelestialObjectsMethods MethodCaller;
        public CelestialsMerger(CelestialObjectsMethods methodCaller)
        {
            MethodCaller = methodCaller;
        }
        //This is the method we call from CelestialObjectsMethods class to merge the celestial objects, it takes 2 objects, remove them from the list, then pass them to "Merge" method
        public void CreateAndAddMergedCelestial<T>(List<ICelestialObject> list, Func<string, string, double, double, double,double, T> createObject) where T : ICelestialObject
        {
            if (list.Count >= 2)
            {
                var firstCelestialObject = SelectCelestialToMerge<T>(list);
                list.Remove(firstCelestialObject);

                var secondCelestialObject = SelectCelestialToMerge<T>(list);
                list.Remove(secondCelestialObject);

                var mergedCelestial = Merge<T>(firstCelestialObject, secondCelestialObject, createObject);
                list.Add(mergedCelestial);
                DataSavingManager.Save<T>(list);
                Console.WriteLine($"\nThe New {MethodCaller.typeNamePair[typeof(T)]} Is: \n\n{mergedCelestial}");
                Console.WriteLine("\nPress Any Key To Continue");
                Console.ReadKey();
            }
            else if(typeof(T) == typeof(Galaxy)&& list.Count < 2)
            {
                Console.WriteLine($"You Need To Have At Least 2 Galaxies So You Can Merge");
            }
            else
            {
                Console.WriteLine($"You Need To Have At Least 2 {typeof(T).Name}s So You Can Merge");
            }

        }
        //This is the logic for Merging, it is not like we actually merge them physically, except for the gravity logic and formula, the new gravity is the actual surface gravity of the new theoretical celestial object
        private T Merge<T>(T celestialObject1, T celestialObject2, Func<string,string,double,double,double, double,T> createCelestialObject) where T : ICelestialObject
        {
            int firstHalfLength = celestialObject1.Name.Length / 2;
            string firstHalfName = celestialObject1.Name.Substring(0, firstHalfLength);
            int secondHalfLength = celestialObject2.Name.Length / 2;
            string secondHalfName = celestialObject2.Name.Substring(secondHalfLength);
            string firstHalfType = MergeType<T>(celestialObject1, true);
            string secondHalfType = MergeType<T>(celestialObject2, false);

            string mergedName = firstHalfName + secondHalfName;
            string mergedType = $"{firstHalfType.Trim()} {secondHalfType.Trim()}";
            double mergedAge = (celestialObject1.Age + celestialObject2.Age) / 2;
            double mergedMass = celestialObject1.Mass + celestialObject2.Mass;
            double mergedRadius = celestialObject1.Radius + celestialObject2.Radius;
            double mergedGravity = 0;
            //These are the formulas for the surface gravity, for galaxies, we must first convert from "Light Yerars" into "Meters"
            // For others, we just apply the formually normally
            if (typeof(T) == typeof(Galaxy))
            {
                double mergedRadiusInMeter = mergedRadius * 9.461e15;
                mergedGravity = (ICelestialObject.UNIVERSAL_GRAVITATIONAL_CONSTANT * mergedMass) / Math.Pow(mergedRadiusInMeter, 2);
            }
            else
            {
                mergedGravity = (ICelestialObject.UNIVERSAL_GRAVITATIONAL_CONSTANT * mergedMass) / Math.Pow(mergedRadius * 1000, 2);
            }
            return createCelestialObject(mergedName, mergedType, mergedAge, mergedMass, mergedRadius, mergedGravity);
        }
        //A method to show all the saved objetcs so you can choose from them
        private T SelectCelestialToMerge<T>(List<ICelestialObject> list)
        {
            double choice = 0;
            while (choice<1 || choice > list.Count)
            {
                MethodCaller.DisplayAllCelestialObjectSaved<T>();
                MethodCaller.CheckFormat(ref choice, $"Enter {typeof(T).Name} Number: ");
            }
            var celestialObject = list[(int)choice - 1];
            return (T)celestialObject;
        }
        //This method merge the types of them, my own custom logic
        private string MergeType<T>(T celestialObject, bool firstHalf) where T: ICelestialObject
        {
            int firstSpaceIndex = celestialObject.CelestialType.IndexOf(' ');
            if (firstSpaceIndex == -1 )
            {
                return celestialObject.CelestialType;
            }
            else if (firstHalf)
            {
                return celestialObject.CelestialType.Substring(0, firstSpaceIndex);
            }
            return celestialObject.CelestialType.Substring(firstSpaceIndex+1);
        }
    }
}
