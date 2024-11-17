using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Game.Scripts.PathFinderSystem
{
    public class Test : MonoBehaviour
    {
        private PathFinder _pathFinder;

        private void Start()
        {
            _pathFinder = new PathFinder();
        }

        [Button]
        public void TestPathFinder()
        {
            List<Edge> edges = new List<Edge>
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

          
            var result = _pathFinder.GetPath(new Vector2(2, 25), new Vector2(17, 30), edges);
            
            Debug.Log(result);
        }




        [Button]
        public void Test2()
        {
            var edges = new List<Edge>
            {
                new Edge(
                    new Rectangle(new Vector2(0, 0), new Vector2(2, 2)),
                    new Rectangle(new Vector2(2, 0), new Vector2(4, 2)),
                    new Vector2(1, 1), new Vector2(3, 1)
                ),
                new Edge(
                    new Rectangle(new Vector2(2, 0), new Vector2(4, 2)),
                    new Rectangle(new Vector2(4, 2), new Vector2(6, 4)),
                    new Vector2(3, 1), new Vector2(5, 3)
                ),
                new Edge(
                    new Rectangle(new Vector2(4, 2), new Vector2(6, 4)),
                    new Rectangle(new Vector2(6, 4), new Vector2(8, 6)),
                    new Vector2(5, 3), new Vector2(7, 5)
                )
            };

            var startPoint = new Vector2(1, 1);
            var endPoint = new Vector2(7, 5);

            var pathFinder = new PathFinder();
            var result = pathFinder.GetPath(startPoint, endPoint, edges);

            Debug.Log(result);
        }
    }
}