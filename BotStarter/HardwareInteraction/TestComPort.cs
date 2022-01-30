namespace BotStarter.HardwareInteraction
{
    public class TestComPort : IComPortConnector
    {
        public void RotateMotor(int amount, int motorIndex, int initialNumber, int goalNumber, int delay)
        {
            Console.WriteLine($"Test {motorIndex} -- " + DateTime.Now.ToLongTimeString());
        }

        public void RotateTwoMotors(int amount, int motorIndex1, int motorIndex2, int initialNumber, int goalNumber, int delay)
        {
            Console.WriteLine($"Test {motorIndex2} -- " + DateTime.Now.ToLongTimeString());
        }

        public void RotateThreeMotors(int amount, int motorIndex1, int motorIndex2, int motorIndex3, int initialNumber, int goalNumber, int delay)
        {
            Console.WriteLine($"Test {motorIndex3} -- " + DateTime.Now.ToLongTimeString());
        }
    }
}
