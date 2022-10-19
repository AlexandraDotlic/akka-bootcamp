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
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            var consoleWriterActor = MyActorSystem.ActorOf(Props.Create(() =>
            new ConsoleWriterActor()));
            var consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() =>
            new ConsoleReaderActor(consoleWriterActor)));

            // tell console reader to begin
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }
    }
    #endregion
}
