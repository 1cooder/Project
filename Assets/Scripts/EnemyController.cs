using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] 
    private EnemyData _enemyType = null;

    public HealthBarBehaviour HealthBar;
    Sprite _sprite;
    Sprite[] _damagedSprite;

    Vector2 _enemyScale;

    Vector3 _startPosition = Vector3.zero;

    private float _speed;
    private float _maxHealth;
    private float _health;
    private float _damage;
    private float _moveRadius;
    private float _chaseRadius;
    
    string _enemyName;
    
	private Transform target = null;
    int MaxDist = 6;
    int MinDist = 4;
	[SerializeField]
    Animator m_Animator;


    private void Awake()
    {
        InitEnemyFromScriptableObject();
        _health = _maxHealth;
		HealthBar.SetHealthBar(_health, _maxHealth);

        _startPosition = transform.position;
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _sprite;
        transform.localScale = _enemyScale;
		m_Animator = gameObject.GetComponent<Animator>();
        target = PlayerController.Instance.transform;
    }

    private void Update()
    {
        Move();
    }
	
	//Enemy attack
    void Attack()
    {
        if (Vector2.Distance(transform.position, target.position) <= MaxDist)
        {
            m_Animator.SetTrigger("toAttack");
        }
        float directionY = transform.TransformPoint(target.position).x;
        if (directionY > 0 && transform.localRotation.y != 0f)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f,0f));
            return;
        }

        if (directionY < 0 && transform.localRotation.y != 180f)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f,0f));
        }

    }
	
	

    //Getting Enemy values from scriptable object
    public void InitEnemyFromScriptableObject()
    {

        _sprite = _enemyType._enemySprite;
        _enemyName = _enemyType._Name;

        _enemyScale = _enemyType._enemyScale;
        _speed = _enemyType._speed;
        _maxHealth = _enemyType._maxHealth;
        _damage = _enemyType._damage;
        _moveRadius = _enemyType._patrollingRadius;
        _chaseRadius = _enemyType._chaseRadius;
        _damagedSprite = _enemyType._damagedSprites;

    }

    //Enemy take hit
    public void TakeHit(float hit)
    {
        _health -= hit;
        ChangeSpriteOnDamage();
        DestroyEnemy();
        HealthBar.SetHealthBar(_health, _maxHealth);
    }

    //Enemy destroy/died
    public void DestroyEnemy()
    {
        if(_health <= 0)
        {	
			m_Animator.SetTrigger("toDIE");
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
        if (Vector3.Distance(transform.position, target.position) < _chaseRadius )
        {
            Attack();
            _startPosition = transform.position;
            return;
        }
        Vector3 nextPosition = transform.position +_speed * Time.deltaTime * transform.right;
        if (Vector3.Distance(_startPosition, nextPosition) >_moveRadius)
        {
            _startPosition = transform.position;
            transform.Rotate(Vector3.up,180f);
        }
        else
        {
            transform.position = nextPosition;
        }
    }
}
