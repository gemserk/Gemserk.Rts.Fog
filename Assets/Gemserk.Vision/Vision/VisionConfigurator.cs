using UnityEngine;

namespace Gemserk.Vision
{
    public class VisionConfigurator : MonoBehaviour
    {
        [SerializeField]
        protected VisionMatrixSystem _visionSystem;
		
        [SerializeField]
        private Camera _fogCamera;
		
        [SerializeField]
        private Transform _fogSpriteTransform;
		
        [SerializeField]
        private int _gridWidth = 96;

        [SerializeField]
        private int _gridHeight = 96;

        [SerializeField]
        private int _gridTilesWidth = 6;
        
        [SerializeField]
        private int _gridTilesHeight = 8;
		
        public void SetWorldSize(float worldWidth, float worldHeight)
        {
            _visionSystem.width = Mathf.RoundToInt(worldWidth * 100 * _gridTilesWidth / _gridWidth);
            _visionSystem.height = Mathf.RoundToInt(worldHeight * 100 * _gridTilesHeight / _gridHeight);

            // TODO: we might need to consider biggest size
            _fogCamera.orthographicSize = worldHeight / 2;
            _fogSpriteTransform.localScale = new Vector3(worldWidth, worldHeight, 1);
            
            _visionSystem.Init();
        }
    }
}