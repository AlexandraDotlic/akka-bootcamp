using System;
﻿using Akka.Actor;

namespace WinTail
{
    #region Program
    class Program
    {
        /*NOTE: 
         * When creating Props, ActorSystem, or ActorRef you will very rarely see the new keyword.
         * These objects must be created through the factory methods built into Akka.NET.
         * If you're using new you might be making a mistake.
         */
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            // initialize MyActorSystem
            // YOU NEED TO FILL IN HERE
            MyActorSystem = ActorSystem.Create("Myactorsystem");

            PrintInstructions();

            // time to make your first actors!
            //YOU NEED TO FILL IN HERE
            // make consoleWriterActor using these props: Props.Create(() => new ConsoleWriterActor())
            // make consoleReaderActor using these props: Props.Create(() => new ConsoleReaderActor(consoleWriterActor))
            var consoleWriterActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleWriterActor()));
            var consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleReaderActor(consoleWriterActor)));

            // tell console reader to begin
            //YOU NEED TO FILL IN HERE
            consoleReaderActor.Tell("start");
            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.Write("Some lines will appear as");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" red ");
            Console.ResetColor();
            Console.Write(" and others will appear as");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" green! ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }
    }
    #endregion
}
