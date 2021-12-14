namespace BotStarter
{
    internal class RunApp : IRunApp
    {
        IManipulator _manipulator;
        IConfiguration _configuration;

        public RunApp(IManipulator manipulator, IConfiguration configuration)
        {
            _manipulator = manipulator;
            _configuration = configuration;
        }

        public void Run()
        {
            int firstMotorGoalPosition = 220;
            int secondMotorGoalPosition = 300;

            MoveTwoMotorsInParallel(firstMotorGoalPosition, secondMotorGoalPosition);
            //_manipulator.SmoothOneStepMoveRight(GetInitialPosition(4), 400);
        }

        private void MoveTwoMotorsInParallel(int firstMotorGoalPosition, int secondMotorGoalPosition)
        {
            int firstMotorInitialPosition = GetInitialPosition(1);
            int secondMotorInitialPosition = GetInitialPosition(2);

            int amountOfFirstMotorSteps = firstMotorGoalPosition - firstMotorInitialPosition;
            int amountOfSecondMotorSteps = secondMotorGoalPosition - secondMotorInitialPosition;

            int averageFirstMotorStep = 1;
            int averageSecondMotorStep = (amountOfFirstMotorSteps + amountOfSecondMotorSteps) / amountOfFirstMotorSteps;

            while (firstMotorInitialPosition < 220 && secondMotorInitialPosition < 300)
            {
                firstMotorInitialPosition = firstMotorInitialPosition + averageFirstMotorStep;
                _manipulator.MoveForwardByMotor(1, firstMotorInitialPosition);

                secondMotorInitialPosition = secondMotorInitialPosition + averageSecondMotorStep;
                _manipulator.MoveForwardByMotor(2, secondMotorInitialPosition);

                //Thread.Sleep(50);
            }
        }

        //ToDo get data from json
        private int GetInitialPosition(int motorNmr)
        {
            var angles = _configuration.GetLastAngles();

            return angles.Where(angle => angle.Key == motorNmr).FirstOrDefault().Value;
        }
    }
}
