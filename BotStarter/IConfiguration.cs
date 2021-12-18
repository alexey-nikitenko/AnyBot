
using BotStarter.Models;

namespace BotStarter
{
    public interface IConfiguration
    {
        Dictionary<string, int> GetLastAngles();
        List<CoordinatesModel> GetCoordinates();
        void SaveCoordinates(CoordinatesModel coordinatesModel);
        void SaveLastAngle(int motorNbr, int angleValue);
    }
}