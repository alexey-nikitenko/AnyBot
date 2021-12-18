using BotStarter.Models;
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

        public List<CoordinatesModel> GetCoordinates()
        {
            string fileName = "coordinates.json";
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string path = Path.Combine(solutiondir, fileName);

            var coordinates = JObject.Parse(path).ToObject<List<CoordinatesModel>>();

            return coordinates;
        }

        public void SaveCoordinates(CoordinatesModel coordinatesModel)
        {
            string fileName = "coordinates.json";

            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string path = Path.Combine(solutiondir, fileName);

            string coordinatesJson = File.ReadAllText(path);

            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(coordinatesJson);

            foreach (var motorCoordinate in coordinatesModel.Coordinates)
            {
                if (jsonObj != null)
                {
                    jsonObj[coordinatesModel.Name] = coordinatesModel.Coordinates;
                }
                else
                {
                    jsonObj = new Dictionary<string, Dictionary<string,int>>();
                    jsonObj[coordinatesModel.Name] = coordinatesModel.Coordinates;
                }
            }

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObj);
                file.Close();
            }
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
