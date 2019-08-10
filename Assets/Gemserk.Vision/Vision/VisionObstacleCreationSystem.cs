using System.Collections.Generic;
using UnityEngine;

namespace Gemserk.Vision
{
    public class VisionObstacleCreationSystem : MonoBehaviour
    {
        private static VisionObstacleCreationSystem _instance;

        private const string _instanceName = "~VisionObstacleCreationSystem";
		
        public static VisionObstacleCreationSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    var instanceGameObject = GameObject.Find(_instanceName);
                    if (instanceGameObject != null)
                    {
                        _instance = instanceGameObject.GetComponentInChildren<VisionObstacleCreationSystem>();
                    }
                }
                return _instance;
            }
        }
		
        private VisionMatrixSystem _visionMatrixSystem;
		
        private readonly List<VisionObstacle> _pendingObstacles = new List<VisionObstacle>();
		
        private bool _dirty;

        private void OnValidate()
        {
            gameObject.name = _instanceName;
        }

        public VisionMatrixSystem VisionMatrixSystem
        {
            get => _visionMatrixSystem;
            set => _visionMatrixSystem = value;
        }

        private void Awake()
        {
            _instance = this;
        }
        
        public void Register(VisionObstacle obstacle)
        {
            _pendingObstacles.Add(obstacle);
        }

        private void FixedUpdate()
        {
            if (_visionMatrixSystem == null)
                return;
            
            _pendingObstacles.Sort((o1, o2) => o1.groundLevel - o2.groundLevel);
			
            for (var i = 0; i < _pendingObstacles.Count; i++)
            {
                _visionMatrixSystem.RegisterObstacle(_pendingObstacles[i]);
                Destroy(_pendingObstacles[i].gameObject);
            }
			
            _pendingObstacles.Clear();
        }
		
    }
}