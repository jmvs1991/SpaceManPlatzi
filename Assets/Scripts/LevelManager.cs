using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public LevelManager sharedInstance;
    public List<LevelBlockController> allTheLevelBlocks = new List<LevelBlockController>();
    public List<LevelBlockController> currentLevelBlocks = new List<LevelBlockController>();
    public Transform startPosition;

    void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBlock() 
    {
        LevelBlockController block;

        Vector3 spawnPosition = Vector3.zero;

        if (!currentLevelBlocks.Any())
        {
            block = Instantiate(allTheLevelBlocks[0]);
            spawnPosition = startPosition.position;
        }
        else
        {
            int ramdomIDx = Random.Range(0, allTheLevelBlocks.Count);
            block = Instantiate(allTheLevelBlocks[ramdomIDx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
        }

        block.transform.SetParent(this.transform, false);
        Vector3 correction = new Vector3(spawnPosition.x - block.startPoint.position.x,
                                        spawnPosition.y - block.startPoint.position.y,
                                        0);

        block.transform.position = correction;

        currentLevelBlocks.Add(block);
    }

    public void RemoveLevelBlock() { }

    public void RemoveAllLevelBlocks() { }

    public void GenerateInitialBlocks() 
    {
        for(int i = 0; i < 3; i++) 
        {
            AddLevelBlock();
        }
    }
}
