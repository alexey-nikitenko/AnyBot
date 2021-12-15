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
            int firstMotorGoalPosition = 100;
            int secondMotorGoalPosition = 100;

            ChangeTwoMotorsAngle(firstMotorGoalPosition, secondMotorGoalPosition);
        }

        private void ChangeTwoMotorsAngle(int firstMotorGoalPosition, int secondMotorGoalPosition)

        {
            int firstMotorInitialPosition = GetInitialPosition(1);
            int secondMotorInitialPosition = GetInitialPosition(2);

            bool isFirstMoveForward = true;
            bool isSecondMoveForward = true;

            int amountOfFirstMotorSteps = firstMotorGoalPosition - firstMotorInitialPosition;
            int amountOfSecondMotorSteps = secondMotorGoalPosition - secondMotorInitialPosition;

            if (amountOfFirstMotorSteps < 0) isFirstMoveForward = false;
            if (amountOfSecondMotorSteps < 0) isSecondMoveForward = false;

            if (isFirstMoveForward && isSecondMoveForward)
            {
                while (firstMotorInitialPosition < firstMotorGoalPosition || secondMotorInitialPosition < secondMotorGoalPosition)
                {
                    if(firstMotorInitialPosition != firstMotorGoalPosition) 
                        firstMotorInitialPosition = MoveFirstForward(firstMotorInitialPosition);
                    if (secondMotorInitialPosition != secondMotorGoalPosition) 
                        secondMotorInitialPosition = MoveSecondForward(secondMotorInitialPosition);
                }
            }

            if (!isFirstMoveForward && isSecondMoveForward)
            {
                while (firstMotorInitialPosition > firstMotorGoalPosition || secondMotorInitialPosition < secondMotorGoalPosition)
                {
                    if (firstMotorInitialPosition != firstMotorGoalPosition)
                        firstMotorInitialPosition = MoveFirstBack(firstMotorInitialPosition);
                    if (secondMotorInitialPosition != secondMotorGoalPosition)
                        secondMotorInitialPosition = MoveSecondForward(secondMotorInitialPosition);
                }
            }

            if (isFirstMoveForward && !isSecondMoveForward)
            {
                while (firstMotorInitialPosition < firstMotorGoalPosition || secondMotorInitialPosition > secondMotorGoalPosition)
                {
                    if (firstMotorInitialPosition != firstMotorGoalPosition)
                        firstMotorInitialPosition = MoveFirstForward(firstMotorInitialPosition);
                    if (secondMotorInitialPosition != secondMotorGoalPosition)
                        secondMotorInitialPosition = MoveSecondBack(secondMotorInitialPosition);
                }
            }

            if (!isFirstMoveForward && !isSecondMoveForward)
            {
                while (firstMotorInitialPosition < firstMotorGoalPosition || secondMotorInitialPosition > secondMotorGoalPosition)
                {
                    if (firstMotorInitialPosition != firstMotorGoalPosition)
                        firstMotorInitialPosition = MoveFirstBack(firstMotorInitialPosition);
                    if (secondMotorInitialPosition != secondMotorGoalPosition)
                        secondMotorInitialPosition = MoveSecondBack(secondMotorInitialPosition);
                }
            }
        }

        private int MoveSecondBack(int secondMotorInitialPosition)
        {
            secondMotorInitialPosition = secondMotorInitialPosition - 1;
            MoveAndSave(2, secondMotorInitialPosition);
            return secondMotorInitialPosition;
        }

        private int MoveFirstBack(int firstMotorInitialPosition)
        {
            firstMotorInitialPosition = firstMotorInitialPosition - 1;
            MoveAndSave(1, firstMotorInitialPosition);
            return firstMotorInitialPosition;
        }

        private int MoveSecondForward(int secondMotorInitialPosition)
        {
            secondMotorInitialPosition = secondMotorInitialPosition + 1;
            MoveAndSave(2, secondMotorInitialPosition);
            return secondMotorInitialPosition;
        }

        private int MoveFirstForward(int firstMotorInitialPosition)
        {
            firstMotorInitialPosition = firstMotorInitialPosition + 1;
            MoveAndSave(1, firstMotorInitialPosition);
            return firstMotorInitialPosition;
        }

        private void MoveAndSave(int motorNbr, int motorInitialPosition)
        {
            _manipulator.MoveByMotor(motorNbr, motorInitialPosition);
            _configuration.SaveLastAngle(motorNbr, motorInitialPosition);
        }

        private int GetInitialPosition(int motorNmr)
        {
            var angles = _configuration.GetLastAngles();

            return angles.Where(angle => angle.Key.Equals(motorNmr.ToString())).FirstOrDefault().Value;
        }
    }
}
