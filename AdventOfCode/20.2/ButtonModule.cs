namespace _20._2
{
    internal class ButtonModule : IModule
    {
        private readonly ModuleList modules;
        private long buttonPushes = 0;
        
        public long[] MfInputHighs { get; private set; }


        public ButtonModule(ModuleList modules)
        {
            this.modules = modules;
            this.MfInputHighs = new long[modules["mf"].InputModules.Length];
        }
        public string Name => "Button";

        public string State => "";

        public IModule[] InputModules => Array.Empty<IModule>();

        public Pulse[] Process(Pulse pulse)
            => Array.Empty<Pulse>();

        public bool Push()
        {
            if (MfInputHighs.All(i => i != 0))
                return true;

            buttonPushes++;

            var pulsesQueue = new Queue<Pulse>();

            IModule broadcaster = modules["broadcaster"];
            Pulse[] sentPulses = broadcaster.Process(new Pulse { Type = PulseType.Low, Sender = this, Receiver = broadcaster });
            foreach (Pulse pulse in sentPulses)
                pulsesQueue.Enqueue(pulse);

            while (pulsesQueue.Count > 0)
            {
                Pulse currentPulse = pulsesQueue.Dequeue();
                if (currentPulse.Receiver.Name == "mf" && currentPulse.Type == PulseType.High)
                {
                    int indexOfSender = Array.IndexOf(currentPulse.Receiver.InputModules, currentPulse.Sender);
                    MfInputHighs[indexOfSender] = buttonPushes;
                }
                Pulse[] newPulses = currentPulse.Receiver.Process(currentPulse);
                foreach (Pulse pulse in newPulses)
                    pulsesQueue.Enqueue(pulse);
            }

            return false;
        }

    }
}
