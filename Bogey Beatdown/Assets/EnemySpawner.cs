using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public int numEnemySpawn = 4;
    public float spawnRate = 2f;

    private bool isMovingLeft = true;
    public float spawnMovingSpeed = 4f;
    private float maxX, minX;
    enum SpawnLocation { Top, Bottom };

    [SerializeField]
    private SpawnLocation spawnLocation = SpawnLocation.Top;

    // Use this for initialization
    private void Awake()
    {
        maxX = transform.position.x + 1;
        minX = transform.position.x - 1;
    }

    private void Update()
    {
        if (isMovingLeft)
        {
            transform.position += new Vector3(spawnMovingSpeed * Time.deltaTime * -1, 0);
            if (transform.position.x <= minX)
                isMovingLeft = false;
        }
        else
        {
            transform.position += new Vector3(spawnMovingSpeed * Time.deltaTime * 1, 0);
            if (transform.position.x >= maxX)
                isMovingLeft = true;
        }

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (numEnemySpawn > 0)
        {
            var enemy = Instantiate(Resources.Load("Prefabs/Characters/Enemy"), transform) as GameObject;
            enemy.transform.parent = transform.parent;
            int dir = spawnLocation == SpawnLocation.Top ? -1 : 1;
            enemy.GetComponent<EnemyAI>().StartSpawn(transform.position, dir);
            --numEnemySpawn;
            ++Global.StageEnemies;
            yield return new WaitForSeconds(spawnRate);
        }
    }    
}
