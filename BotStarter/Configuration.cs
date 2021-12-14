using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace BotStarter
{
    public class Configuration : IConfiguration
    {
        public Dictionary<int, int> GetLastAngles()
        {
            var configJson = File.ReadAllText(@"..\..\..\config.json");
            var servoLastAngle = JObject.Parse(configJson)["servoLastAngle"].ToObject<Dictionary<int, int>>();

            return servoLastAngle;
        }
    }
}
