namespace _20._1
{
    internal interface IModule
    {
        string Name { get; }
        string State { get; }
        Pulse[] Process(Pulse pulse);
    }
}
