using BotStarter.HardwareInteraction;
using BotStarter.Models;
using ImageRecognition;
using static BotStarter.Constants;

namespace BotStarter.Orders
{
    internal class ProcessOrder : IProcessOrder
    {
        IManipulator _manipulator;
        IConfiguration _configuration;
        IEmguCvProcessor _emguCvProcessor;
        
        private volatile bool _isClickContinue = false;
        private volatile Dictionary<string, int>? _item;
        private volatile Dictionary<string, string>? _config;
        Thread _sender;

        public ProcessOrder(IManipulator manipulator, IConfiguration configuration, IEmguCvProcessor emguCvProcessor)
        {
            _manipulator = manipulator;
            _configuration = configuration;
            _emguCvProcessor = emguCvProcessor;
            _config = _configuration.GetParameters();
        }

        public void RunOrder(string order)
        {
            switch (order)
            {
                case heal:
                    ClickAndBack("2");
                    break;
                case buff:
                    ClickAndBack("3");
                    break;
                case follow:
                    Follow();
                    break;
                case keep:
                    ClickAndBackConstantly("4", true, 1000);
                    break;
                case stop:
                    BreakClickProcess();
                    break;
                case bot:
                    RunBot();
                    break;
                default:
                    break;
            }
        }

        private void RunBot()
        {
            double healingSkillCheckedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
            double manaSkillCheckedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;

            int xInitial = 100;
            int yInitial = 100;
            int xGoal = 200;
            int yGoal = 200;

            MoveMouse(xInitial, yInitial, xGoal, yGoal);
            PressLeft();
            MoveMouse(xInitial + 100, yInitial + 100, xGoal - 100, yGoal - 100);
            ReleaseLeft();
        }

        private void PressLeft()
        {
            int motor = int.Parse(_config[leftMouseKeyMotor]);
            int initial = int.Parse(_config[leftClickInitialPosition]);
            int goal = int.Parse(_config[leftClickGoalPosition]);

            _manipulator.MoveByMotorWithSpeedSlow(1, motor, initial, goal, 0);
        }
        private void ReleaseLeft()
        {
            int motor = int.Parse(_config[leftMouseKeyMotor]);
            int initial = int.Parse(_config[leftClickInitialPosition]);
            int goal = int.Parse(_config[leftClickGoalPosition]);

            _manipulator.MoveByMotorWithSpeedSlow(1, motor, goal, initial, 0);
        }

        private void LeftClick()
        {
            int motor = int.Parse(_config[leftMouseKeyMotor]);
            int initial = int.Parse(_config[leftClickInitialPosition]);
            int goal = int.Parse(_config[leftClickGoalPosition]);

            _manipulator.MoveByMotorWithSpeedSlow(viceVersa, motor, initial, goal, 0);
        }

        private void RightClick()
        {
            int motor = int.Parse(_config[rightMouseKeyMotor]);
            int initial = int.Parse(_config[rightClickInitialPosition]);
            int goal = int.Parse(_config[rightClickGoalPosition]);

            _manipulator.MoveByMotorWithSpeedSlow(viceVersa, motor, initial, goal, 0);
        }

        private void MoveMouse(int xInitial, int yInitial, int xGoal, int yGoal)
        {
            _manipulator.MoveByMotorWithSpeedSlow(1, 11, xGoal, xInitial, 0);
            Thread.Sleep(5000);
            _manipulator.MoveByMotorWithSpeedSlow(1, 10, yInitial, yGoal, 1);
            Thread.Sleep(5000);
            _manipulator.MoveByMotorWithSpeedSlow(1, 10, yGoal, yInitial, 1);
            Thread.Sleep(5000);
            _manipulator.MoveByMotorWithSpeedSlow(1, 11, xInitial, xGoal, 0);
            Thread.Sleep(5000);
        }

        private void BreakClickProcess()
        {
            _isClickContinue = false;
        }

        private void ClickAndBackConstantly(string button, bool clickingContinue, int pause = 0)
        {
            var coordinates = _configuration.GetCoordinates();
            _isClickContinue = clickingContinue;
            _item = coordinates[button];

            _sender = new Thread(new ThreadStart(ThreadProc));
            _sender.Start();
        }

        private void ThreadProc()
        {
            while (_isClickContinue)
            {
                FindAndClickButton(_item, 0);
                BackToMovablePosition();
            }
        }

