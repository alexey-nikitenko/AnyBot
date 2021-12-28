using BotStarter.Orders;
using System.Speech.Recognition;

namespace SpeechRecognition
{
    public class SpeechRecognitionProcessor : ISpeechRecognitionProcessor
    {
        private static readonly string heal = "Doctor give me a heal";
        private static readonly string buff = "Doctor give me a buff";
        private static readonly string follow = "Doctor follow me";
        private static readonly string stop = "Doctor stop";
        private static readonly string keep = "Doctor keep healing";

        IProcessOrder _processOrder;

        public SpeechRecognitionProcessor(IProcessOrder processOrder)
        {
            _processOrder = processOrder;
        }

        public void GetRecognizedSpeechInConsole()
        {

            // Create an in-process speech recognizer for the en-US locale.  
            using (SpeechRecognitionEngine recognizer =
              new SpeechRecognitionEngine(
                new System.Globalization.CultureInfo("en-US")))
            {

                GrammarBuilder builder = new GrammarBuilder();
                builder.Append(GetChoices());

                // Create a grammar for the phrase.  
                Grammar phraseGrammar = new Grammar(builder);

                // Create and load a dictation grammar.  
                recognizer.LoadGrammar(phraseGrammar);

                // Add a handler for the speech recognized event.  
                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(ProcessRecognizedSpeech);

                // Configure input to the speech recognizer.  
                recognizer.SetInputToDefaultAudioDevice();

                // Start asynchronous, continuous speech recognition.  
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                // Keep the console window open.  
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }

        private Choices GetChoices()
        { 
            Choices choices = new Choices();

            choices.Add(heal);
            choices.Add(buff);
            choices.Add(follow);
            choices.Add(stop);
            choices.Add(keep);

            return choices;
        }
        // Handle the SpeechRecognized event.  
        void ProcessRecognizedSpeech(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(e.Result.Text);
            _processOrder.RunOrder(e.Result.Text);
        }

    }
}