using System.Collections.Generic;

namespace Metro_Navigation.Sources.Model
{
    class BreadthFirstSearch
    {
        public Dictionary<ushort, List<ushort>> edgesTo;
        public Dictionary<ushort, int> distTo;
        private GraphAdjList g;

        public BreadthFirstSearch(GraphAdjList G)
        {
            edgesTo = new Dictionary<ushort, List<ushort>>();
            distTo = new Dictionary<ushort, int>();
            g = G;
        }

        public List<ushort> BFS(ushort a, ushort b)
        {
            foreach (var vertex in g.GetVertexes())
            {
                distTo[vertex] = -1;
                edgesTo[vertex] = new List<ushort>();
            }
            var queue = new Queue<ushort>();
            queue.Enqueue(a);
            distTo[a] = 0;

            while (queue.Count != 0)
            {
                ushort v = queue.Dequeue();

                foreach (var w in g.GetAdj(v))
                {
                    if (distTo[w] == -1)
                    {
                        queue.Enqueue(w);
                        distTo[w] = distTo[v] + 1;
                        edgesTo[w].AddRange(edgesTo[v]);
                        edgesTo[w].Add(v);

                        if(w == b)
                        {
                            edgesTo[w].Add(w);
                            return edgesTo[w];
                        } 
                    }
                }
            }
            return new List<ushort>();
        }

    }
}
