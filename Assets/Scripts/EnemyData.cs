using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enmy Type", menuName = "Enemy Type")]
public class EnemyData : ScriptableObject
{
    public Sprite EnemySprite = null;
    public Sprite[] DamagedEnemySprites = null; //this array for change sprites on damage

    public Vector2 EnemyScale = Vector2.one;

    public float EnemyMaxHealth = 100;
    public float EnemySpeed = 5;
    public float EnemyDamage = 10;
    public float EnemyMoveRadius = 10f;
    
    public string EnemyName = "Enemy Name";



}
