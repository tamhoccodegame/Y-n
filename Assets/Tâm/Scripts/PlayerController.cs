using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float stopJumpVel;
    [SerializeField] private float fallingForce;

    private float moveInput = 0;

    Rigidbody2D rb;
    Collider2D col;
    Animator animator;

    public Collider2D feetBoxCast;

    public bool canJump = false;
    bool isCoroutineRunnning = false;

    public enum PlayerState {Idle, Moving, Jumping, Attacking};
    public PlayerState currentState = PlayerState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        UpdateAnimation();

        if (rb.velocity.y < stopJumpVel)
        {
            Fall();
        }

    }

    void HandleInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (isCoroutineRunnning) return;

        if (Input.GetKeyDown(KeyCode.J) && !IsInActionState())
        {
            Attack();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && currentState != PlayerState.Attacking)
        {
            Jump();
        }
        else if (moveInput != 0 && !IsInActionState())
        {
            Move(moveInput);
        }
        else if (moveInput == 0 && !IsInActionState())
        {
            ChangeState(PlayerState.Idle);
        }
        else if(moveInput != 0 && currentState == PlayerState.Jumping)
        {
            MoveInAir(moveInput);
        }
    }

    bool IsInActionState()
    {
        return currentState == PlayerState.Attacking || currentState == PlayerState.Jumping;
    }

    void UpdateAnimation()
    {
        if (currentState != PlayerState.Attacking)
            animator.SetFloat("speed", Mathf.Abs(moveInput));
        else animator.SetFloat("speed", 0);
    }

    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
    }

    void Move(float moveInput)
    {
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        ChangeState(PlayerState.Moving);

        if(moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    void MoveInAir(float moveInput)
    {
        Move(moveInput);
        ChangeState(PlayerState.Jumping);
    }

    void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            ChangeState(PlayerState.Jumping);
        }
        
    }

    public void Fall()
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * fallingForce * Time.deltaTime;
    }

    void Attack()
    {
        isCoroutineRunnning = true;
        StartCoroutine(nameof(AttackDelay));
    }

    IEnumerator AttackDelay()
    {
		ChangeState(PlayerState.Attacking);
		moveInput = 0;
		animator.SetTrigger("isAttack");
		yield return new WaitForSeconds(0.5f);
        ChangeState(PlayerState.Idle);
        isCoroutineRunnning = false;
    }

    //bool IsAttacking()
    //{
    //    return true;
    //}
}
