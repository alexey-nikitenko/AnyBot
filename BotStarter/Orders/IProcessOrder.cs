using BotStarter.Models;

namespace BotStarter.Orders
{
    public interface IProcessOrder
    {
        void BackToMovablePosition();
        void BreakClickProcess();
        void ClickAndBack(string button, int pause = 0);
        void ClickAndBackConstantly(string button, bool clickingContinue, int pause = 0);
        void FindAndClickButton(Dictionary<string, int> angleValuesByMotor, int pause = 0);
        void Follow();
        Dictionary<string, int> GetLastCoordinates();
        void LeftClick();
        void MoveAndSave(int v, int value);
        void PressAndRelease(int motorGoalPosition, int pause = 0);
        void RightClick();
        void RunOrder(string order);
        void SaveCoordinates(CoordinatesModel coordinatesModel);
        void ThreadProc();
        void RunSkill(string skill, int timeOut = 0, int amountOfClicks = 1);
        void UseHealOrManaPot();
        bool MonsterInTarget();
    }
}