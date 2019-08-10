using System;
using Gemserk.DataGrids;
using Gemserk.Vision;
using UnityEngine;

public class DebugGroundLevel : MonoBehaviour
{
	public VisionMatrixSystem visionSystem;
	public SpriteRenderer spriteRenderer;

	public Color[] colors;

	// public Vector2 worldSize;
	public Vector2 gridSize;
	
	private GridMaskDataTexture groundLevelTexture;

	private void Awake()
	{
		if (spriteRenderer == null)
		{
			enabled = false;
			return;
		}
		
		groundLevelTexture = new GridMaskDataTexture(TextureFormat.RGBA32, spriteRenderer, colors, 
			visionSystem.width, visionSystem.height, 
			gridSize.x, gridSize.y);
	}

	private void LateUpdate()
	{
		if (visionSystem == null)
			return;
		if (groundLevelTexture == null)
			return;
		groundLevelTexture.UpdateTexture(visionSystem.groundData);
	}
}