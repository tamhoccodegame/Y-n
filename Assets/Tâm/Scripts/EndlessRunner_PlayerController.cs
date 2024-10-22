using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRunner_PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Collider2D feet;
    private bool canJump = true;
    private float jumpForce = 15f;

    public float stopVelocity;
    public float fallingForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;    
        }

        if(rb.velocity.y <= stopVelocity)
        {
            rb.velocity = Vector2.up * Physics2D.gravity.y * fallingForce;
        }
    }
}
