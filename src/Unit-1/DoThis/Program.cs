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

            Props consoleWriterProps = Props.Create<ConsoleWriterActor>();
            IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps,nameof(consoleWriterActor));

            Props validationActorProps = Props.Create(() => new ValidationActor(consoleWriterActor));
            IActorRef validationActor = MyActorSystem.ActorOf(validationActorProps, nameof(validationActor));

            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, nameof(consoleReaderActor));


            // tell console reader to begin
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }
    }
    #endregion
}
