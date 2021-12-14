namespace BotStarter
{
    internal interface IManipulator
    {
        void MoveBack();
        void MoveForward(int steps);
        void MoveForwardByMotor(int motorNmr, int steps);
        void MoveLeft();
        void MoveRight(int steps);
        void SmoothOneStepMoveRight(int initialPosition, int steps);
    }
}