        private void Follow()
        {
            string path = Path.Combine(solutiondir, images);
            string squadImage = Path.Combine(path, squadDir, squad);
            Coordinates coordinates = _emguCvProcessor.GetCoordinates(squadImage);
            WindowHelper.BringWindowToFront();
            WindowHelper.SetCursorPosition(coordinates.X + 50, coordinates.Y + 50);
            RightClick();
            WindowHelper.BringWindowToFront();

            Thread.Sleep(1000);

            string followImage = Path.Combine(path, followDir, followFile);
            Coordinates followCoordinates = _emguCvProcessor.GetCoordinates(followImage);
            WindowHelper.BringWindowToFront();
            WindowHelper.SetCursorPosition(followCoordinates.X + 10, followCoordinates.Y + 10);
            Thread.Sleep(500);
            LeftClick();
        }

        public void RunSkill(string skill, int timeOut = 0, int amountOfClicks = 1)
        {
            string path = Path.Combine(solutiondir, images);
            string skillImage = Path.Combine(path, skillsDir, skill + ".png");

            Coordinates skillCoordinates = _emguCvProcessor.GetCoordinates(skillImage);
            WindowHelper.BringWindowToFront();
            Thread.Sleep(500);
            WindowHelper.SetCursorPosition(skillCoordinates.X + 10, skillCoordinates.Y + 10);
            Thread.Sleep(500);

            for (int i = 0; i < amountOfClicks; i++)
            {
                LeftClick();
                Thread.Sleep(timeOut);
            }
        }

        public void MoveByMotorWithSpeedSlow(int amount, int motorIndex, int initialNumber, int goalNumber, int speedSlow = 0)
        {
            _manipulator.MoveByMotorWithSpeedSlow(amount, motorIndex, initialNumber, goalNumber, speedSlow);
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

            _manipulator.MoveByMotorWithSpeedSlow(1, 1, angles["1"], 180, 5);
            _manipulator.MoveByMotorWithSpeedSlow(1, 2, angles["2"], 250, 5);
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

            _manipulator.MoveByMotorWithSpeedSlow(1, 4, forthMotorInitialPosition, forthMotorGoalPosition, 5);
            Thread.Sleep(500);
            _manipulator.MoveByMotorWithSpeedSlow(1, 2, secondMotorInitialPosition, secondMotorGoalPosition, 5);
            _manipulator.MoveByMotorWithSpeedSlow(1, 1, firstMotorInitialPosition, firstMotorGoalPosition, 5);
            Thread.Sleep(500);
            ManipulatorClick(thirdMotorInitialPosition, thirdMotorGoalPosition, pause);
        }

        private void ManipulatorClick(int thirdMotorInitialPosition, int thirdMotorGoalPosition, int pause)
        {
            _manipulator.MoveByMotorWithSpeedSlow(1, 3, thirdMotorInitialPosition, thirdMotorGoalPosition, 0);

            if(pause > 0) Thread.Sleep(pause);

            _manipulator.MoveByMotorWithSpeedSlow(1, 3, thirdMotorGoalPosition, thirdMotorInitialPosition, 0);
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
            string path = Path.Combine(solutiondir, images);
            string manaImage = Path.Combine(path, manaDir, manaLevel);

            var manaLevelCoords = _emguCvProcessor.GetCoordinates(manaImage);
            if (manaLevelCoords.X > 0) RunSkill(manaPot);

            string healImages = Path.Combine(path, healDir);

            foreach (string file in Directory.GetFiles(healImages))
            {
                var healLevelCoords = _emguCvProcessor.GetCoordinates(file);
                if (healLevelCoords.X > 0) RunSkill(clericHeal);
            }
        }

        public bool MonsterInTarget()
        {
            string path = Path.Combine(solutiondir, images);
            string monsterInTargetImages = Path.Combine(path, monsterDir);

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
            string path = Path.Combine(solutiondir, images);
            string caseImage = Path.Combine(path, skillsDir, caseFile);

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

            _manipulator.MoveByMotorWithSpeedSlow(1, motorIndex, motorInitialPosition, goalPosition, 0);
        }

        public void MoveByMotorWithSpeedSlow(int amount, int motorNumber1, int motorNumber2, int motorNumber3, int initial, int goal, int delay)
        {
            _manipulator.MoveThreeMotors(amount, motorNumber1, motorNumber2, motorNumber3, initial, goal, delay);
        }

        public void MoveByMotorWithSpeedSlow(int amount, int motorNumber1, int motorNumber2, int initial, int goal, int delay)
        {
            _manipulator.MoveTwoMotors(amount, motorNumber1, motorNumber2, initial, goal, delay);
        }

        private void RunPeriodicaly(ref double checkedTime, string skill, int period)
        {
            if (new TimeSpan(DateTime.Now.Ticks).TotalSeconds - checkedTime > period)
            {
                checkedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
            }
        }
    }
}
