namespace BotStarter
{
    public interface IRunApp
    {
        void Run();
        public Dictionary<string, int> GetLastCoordinates();
        void MoveAndSave(int v, int value);
    }
}