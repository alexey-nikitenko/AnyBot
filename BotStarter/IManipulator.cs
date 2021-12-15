namespace BotStarter
{
    internal interface IManipulator
    {
        void MoveAndSave(int motorNmr, int steps);
        void ChangeTwoMotorsAngleOneTime(int firstMotorGoalPosition, int secondMotorGoalPosition);
    }
}