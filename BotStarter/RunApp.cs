namespace BotStarter
{
    internal class RunApp : IRunApp
    {
        IManipulator _manipulator;
        

        public RunApp(IManipulator manipulator)
        {
            _manipulator = manipulator;
        }

        public void Run()
        {
            int firstMotorGoalPosition = 200;
            int secondMotorGoalPosition = 200;
            int thirdMotorGoalPosition = 500;
            int forthMotorGoalPosition = 300;

            _manipulator.ChangeTwoMotorsAngleOneTime(firstMotorGoalPosition, secondMotorGoalPosition);

            _manipulator.MoveAndSave(3, thirdMotorGoalPosition);
            _manipulator.MoveAndSave(4, forthMotorGoalPosition);
        }
    }
}
