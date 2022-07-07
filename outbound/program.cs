using SignalWire.Relay;
using SignalWire.Relay.Calling;
using System;

namespace Example
{
    internal class ExampleConsumer : Consumer
    {
        protected override void Setup()
        {
            Project = "649dc08e-3558-4efe-a598-46a966164b83";
            Token = "PTdaf6a2203f03e081317bc3c4dae79542fd737ae9cf49db60";
        }

        protected override void Ready()
        {
            // Create a new phone call and dial it immediately, block until it's answered, times out,
            // busy, or another error occurs
            DialResult resultDial = Client.Calling.DialPhone("+14803769009", "+17607972682");

            // Prompt with TTS and collect the PIN, block until it's finished or an error occurs
            PromptResult resultPrompt = resultDial.Call.PromptTTS(
                "Welcome to SignalWire! Please enter your PIN",
                new CallCollect
                {
                    InitialTimeout = 10,
                    Digits = new CallCollect.DigitsParams { Max = 4, DigitTimeout = 5 }
                });

            if (resultPrompt.Successful)
            {
                // Play back what was collected
                resultDial.Call.PlayTTS("You entered " + resultPrompt.Result + ". Thanks and good bye!");
            }

            // Hangup
            resultDial.Call.Hangup();
        }
    }

    internal class Program
    {
        public static void Main()
        {
            new ExampleConsumer().Run();
        }
    }
}