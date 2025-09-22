Welcome, Space Citizen! 🪐

I made this program to practice a course I am taking on C#.

The program is about adding, or merging celestial objects. When You add, you will have to enter the name, age, mass, radius, and with these properties, the program will also calculate the actual surface gravity, for example, if you put the properties of earth, you will get g = 9.8m/s^2
The first interface has 3 choices, adding, merging, or deleting all items.
I made a save and load methods so no need to worry about all your stuff getting lost.

The first class, which has Main method, has only few codes to execute the actual program.
I currently added only 3 types, Planet, Star, Galaxy. Each new type added must implement the ICelestialObject interface, and must have a ToString() method to print all it's values.
Each new type also must have a list of it's own types, for example, a Planet type has Rocky, and Gas Giant, so we need to have for example a private readonly List<string> "moon_Types" = new List<string>(). You can find all the lists starting from line 45.
After creating the type you want, you must add a new item in the dictionary called TypeCelestialObjectsPair, which takes the type as a key and get the list it has, so for example we must do this: [typeof(Moon)] = new List<ICelestialObject>(). You can find it in line 30
You also need to add the new type to the dictionary called typeNamePair, you can find it in line 37.
When you add the new type (Moon), you also have to load it in line 76.

In line 87, you can find the first console interface, if you wanna add new functionality, you can add the choices there.
After creating the new type you want (Moon), you must go to line 108 and add option of it.  Console.WriteLine("4. Add New Moon");
Then you must go to line 124, where you can find all the switch cases for the choices. You just need to add a new case, put it in  AssignCelestialObjectInfo method, which takes the TypeCelestialObjectsPair dictionary, you put the key for the type you want in it, you must also put the list of the types this celestial object has, and finally a method that returns a new object of the type you want. You must create the method for your new type. For better readabilty, you can put next to the CreateNew(Type) methods, you can find them starting from line 259.

In line 145, you can find the merge cases, you just need to add a new case and use celestialsMerger object to call CreateAndAddMergedCelestial method, and put TypeCelestialObjectsPair dictionary in the argument, and the method that create your new type.
In line 168, you can find the switch cases for the deletion, you just need to add the same stuff but for you own type, and edit line 164 to add the new type as an option.

These are the only things you need to edit so your new type can work properly.

I tried to make all the methods Generic<T>, for better scalability. I also made sure that all inputs must be correct, so you can't assign a negative nor 0 mass, radius, age, and the name also must be unique, and has atleast 1 character. You can find the logic for the name in line 198 and below, and for the other numerics in a method called CheckFormat, in line 299.

Enjoy, Space Citizen! 🛸
