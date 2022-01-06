using BotStarter.HardwareInteraction;
using BotStarter.Models;
using ImageRecognition;

namespace BotStarter.Orders
{
    internal class ProcessOrder : IProcessOrder
    {
        IManipulator _manipulator;
        IConfiguration _configuration;
        IEmguCvProcessor _emguCvProcessor;
        private static readonly string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private volatile bool isClickContinue = false;
        private volatile Dictionary<string, int> item = null;
        Thread sender;

        public ProcessOrder(IManipulator manipulator, IConfiguration configuration
            , IEmguCvProcessor emguCvProcessor)
        {
            _manipulator = manipulator;
            _configuration = configuration;
            _emguCvProcessor = emguCvProcessor;
        }

        public void RunOrder(string order)
        {
            switch (order)
            {
                case "Doctor give me a heal":
                    ClickAndBack("2");
                    break;
                case "Doctor give me a buff":
                    ClickAndBack("3");
                    break;
                case "Doctor follow me":
                    Follow();
                    break;
                case "Doctor keep healing":
                    ClickAndBackConstantly("4", true, 1000);
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

        private void ClickAndBackConstantly(string button, bool clickingContinue, int pause = 0)
        {
            var coordinates = _configuration.GetCoordinates();
            isClickContinue = clickingContinue;
            item = coordinates[button];

            sender = new Thread(new ThreadStart(ThreadProc));
            sender.Start();
        }

        private void ThreadProc()
        {
            while (isClickContinue)
            {
                FindAndClickButton(item, 0);
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
            RightClick();
            WindowHelper.BringWindowToFront();

            Thread.Sleep(1000);

            string followImage = Path.Combine(path, "Follow", "follow.png");
            Coordinates followCoordinates = _emguCvProcessor.GetCoordinates(followImage);
            WindowHelper.BringWindowToFront();
            WindowHelper.SetCursorPosition(followCoordinates.X + 10, followCoordinates.Y + 10);
            Thread.Sleep(500);
            LeftClick();
        }

        public void RunSkill(string skill, int timeOut = 0, int amountOfClicks = 1)
        {
            string path = Path.Combine(solutiondir, "images");
            string followImage = Path.Combine(path, "Skills", skill + ".png");

            Coordinates followCoordinates = _emguCvProcessor.GetCoordinates(followImage);
            WindowHelper.BringWindowToFront();
            Thread.Sleep(500);
            WindowHelper.SetCursorPosition(followCoordinates.X + 10, followCoordinates.Y + 10);
            Thread.Sleep(500);

            for (int i = 0; i < amountOfClicks; i++)
            {
                LeftClick();
                Thread.Sleep(timeOut);
            }
        }

        public void MoveByMotorWithSpeedslow(int motorIndex, int initialNumber, int goalNumber, int speedSlow = 0)
        {
            _manipulator.MoveByMotorWithSpeedslow(motorIndex, initialNumber, goalNumber, speedSlow);
        }

        private void LeftClick()
        {
            _manipulator.MoveByMotorWithSpeedslow(14, 320, 400, 0);
            _manipulator.MoveByMotorWithSpeedslow(14, 400, 320, 0);
        }

        private void RightClick()
        {
            _manipulator.MoveByMotorWithSpeedslow(15, 250, 150, 0);
            _manipulator.MoveByMotorWithSpeedslow(15, 150, 250, 0);
        }

        public void ClickAndBack(string button, int pause = 0 )
        {
            var coordinates = _configuration.GetCoordinates();
            FindAndClickButton(coordinates[button], pause);
            BackToMovablePosition();
        }

        private void BackToMovablePosition()
        {
            var angles = _configuration.GetLastAngles();

            _manipulator.MoveByMotorWithSpeedslow(1, angles["1"], 180, 5);
            _manipulator.MoveByMotorWithSpeedslow(2, angles["2"], 250, 5);
        }

        private void FindAndClickButton(Dictionary<string, int> angleValuesByMotor, int pause)
        {
            var angles = _configuration.GetLastAngles();

            int firstMotorInitialPosition = angles["1"];
            int secondMotorInitialPosition = angles["2"];
            int thirdMotorInitialPosition = angles["3"];
            int forthMotorInitialPosition = angles["4"];

            int firstMotorGoalPosition = angleValuesByMotor["1"];
            int secondMotorGoalPosition = angleValuesByMotor["2"];
            int thirdMotorGoalPosition = angleValuesByMotor["3"];
            int forthMotorGoalPosition = angleValuesByMotor["4"];

            _manipulator.MoveByMotorWithSpeedslow(4, forthMotorInitialPosition, forthMotorGoalPosition, 5);
            Thread.Sleep(500);
            _manipulator.MoveByMotorWithSpeedslow(2, secondMotorInitialPosition, secondMotorGoalPosition, 5);
            _manipulator.MoveByMotorWithSpeedslow(1, firstMotorInitialPosition, firstMotorGoalPosition, 5);
            Thread.Sleep(500);
            ManipulatorClick(thirdMotorInitialPosition, thirdMotorGoalPosition, pause);
        }

        private void ManipulatorClick(int thirdMotorInitialPosition, int thirdMotorGoalPosition, int pause)
        {
            _manipulator.MoveByMotorWithSpeedslow(3, thirdMotorInitialPosition, thirdMotorGoalPosition, 0);

            if(pause > 0) Thread.Sleep(pause);

            _manipulator.MoveByMotorWithSpeedslow(3, thirdMotorGoalPosition, thirdMotorInitialPosition, 0);
        }

        public Dictionary<string, int> GetLastCoordinates()
        {
            return _configuration.GetLastAngles();
        }

        public void SaveCoordinates(CoordinatesModel coordinatesModel)
        {
            _configuration.SaveCoordinates(coordinatesModel);
        }

        public void UseHealOrManaPot()
        {
            string path = Path.Combine(solutiondir, "images");
            string manaImage = Path.Combine(path, "Mana", "manaLevel.png");

            var manaLevelCoords = _emguCvProcessor.GetCoordinates(manaImage);
            if (manaLevelCoords.X > 0) RunSkill("manaPot");

            string healImages = Path.Combine(path, "Heal");

            foreach (string file in Directory.GetFiles(healImages))
            {
                var healLevelCoords = _emguCvProcessor.GetCoordinates(file);
                if (healLevelCoords.X > 0) RunSkill("clericHeal");
            }
        }

        public bool MonsterInTarget()
        {
            string path = Path.Combine(solutiondir, "images");
            string monsterInTargetImages = Path.Combine(path, "Monster");

            foreach (string file in Directory.GetFiles(monsterInTargetImages))
            {
                var healLevelCoords = _emguCvProcessor.GetCoordinates(file);
                if (healLevelCoords.X > 0) 
                    return true;
            }

            return false;
        }

        public void GoToCase()
        {
            string path = Path.Combine(solutiondir, "images");
            string caseImage = Path.Combine(path, "Skills", "case.png");

            var caseCoords = _emguCvProcessor.GetCoordinates(caseImage);
            if (caseCoords.X > 0)
            {
                WindowHelper.BringWindowToFront();
                Thread.Sleep(500);
                WindowHelper.SetCursorPosition(caseCoords.X + 10, caseCoords.Y + 10);
                Thread.Sleep(500);
                LeftClick();
            }
        }

        public void MoveByMotor(int motorIndex, int goalPosition)
        {
            var angles = _configuration.GetLastAngles();
            int motorInitialPosition = angles[motorIndex.ToString()];

            _manipulator.MoveByMotorWithSpeedslow(motorIndex, motorInitialPosition, goalPosition, 0);
        }
    }
}
