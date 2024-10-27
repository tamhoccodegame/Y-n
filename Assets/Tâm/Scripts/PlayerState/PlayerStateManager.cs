using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public IPlayerState PlayerState;

    public readonly IdleState idleState = new IdleState();
	public readonly WalkingState walkingState = new WalkingState();
	public readonly JumpingState jumpingState = new JumpingState();
	public readonly AttackingState attackingState = new AttackingState();
	public readonly CarryingIdleState carryingIdleState = new CarryingIdleState();
	public readonly CarryingWalkingState carryingWalkingState = new CarryingWalkingState();

	public float speed;
	public float jumpForce;
	public float stopJumpVel;
	public float fallingForce;

	public float maxJumpTime;
	public float jumpTimeCounter;

	public float MoveInput {  get; private set; }

	public Rigidbody2D rb { get; private set; }
	public Collider2D col { get; private set; }
	public Animator animator { get; private set; }

	public bool isGrounded = true;
	bool isCoroutineRunnning = false;


	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		animator = GetComponent<Animator>();

        PlayerState = idleState;
        PlayerState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
	{
		MoveInput = Input.GetAxisRaw("Horizontal");
		PlayerState.UpdateState(this);
    }

	public void HandleMovement()
	{
		// Cập nhật vận tốc dựa trên MoveInput và tốc độ
		rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
	}


	public void SwitchState(IPlayerState newState)
    {
        PlayerState.ExitState(this);
        PlayerState = newState;
        PlayerState.EnterState(this);
    }
}
