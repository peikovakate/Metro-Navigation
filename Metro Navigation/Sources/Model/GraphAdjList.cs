using System.Collections.Generic;

namespace Metro_Navigation.Sources.Model
{
    class GraphAdjList
    {
        private readonly int V;

        private readonly Dictionary<ushort,List<ushort>> Adj;

        private readonly List<ushort> Vertices;

        public GraphAdjList(int v)
        {
            V = v;
            Adj = new Dictionary<ushort, List<ushort>>();
            Vertices = new List<ushort>();
            for (ushort id = 1; id<=V; id++)
            {
                Vertices.Add(id);
                Adj[id] = new List<ushort>();
            }
        }
        
        public GraphAdjList(List<ushort> vertices)
        {
            V = vertices.Count;
            Vertices = new List<ushort>(vertices);
            Adj = new Dictionary<ushort, List<ushort>>();
            Vertices = new List<ushort>();
            for (ushort id = 1; id <= V; id++)
            {
                Vertices.Add(id);
                Adj[id] = new List<ushort>();
            }
        }

        public void AddEdge(ushort v, ushort w)
        {
            Adj[v].Add(w);
            Adj[w].Add(v);
        }

        public List<ushort> GetAdj(ushort v)
        {
            return Adj[v];
        }

        public int VertexCount
        {
            get
            {
                return V;
            }
        }

        public List<ushort> GetVertexes()
        {
            return Vertices;
        }
    }
}
