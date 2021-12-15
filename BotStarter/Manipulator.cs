namespace BotStarter
{
    internal class Manipulator : IManipulator
    {
        IComPortConnector _comPortConnector;

        public Manipulator(IComPortConnector comPortConnector)
        {
            _comPortConnector = comPortConnector;
        }

        public void MoveByMotor(int motorNmr, int steps)
        {
            _comPortConnector.RotateMotor(motorNmr, steps);
        }
    }
}
