using System;
using UnityEngine;

namespace Gemserk.Vision
{
    [Serializable]
    public struct VisionData
    {
        public int player;
        public Vector2 position;
        public float range;
        public short groundLevel;
    }
}