using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.PathFinderSystem
{
    public interface IPathFinder
    {
        public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges);
    }
}