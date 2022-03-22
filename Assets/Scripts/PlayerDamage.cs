using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public GameManager gameManager;
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            gameManager.TakeDamage();
        }
    }

}
