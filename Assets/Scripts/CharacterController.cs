using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float moveSpeedDown = 5f;
    [SerializeField] float moveSpeedUp = 3f;
    [SerializeField] float moveSpeed;
    [SerializeField] float GroundRadius = .5f;

    Rigidbody2D rb;
    //bool facingRight = true;
    
    [SerializeField] bool isGrounded = false;

    [SerializeField] LayerMask GroundLayer;

    private int level = 1;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
        }
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
}
