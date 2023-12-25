namespace _25._1
{
    internal class Edge
    {
        public Component Node1 { get; }
        public Component Node2 { get; }
    
        public Edge(Component node1, Component node2)
        {
            Node1 = node1;
            Node2 = node2;
        }

        public override string ToString()
        {
            return $"({Node1.Name}, {Node2.Name}";
        }
    }
}
