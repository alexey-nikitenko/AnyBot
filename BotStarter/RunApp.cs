using SpeechRecognition;

namespace BotStarter
{
    public class RunApp : IRunApp
    {
        ISpeechRecognitionProcessor _speechRecognitionProcessor;

        public RunApp(ISpeechRecognitionProcessor speechRecognitionProcessor)
        {
            _speechRecognitionProcessor = speechRecognitionProcessor;
        }

        public void Run()
        {
            _speechRecognitionProcessor.GetRecognizedSpeechInConsole();
        }
    }
}
