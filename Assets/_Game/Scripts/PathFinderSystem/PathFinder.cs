using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.PathFinderSystem
{
    public class PathFinder : IPathFinder
    {
        public IEnumerable<Vector2> GetPath(Vector2 start, Vector2 end, IEnumerable<Edge> edges)
        {
            if (edges == null || !edges.Any())
            {
                Debug.LogError("Edges list is empty or null.");
                return Enumerable.Empty<Vector2>();
            }

            foreach (var edge in edges)
            {
                if (IsPointInsideRectangle(start, edge.First) && IsPointInsideRectangle(end, edge.First) ||
                    IsPointInsideRectangle(start, edge.Second) && IsPointInsideRectangle(end, edge.Second))
                {
                    Debug.Log("Путь найден. Количество поворотов: 0");
                    return new List<Vector2> { start, end }; // Прямая линия
                }
            }

            var graph = BuildGraph(edges, start, end);

            var path = FindPath(start, end, graph);

            if (path == null || !path.Any())
            {
                Debug.Log("Путь не найден.");
                return Enumerable.Empty<Vector2>();
            }

            int turns = CalculateTurns(path);
            Debug.Log($"Путь найден. Количество поворотов: {turns}");

            return path;
        }

        private Dictionary<Vector2, List<Vector2>> BuildGraph(IEnumerable<Edge> edges, Vector2 start, Vector2 end)
        {
            var graph = new Dictionary<Vector2, List<Vector2>>();

            foreach (var edge in edges)
            {
                if (!graph.ContainsKey(edge.Start))
                    graph[edge.Start] = new List<Vector2>();

                if (!graph.ContainsKey(edge.End))
                    graph[edge.End] = new List<Vector2>();

                graph[edge.Start].Add(edge.End);
                graph[edge.End].Add(edge.Start);
            }

            AddPointToGraphIfInsideRectangle(graph, start, edges);
            AddPointToGraphIfInsideRectangle(graph, end, edges);

            return graph;
        }

        private void AddPointToGraphIfInsideRectangle(Dictionary<Vector2, List<Vector2>> graph, Vector2 point, IEnumerable<Edge> edges)
        {
            foreach (var edge in edges.Where(edge => IsPointInsideRectangle(point, edge.First) || IsPointInsideRectangle(point, edge.Second)))
            {
                if (!graph.ContainsKey(point))
                    graph[point] = new List<Vector2>();

                graph[point].Add(edge.Start);
                graph[edge.Start].Add(point);
            }
        }

        private bool IsPointInsideRectangle(Vector2 point, Rectangle rect)
        {
            return point.x >= rect.Min.x && point.x <= rect.Max.x &&
                   point.y >= rect.Min.y && point.y <= rect.Max.y;
        }

        private List<Vector2> FindPath(Vector2 start, Vector2 end, Dictionary<Vector2, List<Vector2>> graph)
        {
            var queue = new Queue<Vector2>();
            var previous = new Dictionary<Vector2, Vector2?>();
            var visited = new HashSet<Vector2>();

            queue.Enqueue(start);
            previous[start] = null;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == end)
                    return ReconstructPath(previous, start, end);

                foreach (var neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                        previous[neighbor] = current;
                    }
                }
            }

            return null;
        }

        private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2?> previous, Vector2 start, Vector2 end)
        {
            var path = new List<Vector2>();
            var current = end;

            while (current != start)
            {
                path.Add(current);
                current = previous[current].GetValueOrDefault();
            }

            path.Add(start);
            path.Reverse();
            return path;
        }

        private int CalculateTurns(List<Vector2> path)
        {
            if (path.Count < 3) return 0;

            int turns = 0;

            for (int i = 1; i < path.Count - 1; i++)
            {
                var prevDirection = path[i] - path[i - 1];
                var nextDirection = path[i + 1] - path[i];

                if (!IsCollinear(prevDirection, nextDirection))
                    turns++;
            }

            return turns;
        }

        private bool IsCollinear(Vector2 a, Vector2 b)
        {
            return Mathf.Approximately(a.x * b.y, a.y * b.x);
        }
    }
}
