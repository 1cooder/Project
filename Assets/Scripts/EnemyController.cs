using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    //Getting scriptable object
    [SerializeField] 
    private EnemyData _enemyType = null;

    public HealthBarBehaviour HealthBar;

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
    
	public Transform target;
	int MoveSpeed = 4;
    int MaxDist = 7;
    int MinDist = 4;
	Animator m_Animator;
	private bool inRange;
	


    private void Awake()
    {
        InitEnemyFromScriptableObject();
        _health = _maxHealth;
        Debug.Log(_health);
       HealthBar.SetHealthBar(_health, _maxHealth);

        _startPosition = transform.position;
    }

    private void Start()
    {

        GetComponent<SpriteRenderer>().sprite = _sprite;
        transform.localScale = _enemyScale;
		m_Animator = gameObject.GetComponent<Animator>();

        
    }

    private void Update()
    {
        //Move();
		
		Attack();

    }
	
	   
    public void Attack()  //Enemy attack
    {
		
        if (inRange)
		{
     
	 		if (Vector2.Distance(transform.position, target.position) >= MinDist)
			{
				
				   //transform.Translate (Vector2.right * MoveSpeed * Time.deltaTime);
		    Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
			   
				          m_Animator.SetTrigger("toIdle");
				
				
				 if (Vector2.Distance(transform.position, target.position) <= MaxDist)
					{
						 
						  m_Animator.SetTrigger("toAttack");
						 
					}

			}
		//Enemy face flip
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x) 
        {
            rotation.y = 180;
        }
        else
        {
            //Debug.Log("Twist");
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;		
		
		
		
		
		
		
		
		}
 } 	 		
	
  public  void OnTriggerEnter2D(Collider2D trig)
  {
	       
	   if (trig.gameObject.tag == "Player")
	   {
	    
		inRange = true;
	   }
	   
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

        HealthBar.SetHealthBar(_health, _maxHealth);

    }

    //Enemy destroy/died
    public void DestroyEnemy()
    {

        if(_health <= 0)
        {
            //Destroy(this.gameObject);
            
			
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
        Vector3 nextPosition = transform.position +_speed * Time.deltaTime * transform.right;
        if (Vector3.Distance(_startPosition, nextPosition) >_moveRadius)
        {
            transform.Rotate(Vector3.up,180f);
        }
        else
        {
            transform.position = nextPosition;
        }
    }


}
