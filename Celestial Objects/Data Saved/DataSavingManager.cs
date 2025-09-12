using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Celestial_Objects.Celestial_Objects;
using Celestial_Objects.Celestial_Objects.Planets;

namespace Celestial_Objects.Data_Saved;

//This class handels the data saved and loaded
public class DataSavingManager
{
    //The method saves the list provided in a JSON format then write it in a JSON file
    public static void Save<T>(List<ICelestialObject> celestialObjectsToAdd)
    {
        string typeName = typeof(T).Name;
        string filePath = $"{typeName}sSaved.json";
        string uppdatedJson = JsonSerializer.Serialize(celestialObjectsToAdd);
        File.WriteAllText(filePath, uppdatedJson);
    }
    //This method loads the saved JSON list data, and returning it so we can assign it to the current list when we first open the program
    public static List<T> Load<T>()
    {
        string filePath = $"{typeof(T).Name}sSaved.json";
        if (!File.Exists(filePath))
        {
            return new List<T>();
        }
        string currentJsonList = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(currentJsonList) ?? new List<T>();
    }
}
