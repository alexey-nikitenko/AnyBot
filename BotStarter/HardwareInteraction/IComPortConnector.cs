namespace BotStarter.HardwareInteraction
{
    public interface IComPortConnector
    {
        void RotateMotor(int motorIndex, int magicNumber);
    }
}