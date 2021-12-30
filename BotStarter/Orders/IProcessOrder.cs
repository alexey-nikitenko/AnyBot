using BotStarter.Models;

namespace BotStarter.Orders
{
    public interface IProcessOrder
    {
        void BackToMovablePosition();
        void BreakClickProcess();
        void ClickAndBack(string button, int pause);
        void ClickAndBackConstantly(string button, bool clickingContinue, int pause = 0);
        void FindAndClickButton(Dictionary<string, int> angleValuesByMotor, int pause);
        void Follow();
        Dictionary<string, int> GetLastCoordinates();
        void LeftClick();
        void RightClick();
        void RunOrder(string order);
        void SaveCoordinates(CoordinatesModel coordinatesModel);
        void ThreadProc();
        void RunSkill(string skill, int timeOut = 0, int amountOfClicks = 1);
        void UseHealOrManaPot();
        bool MonsterInTarget();
        void MoveByMotorWithSpeedslow(int v1, int v2, int v3, int v4);
        void ManipulatorClick(int thirdMotorInitialPosition, int thirdMotorGoalPosition, int pause);
        void GoToCase();
    }
}