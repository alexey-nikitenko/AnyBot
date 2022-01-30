namespace BotStarter.HardwareInteraction
{
    public interface IManipulator
    {
        void MoveByMotorWithSpeedSlow(int amount, int motorNmr, int initial, int goal, int speedSlow);
        void MoveTwoMotors(int amount, int motorNmr1, int motorNmr2, int initial, int goal, int speedSlow);
        void MoveThreeMotors(int amount, int motorNmr1, int motorNmr2, int motorNmr3, int initial, int goal, int speedSlow);
    }
}