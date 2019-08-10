using UnityEngine;

namespace Gemserk.Vision
{
    public abstract class VisionObstacle : MonoBehaviour
    {
        public int groundLevel;
        public abstract int GetGroundLevel(Vector2 worldPosition);

        private VisionObstacleCreationSystem _visionObstacleCreationSystem;

#if UNITY_EDITOR
        public string obstacleName = "VisionObstacle";
        
        private void OnValidate()
        {
            name = $"{obstacleName}(height:{groundLevel})";
        }
#endif
        
        protected void Awake()
        {
            _visionObstacleCreationSystem = VisionObstacleCreationSystem.Instance;
            if (_visionObstacleCreationSystem != null)
            {
                _visionObstacleCreationSystem.Register(this);
            }
        }
    }
}