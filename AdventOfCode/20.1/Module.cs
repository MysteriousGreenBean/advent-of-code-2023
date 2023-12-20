using System.Data;

namespace _20._1
{
    internal abstract class Module : IModule
    {
        public required string Name { get; init; }
        public IModule[] InputModules { get; set; } = Array.Empty<IModule>();
        public IModule[] OutputModules { get; set; } = Array.Empty<IModule>();
        public abstract string State { get; }

        public abstract Pulse[] Process(Pulse pulse);

        protected Pulse[] SentPulseToAllOutputs(PulseType pulseType)
        {
            var sentPulses = new Pulse[OutputModules.Length];
            for (int i = 0; i < OutputModules.Length; i++)
            {
                sentPulses[i] = new Pulse
                {
                    Type = pulseType,
                    Sender = this,
                    Receiver = OutputModules[i]
                };
            }
            return sentPulses;
        }
    }

    internal class Broadcaster : Module
    {
        public override string State => string.Empty;

        public override Pulse[] Process(Pulse pulse)
            => SentPulseToAllOutputs(pulse.Type);
    }

    internal class FlipFlop : Module
    {
        public override string State => Name + ":" + isOn.ToString();

        private bool isOn = false;

        public override Pulse[] Process(Pulse pulse)
        {
            if (pulse.Type == PulseType.High)
                return Array.Empty<Pulse>();

            isOn = !isOn;
            return SentPulseToAllOutputs(isOn ? PulseType.High : PulseType.Low);
        }
    }

    internal class Conjunction : Module
    {
        public override string State => 
            Name + ":" +string.Join('|', inputStates?.Select(i => (char)i) ?? Enumerable.Repeat((char)PulseType.Low, InputModules.Length));

        private PulseType[]? inputStates;

        public override Pulse[] Process(Pulse pulse)
        {
            inputStates ??= new PulseType[InputModules.Length];

            int senderIndex = Array.IndexOf(InputModules, pulse.Sender);
            inputStates[senderIndex] = pulse.Type;

            var sentPulseType = inputStates.All(s => s == PulseType.High) ? PulseType.Low : PulseType.High;

            return SentPulseToAllOutputs(sentPulseType);
        }
    }

    internal class Output : Module
    {
        public override string State => "";

        public override Pulse[] Process(Pulse pulse)
            => Array.Empty<Pulse>();
    }
}
