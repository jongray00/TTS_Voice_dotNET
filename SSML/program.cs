using SignalWire.Relay;
using SignalWire.Relay.Calling;
using System;
using System.Collections.Generic;


namespace Calling_InboundCall
{
    internal class TestConsumer : Consumer
    {
        protected override void Setup()
        {
         // ENTER IN YOUR AUTH CREDS
            Project = "649dc08e-3558-4efe-a598-XXXXXXXXXXX";
            Token = "PTdaf6a2203f03e081317bc3c4dae79542fdXXXXXXXXXXX";
            Contexts = new List<string> { "office" };
        }

        // This is executed in a new thread each time, so it is safe to use blocking calls
        protected override void OnIncomingCall(Call call)
        {
            // Answer the incoming call, block until it's answered or an error occurs
            AnswerResult resultAnswer = call.Answer();

            if (!resultAnswer.Successful)
            {
                // The call was not answered successfully, stop the consumer and bail out
                Stop();
                return;
            }

            PlayResult resultPlay = call.Play(
                new List<CallMedia>
                {
                    new CallMedia
                    {
                        Type = CallMedia.MediaType.tts,
                        Parameters = new CallMedia.TTSParams
                        {
                            Text = "<speak> <prosody rate=\"x-slow\"> <say-as interpret-as=\"telephone\"> 6. 6. 1. 2. 5. 9. 2. 8. 0. 1.</say-as> </prosody> </speak>"
                        }
                    }
                },
                volume: 4.0
                );
            

            // Hangup
            call.Hangup();
            Console.WriteLine("Call Ended");


            // Stop the consumer
            Stop();
        }
    }

    internal class Program
    {
        public static void Main()
        {
            // Create the TestConsumer and run it
            new TestConsumer().Run();

            // Prevent exit until a key is pressed
            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
        
};