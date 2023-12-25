namespace _25._1
{
    internal record Component(string Name)
    {
        public bool Visited { get; set; } = false;
        public string Group { get; set; } = string.Empty;
        public List<Component> DirectlyConnectedComponents { get; set; } = new List<Component>();
            public List<Edge> Edges { get; } = new List<Edge>();

        public List<Component> GetIndirectlyConnectedComponents()
        {
            var discoveredComponents = new List<Component>();
            foreach (Component directComponent in DirectlyConnectedComponents)
                DiscoverComponent(directComponent, discoveredComponents);
            return discoveredComponents;
        }

        private void DiscoverComponent(Component component, List<Component> discoveredComponents)
        {
            if (discoveredComponents.Contains(component))
                return;
            
            discoveredComponents.Add(component);

            foreach (Component directComponent in component.DirectlyConnectedComponents)
                DiscoverComponent(directComponent, discoveredComponents);
        }

        public override string ToString()
            => $"{Name}: {string.Join(' ', DirectlyConnectedComponents.Select(c => c.Name))}";

        public void ContractEdge(Edge edge)
        {
            // Merge this component with the one connected by the edge
            // Update the directly connected components and edges
            DirectlyConnectedComponents.Remove(edge.Node1);
            DirectlyConnectedComponents.Remove(edge.Node2);
            DirectlyConnectedComponents.AddRange(edge.Node1.DirectlyConnectedComponents);
            DirectlyConnectedComponents.AddRange(edge.Node2.DirectlyConnectedComponents);
            DirectlyConnectedComponents.RemoveAll(c => c == this); // Remove self-reference

            // Update edges
            Edges.AddRange(edge.Node1.Edges.Where(e => e.Node1 != this && e.Node2 != this));
            Edges.AddRange(edge.Node2.Edges.Where(e => e.Node1 != this && e.Node2 != this));

            // Mark the merged nodes as visited
            edge.Node1.Visited = true;
            edge.Node2.Visited = true;
        }
    }
}
