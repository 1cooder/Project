using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    //Getting Enemy Prefab and Spawn Position
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject SpawnTarget;
    [SerializeField] private int EnemyQuantity = 0;
    [SerializeField] private float range = 2;
    
    private Vector3 SpawnPosition;

    private void Awake()
    {
        SpawnPosition = SpawnTarget.gameObject.transform.position;
    }

    //Checking if enemies are spawned to stop them being overspawned.
    private bool isSpawned = false;

    //Using Trigger to spawn the enemy. With this we can spawn enemy at certain point of the game.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isSpawned == false)
        {
            //Checking to see if code works
            Debug.Log("Spawn");
            //Spawning the enemy
            for (int i = 0; i < EnemyQuantity; i++)
            {
                float s = Random.Range(-range, range);
                Instantiate(EnemyPrefab, new Vector3(SpawnPosition.x - s, SpawnPosition.y, SpawnPosition.z), Quaternion.identity);
            }
            //Disabling spawn
            isSpawned = true;
        }
    }
}
