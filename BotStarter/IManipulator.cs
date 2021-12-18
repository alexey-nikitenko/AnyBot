namespace BotStarter
{
    public interface IManipulator
    {
        void MoveAndSave(int motorNmr, int steps);
        void ChangeTwoMotorsAngleOneTime(int firstMotorNbr, int secondMotorNmb, int firstMotorGoalPosition, 
            int secondMotorGoalPosition, int acceleration);
        void ChangeMotorAngle(int motorNbr, int motorGoalPosition, int acceleration);
    }
}