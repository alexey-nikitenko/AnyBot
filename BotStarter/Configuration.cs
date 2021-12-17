using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BotStarter
{
    public class Configuration : IConfiguration
    {
        public Dictionary<string, int> GetLastAngles()
        {
            string fileName = "angleValues.json";
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string path = Path.Combine(solutiondir, fileName);

            string configJson = File.ReadAllText(path);

            var servoLastAngle = JObject.Parse(configJson).ToObject<Dictionary<string, int>>();

            return servoLastAngle;
        }

        public Dictionary<string, Dictionary<string, int>> GetCoordinates()
        {
            string fileName = "coordinates.json";
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string path = Path.Combine(solutiondir, fileName);

            var coordinates = JObject.Parse(path).ToObject<Dictionary<string, Dictionary<string, int>>>();

            return coordinates;
        }

        public void SaveLastAngle(int motorNbr, int angleValue)
        {
            string fileName = "angleValues.json";
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string path = Path.Combine(solutiondir, fileName);

            string configJson = File.ReadAllText(path);
            var jsonObj = JObject.Parse(configJson);
            jsonObj[motorNbr.ToString()] = angleValue;

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObj);
                file.Close();
            }
        }
    }
}
