using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance { get { return _instance; } }
    private static CharacterController _instance;

    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float moveSpeedDown = 5f;
    [SerializeField] float moveSpeedUp = 3f;
    [SerializeField] float moveSpeed;
    [SerializeField] float GroundRadius = .5f;
    [SerializeField] List<SkillController> skillInstances;
    
    List<SkillController> skills = new List<SkillController>(); 
    
    Rigidbody2D rb;
    
    [SerializeField] bool isGrounded = false;

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

    void HorizontalMove()
    {   
        rb.velocity = new Vector3(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime*100f,rb.velocity.y);
    }

    //void FlipFace()
    //{
    //    facingRight = !facingRight;
    //    Vector3 transLocate = transform.localScale;
    //    transLocate.x *= -1;
    //    transform.localScale = transLocate;
    //}
    
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
