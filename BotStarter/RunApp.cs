using BotStarter.HardwareInteraction;
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
            long lastMannaCheckedTime = DateTime.Now.Ticks;
            long passiveSkillCheckedTime1 = DateTime.Now.Ticks;

            while (true)
            {
                BackToMovablePosition();

                ClickAndBack(coordinates, "7");
                //ClickAndBack(coordinates, "tab");
                BackToMovablePosition();

                //Thread.Sleep(500);
                //ClickAndBack(coordinates, "2");
                //Thread.Sleep(5000);
                //ClickAndBack(coordinates, "tab");
                //Thread.Sleep(500);
                //BackToMovablePosition();
                //ClickAndBack(coordinates, "2");
                //Thread.Sleep(5000);

                //for (int i = 0; i < 4; i++)
                //{
                //    ClickAndBack(coordinates, "4");
                //    Thread.Sleep(500);
                //}

                //if (DateTime.Now.Ticks - lastMannaCheckedTime > 1200000000)
                //{
                //    ClickAndBack(coordinates, "3");
                //    lastMannaCheckedTime = DateTime.Now.Ticks;
                //}

                //if (DateTime.Now.Ticks - passiveSkillCheckedTime1 > 11000000000)
                //{
                //    ClickAndBack(coordinates, "5");
                //    passiveSkillCheckedTime1 = DateTime.Now.Ticks;
                //}
            }
        }

        private void ClickAndBack(Dictionary<string, Dictionary<string, int>> coordinates, string button)
        {
            FindAndClickButton(coordinates[button]);
            BackToMovablePosition();
            Thread.Sleep(500);
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
            _manipulator.ChangeMotorAngle(1, 200,2);
            _manipulator.ChangeTwoMotorsAngleOneTime(1, 2, 200, 270, 2);
        }

        private void FindAndClickButton(Dictionary<string, int> angleValuesByMotor)
        {
            int firstMotorGoalPosition = angleValuesByMotor["1"];
            int secondMotorGoalPosition = angleValuesByMotor["2"];
            int thirdMotorGoalPosition = angleValuesByMotor["3"];
            int forthMotorGoalPosition = angleValuesByMotor["4"];

            _manipulator.ChangeMotorAngle(4, forthMotorGoalPosition, 5);
            _manipulator.ChangeTwoMotorsAngleOneTime(1, 2, firstMotorGoalPosition, secondMotorGoalPosition, 10);

            PressAndRelease(thirdMotorGoalPosition);
        }

        private void PressAndRelease(int motorGoalPosition)
        {
            _manipulator.ChangeMotorAngle(3, motorGoalPosition, 80);
            _manipulator.ChangeMotorAngle(3, 400, 80);
        }

        public void MoveAndSave(int v, int value)
        {
            _manipulator.ChangeMotorAngle(v, value, 1);
        }
    }
}
