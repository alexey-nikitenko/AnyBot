namespace BotStarter.HardwareInteraction
{
    public interface IComPortConnector
    {
        void RotateMotor(int amount, int motorIndex, int initialNumber, int goalNumber, int delay);
        void RotateTwoMotors(int amount, int motorIndex1, int motorIndex2, int initialNumber, int goalNumber, int delay);
        void RotateThreeMotors(int amount, int motorIndex1, int motorIndex2, int motorIndex3, int initialNumber, int goalNumber, int delay);
    }
}