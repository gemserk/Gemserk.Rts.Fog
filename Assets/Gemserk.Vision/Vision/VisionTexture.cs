using System;
using UnityEngine;

namespace Gemserk.Vision
{
    public class VisionTexture : MonoBehaviour
    {
        [SerializeField]
        protected TextureFormat _textureFormat;

        [SerializeField]
        protected SpriteRenderer _spriteRenderer;

        [NonSerialized]
        private Texture2D _texture;

        [NonSerialized]
        private Color[] _colors;
    
        [SerializeField]
        protected Color _greyColor = new Color(0.5f, 0, 0, 1.0f);
    
        [SerializeField]
        protected Color _whiteColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        [SerializeField] protected Color _startColor = new Color(0, 0, 0, 1.0f);

        [SerializeField]
        protected float _interpolateColorSpeed;

        private const float _defaultInterpolationColorSpeed = 6.0f;

        [SerializeField]
        protected bool _previousVision = true;

        [SerializeField]
        private Sprite _backgroundSprite;

        public bool PreviousVision
        {
            get { return _previousVision; }
            set { _previousVision = value; }
        }

        public bool ColorInterpolation
        {
            get { return _interpolateColorSpeed > 0; }
            set { _interpolateColorSpeed = value ? _defaultInterpolationColorSpeed  : 0.0f; }
        }
	
        public void Create(int width, int height, Vector2 scale)
        {
            _texture =  new Texture2D(width, height, _textureFormat, false, false);
            _texture.filterMode = FilterMode.Point;
            _texture.wrapMode = TextureWrapMode.Clamp;
		
            _spriteRenderer.sprite = Sprite.Create(_texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 1);
            _spriteRenderer.transform.localScale = scale;
        
            _colors = new Color[width * height];
        
            for (var i = 0; i < width * height; i++)
            {
                _colors[i] = _startColor;
            }
        
            _texture.SetPixels(_colors);
            _texture.Apply();
        }

        private VisionMatrix _visionMatrix;
        private bool _dirty = true;

        private int _activePlayers;
    
        public void UpdateTexture(VisionMatrix visionMatrix, int activePlayers)
        {
            _visionMatrix = visionMatrix;
            _activePlayers = activePlayers;
            _dirty = true;
        }
        
//        [SerializeField]
//        private Vector2 _tilesBlocked = new Vector2(5, 5);
        
//        [SerializeField]
//        private Vector2 _tileSize = new Vector2(16, 16);

        private void Update()
        {
            if (!_dirty || _visionMatrix.values == null)
                return;
        
            _dirty = false;
        
            var interpolationEnabled = _interpolateColorSpeed > Mathf.Epsilon;
            var alpha = Time.deltaTime * _interpolateColorSpeed;
        
            var width = _visionMatrix.width;
            var height = _visionMatrix.height;
            
            // TODO: use bg size too, it should be something like from 0 to bgstart.x + 5
           //  var bgSize = _backgroundSprite.rect.size / tileSize;
            
           //  Debug.Log(bgSize);
            
            // var diff = new Vector2(width, height) - bgSize;

            // var noise = 0;
            
            for (var i = 0; i < width * height; i++)
            {
                // get coordinates...
                var x = i % width;
                var y = Mathf.FloorToInt(i / width);
                
                var newColor = _startColor;
                
                var isVisible = _visionMatrix.IsVisible(_activePlayers, i);
    
                if (isVisible)
                {
                    newColor = _whiteColor;
                } else if (_previousVision && _visionMatrix.WasVisible(_activePlayers, i))
                {
                    newColor = _greyColor;
                } 
        
                if (interpolationEnabled)
                {
                    newColor.r = Mathf.LerpUnclamped(_colors[i].r, newColor.r, alpha);
                } 
    
//                if (x - noise >= _tilesBlocked.x && y - noise >= _tilesBlocked.y 
//                                                 && x + noise < width - _tilesBlocked.x && y + noise < height - _tilesBlocked.y)
//                {
//
//                }
            
                _colors[i] = newColor;

//                noise = (noise + 1) % 3;
            }
        
            _texture.SetPixels(_colors);
            _texture.Apply();
        }

    }
}