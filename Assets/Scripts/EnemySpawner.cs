using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    //Getting Enemy Prefab and Spawn Position
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private Vector3 SpawnPosition;
    [SerializeField] private int EnemyQuantity = 0;

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
                float s = Random.Range(-2, 2);
                Instantiate(EnemyPrefab, new Vector3(SpawnPosition.x - s, SpawnPosition.y, SpawnPosition.z), Quaternion.identity);
            }
            //Disabling spawn
            isSpawned = true;
        }
    }
}
