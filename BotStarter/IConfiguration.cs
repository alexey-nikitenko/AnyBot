
namespace BotStarter
{
    public interface IConfiguration
    {
        Dictionary<string, int> GetLastAngles();
        void SaveLastAngle(int motorNbr, int angleValue);
    }
}