using BotStarter.HardwareInteraction;
using BotStarter.Models;
using ImageRecognition;
using SpeechRecognition;

namespace BotStarter
{
    public class RunApp : IRunApp
    {
        IManipulator _manipulator;
        IConfiguration _configuration;
        IEmguCvProcessor _emguCvProcessor;
        ISpeechRecognitionProcessor _speechRecognitionProcessor;

        string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

        public RunApp(IManipulator manipulator, IConfiguration configuration, 
            IEmguCvProcessor emguCvProcessor, ISpeechRecognitionProcessor speechRecognitionProcessor)
        {
            _manipulator = manipulator;
            _configuration = configuration;
            _emguCvProcessor = emguCvProcessor;
            _speechRecognitionProcessor = speechRecognitionProcessor;
        }

        public void Run()
        {
            var coordinates = _configuration.GetCoordinates();
            long lastMannaCheckedTime = DateTime.Now.Ticks;
            long passiveSkillCheckedTime1 = DateTime.Now.Ticks;
            long healingSkillCheckedTime = DateTime.Now.Ticks;

            string path = Path.Combine(solutiondir, "images");

            string[] fileEntries = Directory.GetFiles(path);
            string blazingArrow = Path.Combine(path,"BlazingArrow", "blazingArrow.png");
            string manaLevel = Path.Combine(path, "Mana", "manaLevel.png");

            _speechRecognitionProcessor.GetRecognizedSpeechInConsole();

            //while (true)
            //{
            //    BackToMovablePosition();

            //    foreach (string fileName in fileEntries)
            //    {
            //        var healthCoords = _emguCvProcessor.GetCoordinates(fileName);
            //        if (healthCoords.X > 0) ClickAndBack(coordinates, "7");
            //    }

            //    if (DateTime.Now.Ticks - healingSkillCheckedTime > 11000000000)
            //    {
            //        ClickAndBack(coordinates, "7");
            //        healingSkillCheckedTime = DateTime.Now.Ticks;
            //    }

            //    Thread.Sleep(500);
            //    ClickAndBack(coordinates, "2");
            //    Thread.Sleep(5000);
            //    ClickAndBack(coordinates, "tab");
            //    Thread.Sleep(500);
            //    BackToMovablePosition();
            //    ClickAndBack(coordinates, "2");
            //    Thread.Sleep(5000);
            //    ClickAndBack(coordinates, "4", 5000);

            //    var blazingArrowCoords = _emguCvProcessor.GetCoordinates(blazingArrow);
            //    if (blazingArrowCoords.X <= 0) ClickAndBack(coordinates, "5");

            //    var manaLevelCoords = _emguCvProcessor.GetCoordinates(manaLevel);
            //    if (manaLevelCoords.X > 0) ClickAndBack(coordinates, "3");
            //}
        }

        private void OrderCompleted(object sender, EventArgs e)
        { 
            
        }

        private void ClickAndBack(Dictionary<string, Dictionary<string, int>> coordinates, string button, int pause = 0)
        {
            FindAndClickButton(coordinates[button], pause);
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
            if(pause > 0) Thread.Sleep(pause);
            _manipulator.ChangeMotorAngle(3, 400, 80);
        }

        public void MoveAndSave(int v, int value)
        {
            _manipulator.ChangeMotorAngle(v, value, 1);
        }
    }
}
