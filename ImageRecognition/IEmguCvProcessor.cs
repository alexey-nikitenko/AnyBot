namespace ImageRecognition
{
    public interface IEmguCvProcessor
    {
        Coordinates GetCoordinates(string imgPath, double threshold = 0.8);
    }
}