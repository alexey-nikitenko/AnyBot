using BotStarter.Models;

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

                FindAndClickButton(coordinates.Where(x => x.Name.Equals("tab"))
                    .Select(i => i.Coordinates).FirstOrDefault());
                BackToMovablePosition();

                FindAndClickButton(coordinates.Where(x => x.Name.Equals("2"))
                    .Select(i => i.Coordinates).FirstOrDefault());
                BackToMovablePosition();
            }
        }

        public Dictionary<string, int> GetLastCoordinates()
        {
            return _configuration.GetLastAngles();
        }

        public void SaveCoordinates(CoordinatesModel coordinatesModel)
        {
            _configuration.SaveCoordinates(coordinatesModel);
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
