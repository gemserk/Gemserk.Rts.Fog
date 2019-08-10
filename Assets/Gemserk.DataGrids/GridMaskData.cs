using UnityEngine;

namespace Gemserk.DataGrids
{
    public class GridMaskData
    {
        private readonly Vector3 gridSize;
        private readonly Vector3 worldSize;
        
        // world to grid conversion

        public GridData gridData;

        public void StoreFlagValue(int value, Vector2 position)
        {
            var p = GetGridPosition(position);
            if (!gridData.IsInside(p.x, p.y)) 
                return;
            gridData.StoreFlagValue(value, p.x, p.y);
        }
        
        public void StoreValue(int value, Vector2 position)
        {
            var p = GetGridPosition(position);
            if (!gridData.IsInside(p.x, p.y)) 
                return;
            gridData.StoreValue(value, p.x, p.y);
        }

        public GridMaskData(Vector2 worldSize, Vector2 gridSize)
        {
            this.gridSize = gridSize;
            this.worldSize = worldSize;
            
            gridData = new GridData(Mathf.CeilToInt(worldSize.x / gridSize.x), 
                Mathf.CeilToInt(worldSize.y / gridSize.y), 0);
        }

        public Vector2Int GetGridPosition(Vector2 position)
        {
            var x = Mathf.RoundToInt((position.x + worldSize.x * 0.5f) / gridSize.x);
            var y = Mathf.RoundToInt((position.y + worldSize.y * 0.5f) / gridSize.y);
            return new Vector2Int(x, y);
        }

        public Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(
                x * gridSize.x - worldSize.x * 0.5f,
                y * gridSize.y - worldSize.y * 0.5f);
        }

        public int GetValue(Vector3 position)
        {
            var x = Mathf.RoundToInt((position.x + worldSize.x * 0.5f) / gridSize.x);
            var y = Mathf.RoundToInt((position.y + worldSize.y * 0.5f) / gridSize.y);
            
            return gridData.IsInside(x, y) ? gridData.ReadValue(x, y) : 0;
        }
    }
}
