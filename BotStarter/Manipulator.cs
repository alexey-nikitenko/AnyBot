namespace BotStarter
{
    internal class Manipulator : IManipulator
    {
        IComPortConnector _comPortConnector;

        public Manipulator(IComPortConnector comPortConnector)
        {
            _comPortConnector = comPortConnector;
        }

        public void MoveForward(int steps)
        {
            int half = steps / 2;

            _comPortConnector.RotateMotor(1, 100 + half);
            _comPortConnector.RotateMotor(2, 100 + half);
        }

        public void MoveForwardByMotor(int motorNmr, int steps)
        {
            _comPortConnector.RotateMotor(motorNmr, steps);
            Console.WriteLine($"Motor {motorNmr} step {steps}");
        }

        public void MoveBack()
        {
            _comPortConnector.RotateMotor(1, 100);
            _comPortConnector.RotateMotor(2, 100);
        }

        public void MoveLeft()
        {
            _comPortConnector.RotateMotor(1, 100);
            _comPortConnector.RotateMotor(2, 100);
        }

        public void MoveRight(int steps)
        {
            _comPortConnector.RotateMotor(4, steps);
        }

        public void SmoothOneStepMoveRight(int initialPosition, int steps)
        {
            if (initialPosition < steps)
            {
                while (initialPosition < steps)
                {
                    _comPortConnector.RotateMotor(4, ++initialPosition);
                    Console.WriteLine(initialPosition);
                }
            }
            else
            {
                while (initialPosition > steps)
                {
                    _comPortConnector.RotateMotor(4, --initialPosition);
                    Console.WriteLine(initialPosition);
                }
            }
            
        }
    }
}
