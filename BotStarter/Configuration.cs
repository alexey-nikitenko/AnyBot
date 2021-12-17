using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BotStarter
{
    public class Configuration : IConfiguration
    {
        public Dictionary<string, int> GetLastAngles()
        {
            string configJson = File.ReadAllText(@"..\..\..\angleValues.json");
            var servoLastAngle = JObject.Parse(configJson).ToObject<Dictionary<string, int>>();

            return servoLastAngle;
        }

        public Dictionary<string, Dictionary<string, int>> GetCoordinates()
        {
            string coordinatesJson = File.ReadAllText(@"..\..\..\coordinates.json");
            var coordinates = JObject.Parse(coordinatesJson).ToObject<Dictionary<string, Dictionary<string, int>>>();

            return coordinates;
        }

        public void SaveLastAngle(int motorNbr, int angleValue)
        {
            string configJson = File.ReadAllText(@"..\..\..\angleValues.json");
            var jsonObj = JObject.Parse(configJson);
            jsonObj[motorNbr.ToString()] = angleValue;

            using (StreamWriter file = File.CreateText(@"..\..\..\angleValues.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObj);
                file.Close();
            }
        }
    }
}
