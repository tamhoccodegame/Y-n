using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	public Transform player;
	public float moveSpeed = 3f;
	public float jumpForce = 5f;
	public float detectionRange = 10f;
	public float groundCheckDistance = 1f;
	public float obstacleCheckDistance = 1f;  // Khoảng cách để kiểm tra chướng ngại vật
	public LayerMask groundLayer;

	private Rigidbody2D rb;
	private bool isGrounded = false;
	private bool isChasing = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		float distanceToPlayer = Vector2.Distance(transform.position, player.position);

		// Check if player is in range
		if (distanceToPlayer < detectionRange)
		{
			isChasing = true;
		}
		else
		{
			isChasing = false;
		}

		if (isChasing)
		{
			ChasePlayer();
		}
	}

	void ChasePlayer()
	{
		Vector2 direction = (player.position - transform.position).normalized;

		// Move towards player
		rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

		// Check if the player is above or below
		if (isGrounded && IsObstacleInFront())
		{
			Jump();  // Chỉ nhảy khi gặp chướng ngại vật
		}

		// Check if there is no ground ahead (to fall down)
		//if (!IsGroundInFront() && player.position.y < transform.position.y)
		//{
		//	rb.velocity = new Vector2(rb.velocity.x, -Mathf.Abs(jumpForce));  // Cho phép rơi xuống
		//}

		// Check if grounded
		RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
		isGrounded = groundCheck.collider != null;
	}

	bool IsObstacleInFront()
	{
		// Kiểm tra chướng ngại vật phía trước mặt
		RaycastHit2D obstacleCheck = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(rb.velocity.x), obstacleCheckDistance, groundLayer);
		return obstacleCheck.collider != null;
	}

	bool IsGroundInFront()
	{
		// Kiểm tra có mặt đất phía trước không (để tránh rơi vào "vách")
		Vector2 groundCheckPos = new Vector2(transform.position.x + Mathf.Sign(rb.velocity.x) * obstacleCheckDistance, transform.position.y);
		RaycastHit2D groundCheck = Physics2D.Raycast(groundCheckPos, Vector2.down, groundCheckDistance, groundLayer);
		return groundCheck.collider != null;
	}

	void Jump()
	{
		rb.velocity = new Vector2(rb.velocity.x, jumpForce);
	}

	//private void OnDrawGizmos()
	//{
	//	// Visualize detection range
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawWireSphere(transform.position, detectionRange);

	//	// Visualize ground and obstacle checks
	//	Gizmos.color = Color.green;
	//	Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);  // Ground check
	//	Gizmos.DrawLine(transform.position, transform.position + Vector3.right * Mathf.Sign(rb.velocity.x) * obstacleCheckDistance);  // Obstacle check	
	//}
}
