namespace BotStarter.MousePointer
{
    public interface IPointer
    {
        void LeftClick();
        void RightClick();
        void Move(int x, int y);
        void Pause(int timeout);
    }
}