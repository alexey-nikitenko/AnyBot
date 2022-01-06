namespace BotStarter.HardwareInteraction
{
    public class Manipulator : IManipulator
    {
        IComPortConnector _comPortConnector;
        IConfiguration _configuration;

        public Manipulator(IComPortConnector comPortConnector, IConfiguration configuration)
        {
            _comPortConnector = comPortConnector;
            _configuration = configuration;
        }

        public void MoveByMotorWithSpeedslow(int motorNmr, int initial, int goal, int speedSlow)
        {
            //_comPortConnector = new TestComPort();
            _comPortConnector.RotateMotor(motorNmr, initial, goal, speedSlow);
            _configuration.SaveLastAngle(motorNmr, goal);
        }
    }
}
