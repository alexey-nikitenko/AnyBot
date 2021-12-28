using BotStarter.HardwareInteraction;
using ImageRecognition;

namespace BotStarter.Orders
{
    internal class ProcessOrder : IProcessOrder
    {
        IManipulator _manipulator;
        IConfiguration _configuration;
        IEmguCvProcessor _emguCvProcessor;

        string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private volatile bool isClickContinue = false;
        private volatile Dictionary<string, int> item = null;
        int pauseGlobal = 0;
        Thread sender;

        public ProcessOrder(IManipulator manipulator, IConfiguration configuration
            ,IEmguCvProcessor emguCvProcessor)
        {
            _manipulator = manipulator;
            _configuration = configuration;
            _emguCvProcessor = emguCvProcessor;
        }

        public void RunOrder(string order)
        {
            var coordinates = _configuration.GetCoordinates();

            switch (order)
            {
                case "Doctor give me a heal":
                    ClickAndBack(coordinates, "2");
                    break;
                case "Doctor give me a buff":
                    ClickAndBack(coordinates, "3");
                    break;
                case "Doctor follow me":
                    Follow();
                    break;
                case "Doctor keep healing":
                    ClickAndBackConstantly(coordinates, "4", true, 1000);
                    break;
                case "Doctor stop":
                    BreakClickProcess();
                    break;
                default:
                    break;
            }

        }

        private void BreakClickProcess()
        {
            isClickContinue = false;
        }

        private void ClickAndBackConstantly(Dictionary<string, Dictionary<string, int>> coordinates, string button, bool clickingContinue, int pause = 0)
        {
            isClickContinue = clickingContinue;
            item = coordinates[button];
            pauseGlobal = pause;

                sender = new Thread(new ThreadStart(ThreadProc));
                sender.Start();
        }

        private void ThreadProc()
        {
            while (isClickContinue)
            {
                FindAndClickButton(item, pauseGlobal);
                BackToMovablePosition();
            }
        }

        private void Follow()
        {
            string path = Path.Combine(solutiondir, "images");
            string squadImage = Path.Combine(path, "Squad", "squad.png");
            Coordinates coordinates = _emguCvProcessor.GetCoordinates(squadImage);
            WindowHelper.BringWindowToFront();
            WindowHelper.SetCursorPosition(coordinates.X + 50, coordinates.Y + 50);
            _manipulator.ChangeMotorAngle(15, 400, 10);
            _manipulator.ChangeMotorAngle(15, 200, 10);
            _manipulator.ChangeMotorAngle(15, 400, 10);
            WindowHelper.BringWindowToFront();

            Thread.Sleep(1000);

            string followImage = Path.Combine(path, "Follow", "follow.png");
            Coordinates followCoordinates = _emguCvProcessor.GetCoordinates(followImage);
            WindowHelper.BringWindowToFront();
            WindowHelper.SetCursorPosition(followCoordinates.X + 10, followCoordinates.Y + 10);
            _manipulator.ChangeMotorAngle(14, 0, 10);
            _manipulator.ChangeMotorAngle(14, 400, 10);
            _manipulator.ChangeMotorAngle(14, 0, 10);
            //WindowHelper.BringWindowToFront();

        }

        private void ClickAndBack(Dictionary<string, Dictionary<string, int>> coordinates, string button, int pause = 0)
        {
            FindAndClickButton(coordinates[button], pause);
            BackToMovablePosition();
            Thread.Sleep(500);
        }

        private void BackToMovablePosition()
        {
            _manipulator.ChangeMotorAngle(1, 200, 2);
            _manipulator.ChangeTwoMotorsAngleOneTime(1, 2, 200, 270, 2);
        }

        private void FindAndClickButton(Dictionary<string, int> angleValuesByMotor, int pause = 0)
        {
            int firstMotorGoalPosition = angleValuesByMotor["1"];
            int secondMotorGoalPosition = angleValuesByMotor["2"];
            int thirdMotorGoalPosition = angleValuesByMotor["3"];
            int forthMotorGoalPosition = angleValuesByMotor["4"];

            _manipulator.ChangeMotorAngle(4, forthMotorGoalPosition, 5);
            _manipulator.ChangeTwoMotorsAngleOneTime(1, 2, firstMotorGoalPosition, secondMotorGoalPosition, 10);

            PressAndRelease(thirdMotorGoalPosition, pause);
        }

        private void PressAndRelease(int motorGoalPosition, int pause = 0)
        {
            _manipulator.ChangeMotorAngle(3, motorGoalPosition, 80);
            if (pause > 0) Thread.Sleep(pause);
            _manipulator.ChangeMotorAngle(3, 400, 80);
        }
    }
}
