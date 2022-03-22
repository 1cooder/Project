using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float Jump_speed = 3f;
    public float Move_speed = 20f;
    Rigidbody2D rb;
    bool facingRight = true;
    bool isGrounded = false;

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
        if (rb.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }
        else if (rb.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }
        if (Input.GetAxis("Vertical") > 0 && isGrounded)
        {
            Jump();
        }
    }


    private void FixedUpdate()
    {
        HorizontalMove();
    }

    void HorizontalMove()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * Move_speed, rb.velocity.y);
    }

    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 transLocate = transform.localScale;
        transLocate.x *= -1;
        transform.localScale = transLocate;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, Jump_speed));
    }

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GroundPosition.position, GroundRadius, GroundLayer);
    }
}
