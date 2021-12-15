namespace BotStarter
{
    internal class Manipulator : IManipulator
    {
        IComPortConnector _comPortConnector;
        IConfiguration _configuration;

        public Manipulator(IComPortConnector comPortConnector, IConfiguration configuration)
        {
            _comPortConnector = comPortConnector;
            _configuration = configuration;
        }

        private void MoveByMotor(int motorNmr, int steps)
        {
            _comPortConnector.RotateMotor(motorNmr, steps);
        }

        public void ChangeTwoMotorsAngleOneTime(int firstMotorGoalPosition, int secondMotorGoalPosition)
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
                    if (firstMotorInitialPosition != firstMotorGoalPosition)
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

        public void MoveAndSave(int motorNbr, int motorInitialPosition)
        {
            MoveByMotor(motorNbr, motorInitialPosition);
            _configuration.SaveLastAngle(motorNbr, motorInitialPosition);
        }

        private int GetInitialPosition(int motorNmr)
        {
            var angles = _configuration.GetLastAngles();

            return angles.Where(angle => angle.Key.Equals(motorNmr.ToString())).FirstOrDefault().Value;
        }
    }
}
