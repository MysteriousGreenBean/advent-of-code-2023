namespace _20._1
{
    internal class Pulse
    {
        public required PulseType Type { get; init; }
        public required IModule Sender { get; init; }
        public required IModule Receiver { get; init; }

        public override string ToString()
            => $"{Sender.Name} -{Type}-> {Receiver.Name}";
    }

    internal enum PulseType
    {
        None = 'n',
        Low = 'l',
        High = 'h'
    }
}
