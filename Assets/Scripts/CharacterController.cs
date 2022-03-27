using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float distToGround;
    public float Jump_speed = 3f;
    public float Move_speed = 20f;
    Rigidbody2D rb;
    bool facingRight = true;
    public bool isGrounded = false;

    public Transform GroundPosition;
    public float GroundRadius;
    public LayerMask GroundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        OnGroundCheck();
        if (isGrounded == false)
        {
            Move_speed = 3;
        }
        else
        {
            Move_speed = 5;    
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rb.velocity.y,0))
        {
            rb.AddForce(Vector3.up * Jump_speed, ForceMode2D.Impulse);

        }
        
        
        //if (rb.velocity.x < 0 && facingRight)
        //{
        //    FlipFace();
        //}
        //else if (rb.velocity.x > 0 && !facingRight)
        //{
        //    FlipFace();
        //} 
    }


    private void FixedUpdate()
    {
        HorizontalMove();
    }

    void HorizontalMove()
    {
        // Movement Horizontal Axis
        rb.velocity = new Vector2(Move_speed * Input.GetAxis("Horizontal"), rb.velocity.y);
    }
    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 transLocate = transform.localScale;
        transLocate.x *= -1;
        transform.localScale = transLocate;
    }
    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GroundPosition.position, GroundRadius, GroundLayer);
    }
    
}
