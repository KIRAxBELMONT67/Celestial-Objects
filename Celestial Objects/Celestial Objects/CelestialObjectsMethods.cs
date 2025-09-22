using Celestial_Objects.Celestial_Objects.CelestialObjectMerger;
using Celestial_Objects.Celestial_Objects.Galaxies;
using Celestial_Objects.Celestial_Objects.Planets;
using Celestial_Objects.Celestial_Objects.Stars;
using Celestial_Objects.Data_Saved;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace Celestial_Objects.Celestial_Objects
{
   
    public class CelestialObjectsMethods
    {
        // This object is to call the merger
        CelestialsMerger celestialsMerger;
        
        public CelestialObjectsMethods()
        {
            celestialsMerger = new CelestialsMerger(this);
        }
        //This dictionary contains the list of each type with their current saved objects
        private Dictionary<Type, List<ICelestialObject>> TypeCelestialObjectsPair = new Dictionary<Type, List<ICelestialObject>>()
        {
            [typeof(Planet)] = new List<ICelestialObject>(),
            [typeof(Star)] = new List<ICelestialObject>(),
            [typeof(Galaxy)] = new List<ICelestialObject>()
        };
        //This dictionary is for the types, whenever you create a new class (Type), you can just assign the new class type here with the name so it can be printed anytime
        public Dictionary<Type, string> typeNamePair = new Dictionary<Type, string>()
        {
            [typeof(Planet)] = "Planet",
            [typeof(Star)] = "Star",
            [typeof(Galaxy)] = "Galaxy",
            //[typeof(BlackHole)] = "Black Hole"
        };
        //A List containing the types of the planets, just add a new one and enjoy
        private readonly List<string> _planetTypes = new List<string>()
        {
            "Rocky",
            "Gas Giant",
            "Gas Dwarf",
            "Hycean"
        };
        //Same but stars
        private readonly List<string> _starsTypes = new List<string>()
        {
            "Neutron Star",
            "Red Dwarf",
            "White Dwarf",
            "Protostar",
        };
        //Same but galaxies
        private readonly List<string> _galaxiesTypes = new List<string>()
        {
            "Spiral",
            "Barred Spiral",
            "Elliptical",
            "Irregular",
            "Lenticular",
            "Peculiar",
        };
        //This is the main method that runs
        public void AskWhatUserWantToDo()
        {
            //Here we assign the number of types we have added, now we have 3, Planets, Stars, and Galaxies, but if we added more, we need to make it equal to that number
            int typesCount = 3;
            //Loading each class's list from the JSON files saved, if no file is there, we just assign the lists to an empty ones
            TypeCelestialObjectsPair[typeof(Planet)] = DataSavingManager.Load<Planet>().Cast<ICelestialObject>().ToList();
            TypeCelestialObjectsPair[typeof(Star)] = DataSavingManager.Load<Star>().Cast<ICelestialObject>().ToList();
            TypeCelestialObjectsPair[typeof(Galaxy)] = DataSavingManager.Load<Galaxy>().Cast<ICelestialObject>().ToList();

            bool isFinished = false;
            while (!isFinished)
            {
                double choice = -1;
                double innerChoice = 0;

              
                Console.WriteLine("\nPress 0 To See All Celestial Objects Saved, Or Anything Else To Do More Stuff");
                Console.WriteLine("1. Add Celestial Object");
                Console.WriteLine("2. Merge Celestial Object");
                Console.WriteLine("3. Delete All Celestial Objects For A Type");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    
                }
                if (choice == 0)
                {
                    break;
                }
                switch (choice)
                {
                    case 1:
                        while (innerChoice < 1 || innerChoice > typesCount)
                        {
                            Console.WriteLine("Choose What You Would Like To Do:");
                            Console.WriteLine("1. Add New Planet");
                            Console.WriteLine("2. Add New Star");
                            Console.WriteLine("3. Add New Galaxy");
                            try
                            {
                                innerChoice = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                innerChoice = 0;
                            }
                        }



                        switch (innerChoice)
                        {
                            case 1:
                                AssignCelestialObjectInfo(TypeCelestialObjectsPair[typeof(Planet)], _planetTypes, CreateNewPlanet);
                                break;
                            case 2:
                                AssignCelestialObjectInfo(TypeCelestialObjectsPair[typeof(Star)], _starsTypes, CreateNewStar);
                                break;
                            case 3:
                                AssignCelestialObjectInfo(TypeCelestialObjectsPair[typeof(Galaxy)], _galaxiesTypes, CreateNewGalaxy);
                                break;


                        }
                        break;

                    case 2:
                        while (innerChoice < 1 || innerChoice > typesCount)
                        {
                            CheckFormat(ref innerChoice, "Choose What You Would Like To Merge:\n1. Merge Planets\n2. Merge Stars\n3. Merge Galaxies");
                        }
                        switch (innerChoice)
                        {
                            //Here we call the merger object to use the merge method
                            case 1:
                                celestialsMerger.CreateAndAddMergedCelestial<Planet>(TypeCelestialObjectsPair[typeof(Planet)], CreateNewPlanet);
                                break;
                            case 2:
                                celestialsMerger.CreateAndAddMergedCelestial<Star>(TypeCelestialObjectsPair[typeof(Star)], CreateNewStar);
                                break;
                            case 3: celestialsMerger.CreateAndAddMergedCelestial<Galaxy>(TypeCelestialObjectsPair[typeof(Galaxy)], CreateNewGalaxy);
                                break;

                        }
                        break;
                    case 3:
                        choice = -1;
                        //This is why adding the number of types is important, we need also to assign them here
                        while (choice <1 || choice > typesCount)
                        {
                            CheckFormat(ref choice, "Enter Celestial Object Type: \n1. Planet\n2. Star\n3. Galaxy");
                        }
                        Console.WriteLine("The List Has Been Successfully Removed!");
               
                        switch (choice)
                        {
                            case 1:
                                TypeCelestialObjectsPair[typeof(Planet)].Clear();
                                DataSavingManager.Save<Planet>(TypeCelestialObjectsPair[typeof(Planet)]);
                                break;
                            case 2:
                                TypeCelestialObjectsPair[typeof(Star)].Clear();
                                DataSavingManager.Save<Star>(TypeCelestialObjectsPair[typeof(Star)]);
                                break;
                            case 3:
                                TypeCelestialObjectsPair[typeof(Galaxy)].Clear();
                                DataSavingManager.Save<Galaxy>(TypeCelestialObjectsPair[typeof(Galaxy)]);
                                break;
                        }
                        break;
                }
            }
        }

        public void AssignCelestialObjectInfo<T>(List<ICelestialObject> listOfTheObject, List<string> listOfTypes, Func<string,string,double,double,double, double, T> createObject) where T:ICelestialObject
        {
            string typeName = typeNamePair[typeof(T)];
            string nameCelestialObject = null, celestialType = null;
            double age=0, mass=0, radius=0, gravity = 0;
            double typeChoice = 0;
            bool isNotDistinctName = true;
            //This loop will make sure that the name is valid
            while (string.IsNullOrWhiteSpace(nameCelestialObject) || !nameCelestialObject.Any(char.IsLetter) || isNotDistinctName)
            {
                Console.WriteLine($"Enter {typeName} Name: ");

                nameCelestialObject = Console.ReadLine();
                isNotDistinctName = listOfTheObject.Any(someCelestialObject => someCelestialObject.Name == nameCelestialObject);
                try
                {
                    if (!nameCelestialObject.Any(char.IsLetter))
                    {
                        Console.WriteLine("Name Must Have At Least 1 Character");
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Name Must Not Be Null\n");
                }
           
                if(isNotDistinctName)
                {
                    Console.WriteLine("Name Must Be Unique");
                }
            }
            nameCelestialObject = nameCelestialObject.Trim();
            while (typeChoice <= 0 || typeChoice > listOfTypes.Count)
            {
                Console.WriteLine("");
                DisplayAllTypesForCelestialObject(listOfTypes);
                CheckFormat(ref typeChoice, $"\nEnter {typeName} Type:");
            }

            celestialType = listOfTypes[(int)typeChoice - 1];
            CheckFormat(ref age, $"Enter {typeName} Age (Billions): ");
            CheckFormat(ref mass, $"Enter {typeName} Mass (Kg): ");

            if (typeof(T) == typeof(Galaxy))
            {
                CheckFormat(ref radius, $"Enter {typeName} Radius (Light Years): ");
            }
            else
            {
                CheckFormat(ref radius, $"Enter {typeName} Radius (Km): ");
            }

            if (typeof(T) == typeof(Galaxy))
            {
                double RadiusInMeter = radius * 9.461e15;
                gravity = (ICelestialObject.UNIVERSAL_GRAVITATIONAL_CONSTANT * mass) / Math.Pow(RadiusInMeter, 2);
            }
            else
            {
                gravity = (ICelestialObject.UNIVERSAL_GRAVITATIONAL_CONSTANT * mass) / Math.Pow(radius * 1000, 2);
            }
            var newCelestialObject = createObject(nameCelestialObject, celestialType, age, mass, radius, gravity);
            listOfTheObject.Add(newCelestialObject);
            DataSavingManager.Save<T>(listOfTheObject);
            Console.WriteLine($"\nThe {typeName} Has Been Added Successfully!");
            Console.WriteLine($"\nThe New {typeName} Is: \n\n{newCelestialObject}");
            Console.WriteLine("\nPress Any Key To Continue");
            Console.ReadKey();

        }
        //These methods are important so we can pass it in the Func<>, so whenever we create a new class, (Black Hole), we need to create a method for it here
        private Planet CreateNewPlanet(string name,string planetType, double age, double mass, double radius, double gravity)
        {
            return new Planet(name, planetType, age, mass, radius, gravity);
        }
        private Star CreateNewStar(string name, string starType, double age, double mass, double radius, double gravity)
        {
            return new Star(name, starType, age, mass, radius, gravity);
        }
        private Galaxy CreateNewGalaxy(string name, string galaxyType, double age, double mass, double radius, double gravity)
        {
            return new Galaxy(name, galaxyType, age, mass, radius, gravity);
        }
        public void DisplayAllCelestialObjectSaved<T>()
        {
            if (TypeCelestialObjectsPair[typeof(T)].Count == 0)
            {
                Console.WriteLine($"There Is No {typeNamePair[typeof(T)]} Saved In The List");
            }
            else
            {
                int i = 1;
                foreach (T item in TypeCelestialObjectsPair[typeof(T)])
                {
                    Console.WriteLine($"{i}.\n{item}");
                    i++;
                }
            }
        
        }
        private void DisplayAllTypesForCelestialObject(List<string> listOfTypes)
        {
            int i = 1;
            foreach (var type in listOfTypes)
            {
                Console.WriteLine($"{i}-{type}");
                i++;
            }
        }

        //This method makes sure that we enter a valid number with no errors
        public bool CheckFormat(ref double numberToCheck, string messageToPrint)
        {
            bool isInFormat = false;
            while (isInFormat == false)
            {
                try
                {
                    Console.WriteLine(messageToPrint);
                    numberToCheck = Convert.ToDouble(Console.ReadLine());
                }
                catch (FormatException)
                {

                }
                if (numberToCheck > 0)
                {
                    isInFormat = true;
                }

            }
            return true;
        }
    }
}
