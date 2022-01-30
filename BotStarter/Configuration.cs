using BotStarter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static BotStarter.Constants;

namespace BotStarter
{
    public class Configuration : IConfiguration
    {
        public Dictionary<string, int> GetLastAngles()
        {
            string path = Path.Combine(solutiondir, angleValuesFile);
            string configJson = File.ReadAllText(path);

            var servoLastAngle = JObject.Parse(configJson).ToObject<Dictionary<string, int>>();

            return servoLastAngle;
        }

        public Dictionary<string, Dictionary<string, int>> GetCoordinates()
        {
            string path = Path.Combine(solutiondir, coordinatesFile);
            string coordinatesJson = File.ReadAllText(path);

            var coordinates = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(coordinatesJson);

            return coordinates;
        }

        public Dictionary<string, string> GetParameters()
        {
            string path = Path.Combine(solutiondir, parametersFile);
            string parametersJson = File.ReadAllText(path);

            var parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(parametersJson);

            return parameters;
        }

        public void SaveCoordinates(CoordinatesModel coordinatesModel)
        {
            string path = Path.Combine(solutiondir, coordinatesFile);
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
            string path = Path.Combine(solutiondir, angleValuesFile);
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
