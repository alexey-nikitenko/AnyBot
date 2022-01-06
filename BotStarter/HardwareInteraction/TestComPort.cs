namespace BotStarter.HardwareInteraction
{
    public class TestComPort : IComPortConnector
    {
        public void RotateMotor(int motorIndex, int magicNumber)
        {
            throw new NotImplementedException();
        }

        public void RotateMotor(int motorIndex, int initialNumber, int goalNumber, int delay)
        {
            Console.WriteLine($"Test {motorIndex} -- " + DateTime.Now.ToLongTimeString()) ;
        }
    }
}
