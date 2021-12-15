using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BotStarter
{
    public class Configuration : IConfiguration
    {
        public Dictionary<string, int> GetLastAngles()
        {
            var configJson = File.ReadAllText(@"..\..\..\angleValues.json");
            var servoLastAngle = JObject.Parse(configJson).ToObject<Dictionary<string, int>>();

            return servoLastAngle;
        }

        public void SaveLastAngle(int motorNbr, int angleValue)
        {
            var configJson = File.ReadAllText(@"..\..\..\angleValues.json");
            var jsonObj = JObject.Parse(configJson);
            jsonObj[motorNbr.ToString()] = angleValue;

            using (StreamWriter file = File.CreateText(@"..\..\..\angleValues.json"))

            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                jsonObj.WriteTo(writer);
            }
        }
    }
}
