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

        string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private volatile bool isClickContinue = false;
        private volatile Dictionary<string, int> item = null;
        int pauseGlobal = 0;
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

        public void BreakClickProcess()
        {
            isClickContinue = false;
        }

        public void ClickAndBackConstantly(string button, bool clickingContinue, int pause = 0)
        {
            var coordinates = _configuration.GetCoordinates();
            isClickContinue = clickingContinue;
            item = coordinates[button];
            pauseGlobal = pause;

            sender = new Thread(new ThreadStart(ThreadProc));
            sender.Start();
        }

        public void ThreadProc()
        {
            while (isClickContinue)
            {
                FindAndClickButton(item, pauseGlobal);
                BackToMovablePosition();
            }
        }

        public void Follow()
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
            //WindowHelper.BringWindowToFront();

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

        public void LeftClick()
        {
            _manipulator.ChangeMotorAngle(14, 320, 20);
            _manipulator.ChangeMotorAngle(14, 480, 20);
            _manipulator.ChangeMotorAngle(14, 320, 20);
        }

        public void RightClick()
        {
            _manipulator.ChangeMotorAngle(15, 250, 50);
            _manipulator.ChangeMotorAngle(15, 100, 50);
            _manipulator.ChangeMotorAngle(15, 250, 50);
        }

        public void ClickAndBack(string button, int pause = 0)
        {
            var coordinates = _configuration.GetCoordinates();
            FindAndClickButton(coordinates[button], pause);
            BackToMovablePosition();
            Thread.Sleep(500);
        }

        public void BackToMovablePosition()
        {
            _manipulator.ChangeMotorAngle(1, 200, 2);
            _manipulator.ChangeTwoMotorsAngleOneTime(1, 2, 200, 270, 2);
        }

        public void FindAndClickButton(Dictionary<string, int> angleValuesByMotor, int pause = 0)
        {
            int firstMotorGoalPosition = angleValuesByMotor["1"];
            int secondMotorGoalPosition = angleValuesByMotor["2"];
            int thirdMotorGoalPosition = angleValuesByMotor["3"];
            int forthMotorGoalPosition = angleValuesByMotor["4"];

            _manipulator.ChangeMotorAngle(4, forthMotorGoalPosition, 5);
            _manipulator.ChangeTwoMotorsAngleOneTime(1, 2, firstMotorGoalPosition, secondMotorGoalPosition, 10);

            PressAndRelease(thirdMotorGoalPosition, pause);
        }

        public void PressAndRelease(int motorGoalPosition, int pause = 0)
        {
            _manipulator.ChangeMotorAngle(3, motorGoalPosition, 80);
            if (pause > 0) Thread.Sleep(pause);
            _manipulator.ChangeMotorAngle(3, 400, 80);
        }

        public Dictionary<string, int> GetLastCoordinates()
        {
            return _configuration.GetLastAngles();
        }

        public void SaveCoordinates(CoordinatesModel coordinatesModel)
        {
            _configuration.SaveCoordinates(coordinatesModel);
        }

        public void MoveAndSave(int v, int value)
        {
            _manipulator.ChangeMotorAngle(v, value, 1);
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
            string monsterInTargetImage = Path.Combine(path, "Monster", "monsterInTarget.png");

            var manaLevelCoords = _emguCvProcessor.GetCoordinates(monsterInTargetImage);

            return manaLevelCoords.X > 0;
        }
    }
}
