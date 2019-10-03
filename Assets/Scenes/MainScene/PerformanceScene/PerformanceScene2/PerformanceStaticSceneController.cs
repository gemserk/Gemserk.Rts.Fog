using System;
using System.Collections.Generic;
using System.Linq;
using Gemserk.Vision;
using UnityEngine;

public class PerformanceStaticSceneController : MonoBehaviour
{
    private VisionMatrixSystem _visionSystem;
    
    public Collider2D spawnBounds;

    public int unitsCount;
    
    private List<Vision> _visions = new List<Vision>();

    public GameObject unitPrefab;

    public int totalPlayers = 8;

    public Transform unitsParent;

    public float minVision = 1;
    public float maxVision = 3;

    public int minGround = 0;
    public int maxGround = 3;
    
    [ContextMenu("Regenerate Units")]
    public void RegenerateUnitsForTesting()
    {
        // TODO: delete unitsParent children
        
        for (var i = 0; i < unitsCount; i++)
        {
            var x = UnityEngine.Random.Range(-spawnBounds.bounds.extents.x, spawnBounds.bounds.extents.x);
            var y = UnityEngine.Random.Range(-spawnBounds.bounds.extents.y, spawnBounds.bounds.extents.y);
            
            var unitObject = GameObject.Instantiate(unitPrefab, unitsParent);
            unitObject.transform.position = new Vector3(x, y, 0);

            var randomPlayer = UnityEngine.Random.Range(0, totalPlayers);
			
            var vision = unitObject.GetComponentInChildren<Vision>();
            vision.player = 1 << randomPlayer;
            vision.range = UnityEngine.Random.Range(minVision, maxVision);

            vision.groundLevel = UnityEngine.Random.Range(minGround, maxGround);
        }
    }
    
    // Start is called before the first frame update
    public void Start()
    {
        unitsParent.GetComponentsInChildren(_visions);
        
        _visionSystem = FindObjectOfType<VisionMatrixSystem>();
        _visionSystem.Init();
		
        var obstacleCreation = FindObjectOfType<VisionObstacleCreationSystem>();
        if (obstacleCreation != null)
        {
            obstacleCreation.VisionMatrixSystem = _visionSystem;
        }

        Application.targetFrameRate = 60;
    }

    private List<VisionData> visionDatas = new List<VisionData>();

    private void FixedUpdate()
    {
        _visionSystem.ClearVision();

        visionDatas.Clear();
        
        for (var i = 0; i < _visions.Count; i++)
        {
            var v = _visions[i];
            visionDatas.Add(new VisionData
            {
                position = v.transform.position,
                player = v.player,
                groundLevel = v.groundLevel,
                range = v.range
            });
        }
        
        _visionSystem.UpdateVisions(visionDatas);

//        for (var i = 0; i < _visions.Count; i++)
//        {
//            var v = _visions[i];
//            v.groundLevel = _visionSystem.GetGroundLevel(v.transform.position);
//            _visionSystem.UpdateVision(new VisionData
//            {
//                position = v.transform.position,
//                player = v.player,
//                groundLevel = v.groundLevel,
//                range = v.range
//            });
//        }
        // update for all visions
    }

    private void LateUpdate()
    {
        _visionSystem.UpdateTextures();
    }
}
