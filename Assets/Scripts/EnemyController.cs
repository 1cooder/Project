using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    //Getting scriptable object
    [SerializeField] 
    private EnemyType _enemyType = null;

    //Getting Enemy Values
    Sprite _sprite;
    Sprite[] _damagedSprite;

    Vector2 _enemyScale;

    private float _speed;
    private float _maxHealth;
    private float _health;
    private float _damage;

    string _enemyName;


    private void Awake()
    {
        InitEnemyFromScriptableObject();
        _health = _maxHealth;
        Debug.Log(_health);
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _sprite;
        transform.localScale = _enemyScale;

    }


    //Getting Enemy values from scriptable object
    public void InitEnemyFromScriptableObject()
    {

        _sprite = _enemyType.enemySprite;
        _enemyName = _enemyType.enemyName;

        _enemyScale = _enemyType.enemyScale;
        _speed = _enemyType.enemySpeed;
        _maxHealth = _enemyType.enemyMaxHealt;
        _damage = _enemyType.enemyDamage;

        _damagedSprite = _enemyType.damagedEnemySprites;

    }

    //Enemy take hit
    public void TakeHit(float hit)
    {

        _health -= hit;
        ChangeSpriteOnDamage();
        DestroyEnemy();
        Debug.Log(_health);
        Debug.Log(GetComponent<SpriteRenderer>().sprite);

    }

    //Enemy destroy/died
    public void DestroyEnemy()
    {

        if(_health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("enemy died");
        }
    }

    //enemy takes damage, the sprite changes compared to maxhealth
    public void ChangeSpriteOnDamage()
    {
        int a = _damagedSprite.Length;

        if(a > 0)
        {
            for(int i = 0; i < a; i++)
            {
                if(_health <= _maxHealth / (a + 1) * (a - i))
                {
                    GetComponent<SpriteRenderer>().sprite =  _damagedSprite[i];
                }
            }
        }
    }

}
