using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int health;
    public Image HealthBar;
    private void Start()
    {
        health = 100;
    }
    public void TakeDamage()
    {
        health -= 10;
        if (health <= 0)
        {
            Debug.Log("Game Over");
        }

        Debug.Log("Health: " + health);
    }


}
