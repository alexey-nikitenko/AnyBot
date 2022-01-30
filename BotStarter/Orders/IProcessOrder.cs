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
        void MoveByMotorWithSpeedSlow(int amount, int motorNumber, int initial, int goal, int delay);
        void MoveByMotorWithSpeedSlow(int amount, int motorNumber1, int motorNumber2, int initial, int goal, int delay);
        void MoveByMotorWithSpeedSlow(int amount, int motorNumber1, int motorNumber2, int motorNumber3, int initial, int goal, int delay);
        void GoToCase();
        void MoveByMotor(int v, int value);
    }
}