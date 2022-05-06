using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enmy Type", menuName = "Enemy Type")]
public class EnemyData : ScriptableObject
{
    public Sprite _enemySprite = null;
    public Sprite[] _damagedSprites = null; //this array for change sprites on damage

    public Vector2 _enemyScale = Vector2.one;

    public float _maxHealth = 100;
    public float _speed = 5;
    public float _damage = 10;
    public float _patrollingRadius = 10f;
    public float _chaseRadius = 20f;
        
    public string _Name = "Enemy Name";



}
