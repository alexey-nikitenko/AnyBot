using BotStarter.HardwareInteraction;
using BotStarter.Models;
using BotStarter.MousePointer;
using BotStarter.Orders;
using ImageRecognition;
using SpeechRecognition;

namespace BotStarter
{
    public class RunApp : IRunApp
    {
        //IManipulator _manipulator;
        //IConfiguration _configuration;
        ISpeechRecognitionProcessor _speechRecognitionProcessor;
        IProcessOrder _order;

        string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

        //public RunApp(IManipulator manipulator, IConfiguration configuration, ISpeechRecognitionProcessor speechRecognitionProcessor)
        //{
        //    _manipulator = manipulator;
        //    _configuration = configuration;
        //    _speechRecognitionProcessor = speechRecognitionProcessor;
        //}

        public RunApp(IProcessOrder processOrder, ISpeechRecognitionProcessor speechRecognitionProcessor)
        {
            _order = processOrder;
            _speechRecognitionProcessor = speechRecognitionProcessor;
        }

        public void Run()
        {

            //_speechRecognitionProcessor.GetRecognizedSpeechInConsole();
            double healingSkillCheckedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
            double manaSkillCheckedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;

            while (true)
            {
                //_order.LeftClick();
                //Thread.Sleep(1000);
                //_order.RightClick();
                //Thread.Sleep(1000);

                //_order.BackToMovablePosition();

                //_order.RunSkill("1", 500);
                //_order.RunSkill("2", 500);

                if (!_order.MonsterInTarget())
                {
                    _order.ClickAndBack("4", 5000);
                    _order.ClickAndBack("tab");
                }
                

                //foreach (string fileName in fileEntries)
                //{
                //    var healthCoords = _emguCvProcessor.GetCoordinates(fileName);
                //    if (healthCoords.X > 0) ClickAndBack(coordinates, "7");
                //}

                RunPeriodicaly(ref healingSkillCheckedTime, "clericAtak", 3);
                RunPeriodicaly(ref manaSkillCheckedTime, "clericBaf", 1800);

                //Thread.Sleep(500);
                //ClickAndBack(coordinates, "2");
                //Thread.Sleep(5000);
                //_order.ClickAndBack(coordinates, "tab");
                //Thread.Sleep(500);
                //BackToMovablePosition();
                //ClickAndBack(coordinates, "2");
                //Thread.Sleep(5000);
                //ClickAndBack(coordinates, "4", 5000);

                _order.UseHealOrManaPot();
            }
        }

        private void RunPeriodicaly(ref double checkedTime, string skill, int period)
        {
            if (new TimeSpan(DateTime.Now.Ticks).TotalSeconds - checkedTime > period)
            {
                _order.RunSkill(skill, 500);
                checkedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
            }
        }
    }
}
