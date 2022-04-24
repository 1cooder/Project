using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    //Getting scriptable object
    [SerializeField] 
    private EnemyData _enemyType = null;

    //Getting Enemy Values
    Sprite _sprite;
    Sprite[] _damagedSprite;

    Vector2 _enemyScale;

    Vector3 _startPosition = Vector3.zero;

    private float _speed;
    private float _maxHealth;
    private float _health;
    private float _damage;
    private float _moveRadius;

    string _enemyName;


    private void Awake()
    {
        InitEnemyFromScriptableObject();
        _health = _maxHealth;
        Debug.Log(_health);
        _startPosition = transform.position;
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _sprite;
        transform.localScale = _enemyScale;
    }

    private void Update()
    {
        Move();
    }

    //Getting Enemy values from scriptable object
    public void InitEnemyFromScriptableObject()
    {

        _sprite = _enemyType.EnemySprite;
        _enemyName = _enemyType.EnemyName;

        _enemyScale = _enemyType.EnemyScale;
        _speed = _enemyType.EnemySpeed;
        _maxHealth = _enemyType.EnemyMaxHealth;
        _damage = _enemyType.EnemyDamage;
        _moveRadius = _enemyType.EnemyMoveRadius;

        _damagedSprite = _enemyType.DamagedEnemySprites;

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

    private void Move()
    {
        transform.position += _speed * Time.deltaTime * transform.right;
        if (Vector3.Distance(_startPosition, transform.position)>_moveRadius)
        {
            transform.Rotate(Vector3.up,180f);
        }
    }


}
