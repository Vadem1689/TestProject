using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.PathFinderSystem
{
    public class PathFinder : IPathFinder
    {
        public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges) 
        { 
            var edgeList = new List<Edge>(edges); 
            var graph = BuildGraph(edgeList);

            
            var startEdge = FindEdgeContainingPoint(A, edgeList); 
            var endEdge = FindEdgeContainingPoint(C, edgeList);
            
            if (startEdge == null || endEdge == null) 
            { 
                Debug.LogWarning("Start or end edge not found.");
                return new List<Vector2>();
            }
        
            var path = FindPathWithMinTurns(A, C, graph, startEdge.Value, endEdge.Value);
        
            if (path == null || path.Count == 0)
            {
                Debug.LogWarning("No path found.");
                return new List<Vector2>();
            }
        
            int turns = CalculateTurns(path);
            Debug.Log($"Path found with {path.Count - 1} segments and {turns} turn(s).");

            return path; 
        }
        
        private Dictionary<Edge, List<Edge>> BuildGraph(List<Edge> edges) 
        { 
            var graph = new Dictionary<Edge, List<Edge>>();
            
            foreach (var edge1 in edges)
            { 
                foreach (var edge2 in edges)
                {
                    if (edge1 != edge2 && edge1.Second.Equals(edge2.First))
                    {
                        if (!graph.ContainsKey(edge1))
                            graph[edge1] = new List<Edge>();

                        graph[edge1].Add(edge2);
                    }
                }
            }

            return graph; 
        }
        
        private Edge? FindEdgeContainingPoint(Vector2 point, List<Edge> edges) 
        { 
            foreach (var edge in edges) 
            {
                if (IsPointOnEdge(point, edge.Start, edge.End))
                    return edge;
            }
            return null; 
        }
        private bool IsPointOnEdge(Vector2 point, Vector2 start, Vector2 end) 
        { 
            var crossProduct = (point.y - start.y) * (end.x - start.x) - (point.x - start.x) * (end.y - start.y);
            
            if (Mathf.Abs(crossProduct) > Mathf.Epsilon)
                return false;

            var dotProduct = (point.x - start.x) * (end.x - start.x) + (point.y - start.y) * (end.y - start.y);

            if (dotProduct < 0)
                return false;

            var squaredLength = (end.x - start.x) * (end.x - start.x) + (end.y - start.y) * (end.y - start.y);

            return dotProduct <= squaredLength; 
        }
        
        private List<Vector2> FindPathWithMinTurns(Vector2 start, Vector2 end, Dictionary<Edge, List<Edge>> graph, Edge startEdge, Edge endEdge) 
        { 
            var queue = new Queue<(Edge currentEdge, List<Vector2> path)>(); 
            queue.Enqueue((startEdge, new List<Vector2> { start }));
            
            while (queue.Count > 0)
            {
                var (currentEdge, path) = queue.Dequeue();

                if (currentEdge.Equals(endEdge))
                {
                    path.Add(end);
                    return path;
                }

                if (graph.TryGetValue(currentEdge, out var value))
                {
                    foreach (var neighbor in value)
                    {
                        var newPath = new List<Vector2>(path) { neighbor.Start };
                        queue.Enqueue((neighbor, newPath));
                    }
                }
            }

            return null; 
        }
        
        private int CalculateTurns(List<Vector2> path) 
        { 
            if (path == null || path.Count < 3)
                return 0;

            int turns = 0;

            for (int i = 1; i < path.Count - 1; i++)
            {
                Vector2 previous = path[i - 1];
                Vector2 current = path[i];
                Vector2 next = path[i + 1];

                Vector2 direction1 = current - previous;
                Vector2 direction2 = next - current;

                if (!IsSameDirection(direction1, direction2))
                {
                    turns++;
                    Debug.Log($"Turn detected at index {i}, point: {current}");
                }
            }

            return turns; 
        }
        
        private bool IsSameDirection(Vector2 dir1, Vector2 dir2) 
        { 
            dir1.Normalize(); 
            dir2.Normalize(); 
            return Mathf.Approximately(Vector2.Dot(dir1, dir2), 1f) || Mathf.Approximately(Vector2.Dot(dir1, dir2), -1f); 
        }
    }
}
