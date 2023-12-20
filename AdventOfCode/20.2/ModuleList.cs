using System.Collections;

namespace _20._2
{
    internal class ModuleList : IEnumerable<IModule>
    {
        private readonly Dictionary<string, Module> modules = new();

        public IModule this[string name]
        {
            get => modules[name];
            set => modules[name] = (Module)value;
        }

        public ModuleList(string[] inputLines)
        {
            var inputModules = new Dictionary<string, List<Module>>();

            foreach (string line in inputLines)
            {
                string[] parts = line.Split(" -> ");
                char moduleType = parts[0][0];
                string moduleName = moduleType == 'b' ? "broadcaster" : parts[0][1..];
                modules[moduleName] = CreateModule(moduleType, moduleName);
            }

            foreach (string line in inputLines)
            {
                string[] parts = line.Split(" -> ");
                char moduleType = parts[0][0];
                string moduleName = moduleType == 'b' ? "broadcaster" : parts[0][1..];
                string[] outputModuleNames = parts[1].Split(", ", StringSplitOptions.RemoveEmptyEntries);

                modules[moduleName].OutputModules = outputModuleNames.Select(GetOrCreateOutputModule).ToArray();
                foreach (string outputModuleName in outputModuleNames)
                {
                    if (!inputModules.ContainsKey(outputModuleName))
                        inputModules[outputModuleName] = new List<Module>();
                    inputModules[outputModuleName].Add(modules[moduleName]);
                }
            }

            foreach (var (moduleName, inputModuleList) in inputModules)
                modules[moduleName].InputModules = inputModuleList.ToArray();
        }

        private IModule GetOrCreateOutputModule(string outputModuleName)
        {
            if (!modules.ContainsKey(outputModuleName))
                modules.Add(outputModuleName, new Output() { Name = outputModuleName });

            return modules[outputModuleName];
        }

        private Module CreateModule(char moduleType, string moduleName)
        {
            return moduleType switch
            {
                'b' => new Broadcaster() { Name = moduleName },
                '%' => new FlipFlop() { Name = moduleName },
                '&' => new Conjunction() { Name = moduleName },
                _ => throw new InvalidDataException($"Unknown module type: {moduleType}")
            };
        }

        public IEnumerator<IModule> GetEnumerator()
            => modules.OrderBy(m => m.Key).Select(m => m.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}
