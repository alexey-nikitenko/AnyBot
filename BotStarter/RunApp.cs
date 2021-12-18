namespace BotStarter
{
    public class RunApp : IRunApp
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
            var coordinates = _configuration.GetCoordinates();

            while (true)
            {
                BackToMovablePosition();

                FindAndClickButton(coordinates["tab"]);
                BackToMovablePosition();

                FindAndClickButton(coordinates["2"]);
                BackToMovablePosition();
            }
        }

        public Dictionary<string, int> GetLastCoordinates()
        {
            return _configuration.GetLastAngles();
        }

        private void BackToMovablePosition()
        {
            _manipulator.ChangeMotorAngle(2, 325, 4);
        }

        private void FindAndClickButton(Dictionary<string, int> angleValuesByMotor)
        {
            int firstMotorGoalPosition = angleValuesByMotor["1"];
            int secondMotorGoalPosition = angleValuesByMotor["2"];
            int thirdMotorGoalPosition = angleValuesByMotor["3"];
            int forthMotorGoalPosition = angleValuesByMotor["4"];

            _manipulator.ChangeMotorAngle(4, forthMotorGoalPosition, 1);
            _manipulator.ChangeTwoMotorsAngleOneTime(1, 2, firstMotorGoalPosition, secondMotorGoalPosition, 4);

            PressAndRelease(thirdMotorGoalPosition);
        }

        private void PressAndRelease(int motorGoalPosition)
        {
            _manipulator.ChangeTwoMotorsAngleOneTime(2, 3, 310, motorGoalPosition, 40);
            _manipulator.ChangeMotorAngle(3, 400, 40);
        }

        public void MoveAndSave(int v, int value)
        {
            _manipulator.ChangeMotorAngle(v, value, 10);
        }
    }
}
