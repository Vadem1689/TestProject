using System;
using UnityEngine;

namespace _Game.Scripts.PathFinderSystem
{
    [Serializable]
    public struct Edge
    {
        public Rectangle First;
        public Rectangle Second;
        public Vector2 Start;
        public Vector2 End;

        public Edge(Rectangle first, Rectangle second, Vector2 start, Vector2 end)
        {
            First = first;
            Second = second;
            Start = start;
            End = end;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Edge other)
            {
                return First.Equals(other.First) &&
                       Second.Equals(other.Second) &&
                       Start.Equals(other.Start) &&
                       End.Equals(other.End);
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + First.GetHashCode();
                hash = hash * 31 + Second.GetHashCode();
                hash = hash * 31 + Start.GetHashCode();
                hash = hash * 31 + End.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Edge lhs, Edge rhs)
        {
            return lhs.Equals(rhs);
        }
        
        public static bool operator !=(Edge lhs, Edge rhs)
        {
            return !(lhs == rhs);
        }
    }

    [Serializable]
    public struct Rectangle : IEquatable<Rectangle>
    {
        public Vector2 Min;
        public Vector2 Max;

        public Rectangle(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public bool Equals(Rectangle other)
        {
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        public override bool Equals(object obj)
        {
            return obj is Rectangle other && Equals(other);
        }
    }
}