
using BotStarter.Models;

namespace BotStarter
{
    public interface IConfiguration
    {
        Dictionary<string, int> GetLastAngles();
        Dictionary<string, Dictionary<string, int>> GetCoordinates();
        void SaveCoordinates(CoordinatesModel coordinatesModel);
        void SaveLastAngle(int motorNbr, int angleValue);
    }
}