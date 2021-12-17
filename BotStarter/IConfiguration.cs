
namespace BotStarter
{
    public interface IConfiguration
    {
        Dictionary<string, int> GetLastAngles();
        Dictionary<string, Dictionary<string, int>> GetCoordinates();
        void SaveLastAngle(int motorNbr, int angleValue);
    }
}