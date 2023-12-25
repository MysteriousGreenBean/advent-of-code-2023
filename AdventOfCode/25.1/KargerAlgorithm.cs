namespace _25._1
{
    internal class KargerAlgorithm
    {
        private Random random = new Random();

        public List<Edge> FindMinimumCut(List<Component> graph)
        {
            List<Component> workingGraph = new List<Component>(graph);
            while (workingGraph.Count > 2)
            {
                // Randomly choose an edge to contract
                Edge randomEdge = GetRandomEdge(workingGraph);

                // Contract the chosen edge
                workingGraph[workingGraph.IndexOf(randomEdge.Node1)].ContractEdge(randomEdge);
            }

            // The remaining edges in the graph represent the minimum cut
            return workingGraph[0].Edges;
        }

        private Edge GetRandomEdge(List<Component> graph)
        {
            // Flatten the edges of the graph into a list
            List<Edge> allEdges = graph.SelectMany(node => node.Edges).Distinct().ToList();

            // Randomly choose an edge from the list
            int randomIndex = random.Next(allEdges.Count);
            return allEdges[randomIndex];
        }
    }
}
