using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enmy Type", menuName = "Enemy Type")]
public class EnemyType : ScriptableObject
{
    public Sprite enemySprite = null;
    public Sprite[] damagedEnemySprites = null; //this array for change sprites on damage

    public Vector2 enemyScale = Vector2.one;

    public float enemyMaxHealt = 100;
    public float enemySpeed = 5;
    public float enemyDamage = 10;

    public string enemyName = "Enemy Name";




}
