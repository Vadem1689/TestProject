using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.PathFinderSystem
{
    public class PathFinder : IPathFinder
    {
        public PathResult GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
        {
            if (edges == null || !edges.Any())
            {
                Debug.LogWarning("Edges are null or empty. Returning no path.");
                return new PathResult(new List<Vector2>(), -1);
            }

            var graph = BuildGraph(edges);
            
            if (!graph.ContainsKey(A) || !graph.ContainsKey(C))
            {
                Debug.LogWarning("Start or end point not found in graph. Returning no path.");
                return new PathResult(new List<Vector2>(), -1);
            }
            
            return FindPathWithMinTurns(graph, A, C);
        }

        private Dictionary<Vector2, List<Vector2>> BuildGraph(IEnumerable<Edge> edges)
        {
            var graph = new Dictionary<Vector2, List<Vector2>>();

            foreach (var edge in edges)
            {
                // Добавляем ребро в граф
                if (!graph.ContainsKey(edge.Start))
                    graph[edge.Start] = new List<Vector2>();
                if (!graph.ContainsKey(edge.End))
                    graph[edge.End] = new List<Vector2>();

                graph[edge.Start].Add(edge.End);
                graph[edge.End].Add(edge.Start);
            }

            return graph;
        }

        private PathResult FindPathWithMinTurns(
            Dictionary<Vector2, List<Vector2>> graph,
            Vector2 start,
            Vector2 goal)
        {
            var queue = new Queue<(Vector2 point, List<Vector2> path, Vector2? direction, int turns)>();
            queue.Enqueue((start, new List<Vector2> { start }, null, 0));
            var visited = new HashSet<Vector2> { start };

            while (queue.Count > 0)
            {
                var (current, path, lastDirection, turns) = queue.Dequeue();

                if (current == goal)
                {
                    Debug.Log($"Path found: {string.Join(" -> ", path)} with {turns} turns.");
                    return new PathResult(path, turns);
                }

                foreach (var neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        var currentDirection = (neighbor - current).normalized;
                        
                        var additionalTurns = lastDirection.HasValue && 
                                              !ApproximatelyEqual(lastDirection.Value, currentDirection)
                            ? 1
                            : 0;
                        
                        if (additionalTurns == 0 && lastDirection.HasValue)
                        {
                            additionalTurns = Mathf.Abs(lastDirection.Value.x - currentDirection.x) > 0 &&
                                              Mathf.Abs(lastDirection.Value.y - currentDirection.y) > 0
                                ? 1
                                : 0;
                        }

                        Debug.Log(
                            $"From {current} to {neighbor}: " +
                            $"LastDir={lastDirection}, CurrentDir={currentDirection}, " +
                            $"Turns={turns}, AdditionalTurns={additionalTurns}");

                        visited.Add(neighbor);
                        var newPath = new List<Vector2>(path) { neighbor };
                        queue.Enqueue((neighbor, newPath, currentDirection, turns + additionalTurns));
                    }
                }
            }

            Debug.LogWarning("No path found.");
            return new PathResult(new List<Vector2>(), -1); 
        }

        private bool ApproximatelyEqual(Vector2 dir1, Vector2 dir2, float tolerance = 0.01f)
        {
            return Mathf.Abs(dir1.x - dir2.x) < tolerance && Mathf.Abs(dir1.y - dir2.y) < tolerance;
        }
    }

    public interface IPathFinder
    {
        PathResult GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges);
    }


    [System.Serializable]
    public class PathResult
    {
        public List<Vector2> Path { get; }
        public int Turns { get; }

        public PathResult(List<Vector2> path, int turns)
        {
            Path = path;
            Turns = turns;
        }

        public override string ToString()
        {
            return Turns >= 0
                ? $"Path: {string.Join(" -> ", Path)}, Turns: {Turns}"
                : "No valid path found.";
        }
    }
}