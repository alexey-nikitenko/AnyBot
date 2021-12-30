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

            _order.MoveByMotorWithSpeedslow(3, 100, 400, 0);

            while (true)
            {

                if (!_order.MonsterInTarget())
                {
                    _order.ClickAndBack("4", 5000);

                }
                if (!_order.MonsterInTarget())
                {
                    _order.ClickAndBack("tab", 0);
                }
                else
                {
                    RunPeriodicaly(ref healingSkillCheckedTime, "clericAtak", 1);
                    _order.GoToCase();
                }

                RunPeriodicaly(ref manaSkillCheckedTime, "clericBaf", 1800);

                

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
