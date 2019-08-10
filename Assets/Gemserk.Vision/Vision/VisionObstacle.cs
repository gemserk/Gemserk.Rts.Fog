using UnityEngine;

namespace Gemserk.Vision
{
    public abstract class VisionObstacle : MonoBehaviour
    {
        public short groundLevel;
        public abstract short GetGroundLevel(Vector2 worldPosition);

        private VisionObstacleCreationSystem _visionObstacleCreationSystem;

#if UNITY_EDITOR
        public string obstacleName = "VisionObstacle";
        
        private void OnValidate()
        {
            name = string.Format("{0}(height:{1})", obstacleName, groundLevel);
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