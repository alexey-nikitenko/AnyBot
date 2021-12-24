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

        private void MoveByMotor(int motorNmr, int steps)
        {
            _comPortConnector.RotateMotor(motorNmr, steps);
        }

        public void ChangeTwoMotorsAngleOneTime(int firstMotorNbr, int secondMotorNbr, int firstMotorGoalPosition, 
            int secondMotorGoalPosition, int acceleration)
        {
            int firstMotorInitialPosition = GetInitialPosition(firstMotorNbr);
            int secondMotorInitialPosition = GetInitialPosition(secondMotorNbr);

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
                    if (firstMotorInitialPosition <= firstMotorGoalPosition)
                    {
                        if (firstMotorInitialPosition + acceleration > firstMotorGoalPosition && firstMotorNbr != 3)
                        {
                            acceleration = 1;
                        }

                        firstMotorInitialPosition = MoveForward(firstMotorNbr, firstMotorInitialPosition, acceleration);
                    }

                    if (secondMotorInitialPosition <= secondMotorGoalPosition)
                    {
                        if (secondMotorInitialPosition + acceleration > secondMotorGoalPosition && secondMotorNbr != 3)
                        {
                            acceleration = 1;
                        }

                        secondMotorInitialPosition = MoveForward(secondMotorNbr, secondMotorInitialPosition, acceleration);
                    }
                }
            }

            if (!isFirstMoveForward && isSecondMoveForward)
            {
                while (firstMotorInitialPosition > firstMotorGoalPosition || secondMotorInitialPosition < secondMotorGoalPosition)
                {
                    if (firstMotorInitialPosition >= firstMotorGoalPosition)
                    {
                        if (firstMotorInitialPosition - acceleration < firstMotorGoalPosition && firstMotorNbr != 3)
                        {
                            acceleration = 1;
                        }

                        firstMotorInitialPosition = MoveBack(firstMotorNbr, firstMotorInitialPosition, acceleration);
                    }

                    if (secondMotorInitialPosition <= secondMotorGoalPosition)
                    {
                        if (secondMotorInitialPosition + acceleration > secondMotorGoalPosition && secondMotorNbr != 3)
                        {
                            acceleration = 1;
                        }

                        secondMotorInitialPosition = MoveForward(secondMotorNbr, secondMotorInitialPosition, acceleration);
                    }
                }
            }

            if (isFirstMoveForward && !isSecondMoveForward)
            {
                while (firstMotorInitialPosition < firstMotorGoalPosition || secondMotorInitialPosition > secondMotorGoalPosition)
                {
                    if (firstMotorInitialPosition <= firstMotorGoalPosition)
                    {
                        if (firstMotorInitialPosition + acceleration > firstMotorGoalPosition && firstMotorNbr != 3)
                        {
                            acceleration = 1;
                        }

                        firstMotorInitialPosition = MoveForward(firstMotorNbr, firstMotorInitialPosition, acceleration);
                    }

                    if (secondMotorInitialPosition >= secondMotorGoalPosition)
                    {
                        if (secondMotorInitialPosition - acceleration < secondMotorGoalPosition && secondMotorNbr != 3)
                        {
                            acceleration = 1;
                        }

                        secondMotorInitialPosition = MoveBack(secondMotorNbr, secondMotorInitialPosition, acceleration);
                    }
                        
                }
            }

            if (!isFirstMoveForward && !isSecondMoveForward)
            {
                while (firstMotorInitialPosition < firstMotorGoalPosition || secondMotorInitialPosition > secondMotorGoalPosition)
                {
                    if (firstMotorInitialPosition >= firstMotorGoalPosition)
                    {
                        if (firstMotorInitialPosition - acceleration < firstMotorGoalPosition && firstMotorNbr != 3)
                        {
                            acceleration = 1;
                        }

                        firstMotorInitialPosition = MoveBack(firstMotorNbr, firstMotorInitialPosition, acceleration);
                    }
                    if (secondMotorInitialPosition >= secondMotorGoalPosition)
                    {
                        if (secondMotorInitialPosition - acceleration < secondMotorGoalPosition && secondMotorNbr != 3)
                        {
                            acceleration = 1;
                        }

                        secondMotorInitialPosition = MoveBack(secondMotorNbr, secondMotorInitialPosition, acceleration);
                    }
                }
            }
        }

        public void ChangeMotorAngle(int motorNbr, int motorGoalPosition, int acceleration)
        {
            int motorInitialPosition = GetInitialPosition(motorNbr);

            bool isFirstMoveForward = true;

            int amountOfFirstMotorSteps = motorGoalPosition - motorInitialPosition;

            if (amountOfFirstMotorSteps < 0) isFirstMoveForward = false;

            if (isFirstMoveForward)
            {
                while (motorInitialPosition < motorGoalPosition)
                {
                    if (motorInitialPosition + acceleration > motorGoalPosition && motorNbr != 3) 
                        acceleration = 1;

                    if (motorInitialPosition <= motorGoalPosition)
                        motorInitialPosition = MoveForward(motorNbr, motorInitialPosition, acceleration);
                }
            }

            if (!isFirstMoveForward)
            {
                while (motorInitialPosition > motorGoalPosition)
                {
                    if (motorInitialPosition - acceleration < motorGoalPosition && motorNbr != 3)
                        acceleration = 1;

                    if (motorInitialPosition >= motorGoalPosition)
                        motorInitialPosition = MoveBack(motorNbr, motorInitialPosition, acceleration);
                }
            }
        }

        private int MoveBack(int motorNbr, int motorInitialPosition, int acceleration)
        {
            motorInitialPosition = motorInitialPosition - acceleration;
            MoveAndSave(motorNbr, motorInitialPosition);
            Console.WriteLine(motorInitialPosition);
            return motorInitialPosition;
        }
        private int MoveForward(int motorNbr, int motorInitialPosition, int acceleration)
        {
            motorInitialPosition = motorInitialPosition + acceleration;
            MoveAndSave(motorNbr, motorInitialPosition);
            Console.WriteLine(motorInitialPosition);
            return motorInitialPosition;
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
