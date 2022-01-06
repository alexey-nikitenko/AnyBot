using BotStarter.Models;

namespace BotStarter.Orders
{
    public interface IProcessOrder
    {
        void ClickAndBack(string button, int pause);
        Dictionary<string, int> GetLastCoordinates();
        void RunOrder(string order);
        void SaveCoordinates(CoordinatesModel coordinatesModel);
        void RunSkill(string skill, int timeOut = 0, int amountOfClicks = 1);
        void UseHealOrManaPot();
        bool MonsterInTarget();
        void MoveByMotorWithSpeedslow(int v1, int v2, int v3, int v4);
        void GoToCase();
        void MoveByMotor(int v, int value);
    }
}