using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get { return _instance; } }
    private static PlayerController _instance;
    public HealthBarBehaviour _healthBar;


    [SerializeField] 
    private float jumpSpeed = 3f;
    [SerializeField] 
    float _moveSpeedDown = 5f;
    [SerializeField] 
    private float _moveSpeedUp = 3f;
    [SerializeField] 
    private float _moveSpeed;
    [SerializeField] 
    private float _groundRadius = .5f;
    [SerializeField]
    private float _enemyHitForce = 5f;
    [SerializeField]
    private float _delayAfterGotHit = 2f;

    [SerializeField]
    private float _maxHealth = 100f;

    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    Transform _skillSpawnPosition;
    
    [SerializeField] List<SkillController> _skillInstances;
    
    List<SkillController> _skills = new List<SkillController>(); 
    
    Rigidbody2D rb;
    
    [SerializeField] 
    bool _isGrounded = false;

    [SerializeField] 
    private LayerMask _groundLayer;
    [SerializeField] 
    private LayerMask _enemyLayer;


    private int level = 1;
    private float _direction = 0f;
	
	Animator m_Animator;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        Debug.Log(_currentHealth);
        _healthBar.SetHealthBar(_currentHealth, _maxHealth);


        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        //DontDestroyOnLoad(this.gameObject);
        
        rb = GetComponent<Rigidbody2D>();

        for(int i = 0; i < _skillInstances.Count; i++)
        {
            _skills.Add(Instantiate(_skillInstances[i], transform));
        }   
    }

    private void Start()
    {

		m_Animator = gameObject.GetComponent<Animator>();

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
        }
        SkillInputController();
        _direction = Input.GetAxisRaw("Horizontal");

        TurnAround();
    }


    private void FixedUpdate()
    {
        OnGroundCheck();

        if (_isGrounded)
            _moveSpeed = _moveSpeedDown;
        else
            _moveSpeed = _moveSpeedUp;

        HorizontalMove();

    }

    private void TurnAround()
    {
        if (_direction>0 && transform.localEulerAngles.y != 0f)
        {
            transform.localEulerAngles = new Vector3(0f,0f,0f);
        }
        if (_direction < 0 && transform.localEulerAngles.y == 0f)
        {
            transform.localEulerAngles = new Vector3(0f, -180f, 0f);
        }
    }

    void HorizontalMove()
    {
        if (_direction != 0f)
        {
            rb.velocity = new Vector2(_moveSpeed * transform.right.x * Time.deltaTime * 100f, rb.velocity.y);
			 
			 m_Animator.ResetTrigger("toWalk");
			 m_Animator.SetTrigger("toWalk");
        }
        else
        {
            rb.velocity = new Vector2(0f,rb.velocity.y);
			
			 m_Animator.ResetTrigger("toIDLE");
			 m_Animator.SetTrigger("toIDLE");
        }
    }

    public Vector2 GetDirection()
    {
        return transform.right;
    }

    
    void OnGroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(transform.position, _groundRadius, _groundLayer);
    }
    
    public int GetLevel()
    {
        return level;
    }
    
    public void LevelCompleted()
    {
        level++;
    }

    public Vector3 GetSkillSpawnPosition()
    {
        return _skillSpawnPosition.position;
    }

    private void SkillInputController()
    {
        for(int i = 0; i < _skills.Count; i++)
        {
            if (Input.GetKeyDown(_skills[i].GetSkillKey()))
            {
                _skills[i].SpawnBullet();
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_enemyLayer & (1 << collision.gameObject.layer)) !=0)
        {
            rb.AddForce(-transform.right*_enemyHitForce);
            StartCoroutine(BackToNormal());
        }
    }
    IEnumerator BackToNormal()
    {
        gameObject.layer = LayerMask.NameToLayer("SafeLayer");
        yield return new WaitForSeconds(_delayAfterGotHit);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public enum PlayerStatus
    {
        Idle,
        GotHit,
    }

    public void TakeHit(float hit)
    {
        _currentHealth -= hit;

        _healthBar.SetHealthBar(_currentHealth, _maxHealth);
    }

}
