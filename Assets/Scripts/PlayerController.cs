using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get { return _instance; } }
    private static PlayerController _instance;

    [SerializeField] 
    float jumpSpeed = 3f;
    
    [SerializeField] 
    float moveSpeedDown = 5f;
    
    [SerializeField] 
    float moveSpeedUp = 3f;
    [SerializeField] 
    float moveSpeed;
    [SerializeField] 
    float GroundRadius = .5f;
    
    [SerializeField] List<SkillController> skillInstances;
    
    List<SkillController> skills = new List<SkillController>(); 
    
    Rigidbody2D rb;
    
    [SerializeField] 
    bool isGrounded = false;

    [SerializeField] LayerMask GroundLayer;

    private int level = 1;

    private void Awake()
    {
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

        for(int i = 0; i < skillInstances.Count; i++)
        {
            skills.Add(Instantiate(skillInstances[i], transform));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
        }
        SkillInputController();
        TurnAround();
    }


    private void FixedUpdate()
    {
        OnGroundCheck();

        if (isGrounded)
            moveSpeed = moveSpeedDown;
        else
            moveSpeed = moveSpeedUp;

        HorizontalMove();

    }

    private void TurnAround()
    {
        float direction = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        if (direction>0 && transform.localEulerAngles.y != 0f)
        {
            transform.localEulerAngles = new Vector3(0f,0f,0f);
        }
        if (direction < 0 && transform.localEulerAngles.y != -180f)
        {
            transform.localEulerAngles = new Vector3(0f, -180f, 0f);
        }
        transform.position = pos;
    }

    void HorizontalMove()
    {
        rb.velocity = new Vector3(moveSpeed*transform.right.x*Time.deltaTime*100f,rb.velocity.y);
    }

    public Vector2 GetDirection()
    {
        return transform.right;
    }

    
    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, GroundRadius, GroundLayer);
    }
    
    public int GetLevel()
    {
        return level;
    }
    
    public void LevelCompleted()
    {
        level++;
    }
    private void SkillInputController()
    {
        for(int i = 0; i < skills.Count; i++)
        {
            if (Input.GetKeyDown(skills[i].GetSkillKey()))
            {
                skills[i].SpawnBullet();
            }

        }
    }
}
