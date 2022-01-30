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

        public void MoveByMotorWithSpeedSlow(int amount, int motorNmr, int initial, int goal, int speedSlow)
        {
            //_comPortConnector = new TestComPort();
            _comPortConnector.RotateMotor(amount, motorNmr, initial, goal, speedSlow);
            _configuration.SaveLastAngle(motorNmr, goal);
        }

        public void MoveTwoMotors(int amount, int motorNmr1, int motorNmr2, int initial, int goal, int speedSlow)
        {
            //_comPortConnector = new TestComPort();
            _comPortConnector.RotateTwoMotors(amount, motorNmr1, motorNmr2, initial, goal, speedSlow);
            _configuration.SaveLastAngle(motorNmr1, goal);
            _configuration.SaveLastAngle(motorNmr2, goal);
        }

        public void MoveThreeMotors(int amount, int motorNmr1, int motorNmr2, int motorNmr3, int initial, int goal, int speedSlow)
        {
            _comPortConnector.RotateThreeMotors(amount, motorNmr1, motorNmr2, motorNmr3, initial, goal, speedSlow);
            _configuration.SaveLastAngle(motorNmr1, goal);
            _configuration.SaveLastAngle(motorNmr2, goal);
            _configuration.SaveLastAngle(motorNmr3, initial);
        }
    }
}
