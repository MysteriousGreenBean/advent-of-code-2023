namespace _20._1
{
    internal class ButtonModule : IModule
    {
        private readonly ModuleList modules;
        private readonly List<(string state, int highPulsesSent, int lowPulsesSent)> history = new();
        private bool isLooped = false;
        private int pushedCount = 0;

        public long HighPulsesSent { get; private set; } = 0;
        public long LowPulsesSent { get; private set; } = 0;

        public ButtonModule(ModuleList modules)
        {
            this.modules = modules;
            this.PreserveState(0, 0);
        }

        public string State => history[pushedCount].state;

        public string Name => "Button";

        public Pulse[] Process(Pulse pulse)
            => Array.Empty<Pulse>();

        public void Push()
        {
            if (isLooped)
            {
                pushedCount = pushedCount >= history.Count - 1 ? 1 : pushedCount + 1;
                HighPulsesSent += history[pushedCount].highPulsesSent;
                LowPulsesSent += history[pushedCount].lowPulsesSent;
                return;
            }

            int lowPulsesSent = 1;
            int highPulsesSent = 0;
            var pulsesQueue = new Queue<Pulse>();

            IModule broadcaster = modules["broadcaster"];
            Pulse[] sentPulses = broadcaster.Process(new Pulse { Type = PulseType.Low, Sender = this, Receiver = broadcaster });
            foreach (Pulse pulse in sentPulses)
                pulsesQueue.Enqueue(pulse);

            lowPulsesSent += sentPulses.Length;

            while (pulsesQueue.Count > 0)
            {
                Pulse currentPulse = pulsesQueue.Dequeue();
                Pulse[] newPulses = currentPulse.Receiver.Process(currentPulse);
                foreach (Pulse pulse in newPulses)
                {
                    pulsesQueue.Enqueue(pulse);
                    if (pulse.Type == PulseType.High)
                        highPulsesSent++;
                    else
                        lowPulsesSent++;
                }
            }

            pushedCount++;
            this.PreserveState(highPulsesSent, lowPulsesSent);
            HighPulsesSent += highPulsesSent;
            LowPulsesSent += lowPulsesSent;
            isLooped = history[0].state == history[^1].state;
        }

        private void PreserveState(int highPulsesSent, int lowPulsesSent)
            => history.Add((string.Join('+', modules.Select(m => m.State)), highPulsesSent, lowPulsesSent));
    }
}
