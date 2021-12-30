namespace BotStarter.HardwareInteraction
{
    public interface IManipulator
    {
        void MoveByMotorWithSpeedslow(int motorNmr, int initial, int goal, int speedSlow);
    }
}