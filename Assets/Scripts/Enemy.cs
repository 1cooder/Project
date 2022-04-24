using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    //Getting scriptable object
    [SerializeField] 
    private EnemyType enemyType = null;

    //Getting Enemy Values
    Sprite _sprite;
    Sprite[] damagedSprites;

    Vector2 enemyScale;

    float speed;
    float maxHealt;
    float health;
    float damage;

    string _enemyName;


    private void Awake()
    {
        InitEnemyFromScriptableObject();
        health = maxHealt;
        Debug.Log(health);
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _sprite;
        transform.localScale = enemyScale;

    }


    //Getting Enemy values from scriptable object
    public void InitEnemyFromScriptableObject()
    {

        _sprite = enemyType.enemySprite;
        _enemyName = enemyType.enemyName;

        enemyScale = enemyType.enemyScale;
        speed = enemyType.enemySpeed;
        maxHealt = enemyType.enemyMaxHealt;
        damage = enemyType.enemyDamage;

        damagedSprites = enemyType.damagedEnemySprites;

    }

    //Enemy take hit
    public void TakeHit(float hit)
    {

        health -= hit;
        ChangeSpriteOnDamage();
        DestroyEnemy();
        Debug.Log(health);
        Debug.Log(GetComponent<SpriteRenderer>().sprite);

    }

    //Enemy destroy/died
    public void DestroyEnemy()
    {

        if(health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("enemy died");
        }
    }

    //enemy takes damage, the sprite changes compared to maxhealth
    public void ChangeSpriteOnDamage()
    {
        int a = damagedSprites.Length;

        if(a > 0)
        {
            for(int i = 0; i < a; i++)
            {
                if(health <= maxHealt / (a + 1) * (a - i))
                {
                    GetComponent<SpriteRenderer>().sprite =  damagedSprites[i];
                }
            }
        }
    }

}
