using BotStarter.Orders;
using System.Speech.Recognition;
using static BotStarter.Constants;

namespace SpeechRecognition
{
    public class SpeechRecognitionProcessor : ISpeechRecognitionProcessor
    {
        IProcessOrder _processOrder;

        public SpeechRecognitionProcessor(IProcessOrder processOrder)
        {
            _processOrder = processOrder;
        }

        public void GetRecognizedSpeechInConsole()
        {
            using (SpeechRecognitionEngine recognizer =
              new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))
            {
                GrammarBuilder builder = new GrammarBuilder();
                builder.Append(GetChoices());

                Grammar phraseGrammar = new Grammar(builder);

                recognizer.LoadGrammar(phraseGrammar);
                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(ProcessRecognizedSpeech);
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

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
            choices.Add(bot);

            return choices;
        }

        void ProcessRecognizedSpeech(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(e.Result.Text);
            _processOrder.RunOrder(e.Result.Text);
        }

    }
}