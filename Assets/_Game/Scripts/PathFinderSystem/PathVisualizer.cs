using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Game.Scripts.PathFinderSystem
{
    public class PathVisualizer : MonoBehaviour
    {
        [ShowNonSerializedField] private Vector2 _startPoint; 
        [ShowNonSerializedField] private Vector2 _endPoint;   
        
        private List<Edge> _edges; 
        
        private List<Vector2> _path;
        

       private void OnDrawGizmos() 
       { 
           if (_edges == null || _edges.Count == 0) 
           {
               Debug.LogWarning("Edges list is empty or null. No connections to draw."); 
           }
           else
           {
               Gizmos.color = Color.cyan;
               HashSet<Rectangle> drawnRectangles = new HashSet<Rectangle>();
               foreach (var edge in _edges)
               {
                   if (!drawnRectangles.Contains(edge.First))
                   {
                       DrawRectangle(edge.First);
                       drawnRectangles.Add(edge.First);
                   }

                   if (!drawnRectangles.Contains(edge.Second))
                   {
                       DrawRectangle(edge.Second);
                       drawnRectangles.Add(edge.Second);
                   }
               }
           }

           if (_path != null && _path.Count > 0)
           {
               Gizmos.color = Color.green;

               for (int i = 0; i < _path.Count - 1; i++)
               {
                   Gizmos.DrawLine(ToVector3(_path[i]), ToVector3(_path[i + 1]));
               }

               Gizmos.color = Color.red;
               foreach (var point in _path)
               {
                   Gizmos.DrawSphere(ToVector3(point), 0.1f);
               }

           }
           else
           {
               Debug.Log("Path is null or contains less than 2 points. No path to draw.");
           }
           Gizmos.color = Color.magenta;
           Gizmos.DrawSphere(ToVector3(_startPoint), 0.4f);
           Gizmos.DrawSphere(ToVector3(_endPoint), 0.4f);
       }

       private void DrawRectangle(Rectangle rect)
       {
           Vector3 bottomLeft = ToVector3(rect.Min);
           Vector3 bottomRight = new Vector3(rect.Max.x, rect.Min.y, 0);
           Vector3 topRight = ToVector3(rect.Max);
           Vector3 topLeft = new Vector3(rect.Min.x, rect.Max.y, 0);

           Gizmos.DrawLine(bottomLeft, bottomRight);
           Gizmos.DrawLine(bottomRight, topRight);
           Gizmos.DrawLine(topRight, topLeft);
           Gizmos.DrawLine(topLeft, bottomLeft);
       }

       private Vector3 ToVector3(Vector2 vector)
       {
           return new Vector3(vector.x, vector.y, 0);
       }

       public void CalculatePath()
       {
           if (_edges == null || _edges.Count == 0) return;

           // Используем наш PathFinder
           var pathFinder = new PathFinder();
           _path = new List<Vector2>(pathFinder.GetPath(_startPoint, _endPoint, _edges));

           if (_path.Count > 0)
           {
               Debug.Log($"Path calculated: {string.Join(" -> ", _path)}");
           }
           else
           {
               Debug.LogWarning("No path found.");
           }
       }

       [Button]
        public void TestPathFinder()
        {
            _edges = new List<Edge>
            {
                new Edge(
                    new Rectangle(new Vector2(-15, 15), new Vector2(2, 25)),
                    new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
                    new Vector2(-3, 25),
                    new Vector2(2, 25)),

                new Edge(
                    new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
                    new Rectangle(new Vector2(17, 20), new Vector2(37, 30)),
                    new Vector2(17, 25),
                    new Vector2(17, 30)),
            };
            
            
            _startPoint = new Vector2(2, 25);
            _endPoint = new Vector2(17, 30);
             
            CalculatePath();
        }
        
        
        [Button]
        public void Test4()
        {
            _edges = new List<Edge>
            {
                new Edge(
                    new Rectangle(new Vector2(0, 0), new Vector2(2, 2)),
                    new Rectangle(new Vector2(2, 0), new Vector2(4, 2)),
                    new Vector2(1, 1), new Vector2(3, 1) // Горизонтальный сегмент
                ),
                new Edge(
                    new Rectangle(new Vector2(2, 0), new Vector2(4, 2)),
                    new Rectangle(new Vector2(4, 2), new Vector2(6, 4)),
                    new Vector2(3, 1), new Vector2(5, 3) // Диагональный сегмент
                ),
                new Edge(
                    new Rectangle(new Vector2(4, 2), new Vector2(6, 4)),
                    new Rectangle(new Vector2(6, 4), new Vector2(8, 6)),
                    new Vector2(5, 3), new Vector2(7, 5) // Ещё один диагональный сегмент
                )
            };

             _startPoint = new Vector2(1, 1);
             _endPoint = new Vector2(7, 5);
             
             CalculatePath();
        }
        
        [Button]
        public void Test3()
        {
             _edges = new List<Edge>
            {
                new Edge(
                    new Rectangle(new Vector2(5, 35), new Vector2(22, 45)),
                    new Rectangle(new Vector2(17, 45), new Vector2(37, 55)),
                    new Vector2(17, 45),
                    new Vector2(22, 45)),

                new Edge(
                    new Rectangle(new Vector2(17, 45), new Vector2(37, 55)),
                    new Rectangle(new Vector2(37, 40), new Vector2(57, 50)),
                    new Vector2(37, 45),
                    new Vector2(37, 50)),
            };

            _startPoint = new Vector2(13.5f, 37);
            _endPoint = new Vector2(50, 45);

            CalculatePath();
        }
    }
}