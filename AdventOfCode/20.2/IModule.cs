namespace _20._2
{
    internal interface IModule
    {
        string Name { get; }
        string State { get; }
        IModule[] InputModules { get; }
        Pulse[] Process(Pulse pulse);
    }
}
