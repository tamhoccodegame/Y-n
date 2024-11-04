using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class BossCrocodile : MonoBehaviour
{
	public float moveSpeed = 3f;
	public float chaseSpeed = 5f;
	public float attackRange = 1.5f;
	public float patrolRange = 5f;
	public float minDistanceFromPlayerForBubble = 5f;
	public int minAttackCount = 1;
	public int maxAttackCount = 3;
	public GameObject bubblePrefab;
	public Transform bubbleSpawnPoint;
	public float attackInterval = 2f;

	private Vector2 initialPosition;
	private bool movingRight = true;
	private Animator animator;
	private GameObject player;
	private Rigidbody2D rb;

	void Start()
	{
		initialPosition = transform.position;
		animator = GetComponent<Animator>();
		player = GameObject.FindWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0; // Đảm bảo đứng trên mặt phẳng
		rb.freezeRotation = true;
		StartCoroutine(BossRoutine());
	}

	IEnumerator BossRoutine()
	{
		while (true)
		{
			// Di chuyển qua lại
			yield return StartCoroutine(Patrol());

			// Đuổi và tấn công người chơi
			yield return StartCoroutine(ChaseAndAttack());

			// Tấn công bằng Bubble
			yield return StartCoroutine(BlowBubble());

			yield return new WaitForSeconds(attackInterval);
		}
	}

	IEnumerator Patrol()
	{
		while (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(initialPosition.x, 0)) < patrolRange)
		{
			Move();
			yield return null;
		}
	}

	void Move()
	{
		animator.Play("Walk"); // Play animation di chuyển
		float moveDirection = movingRight ? 1 : -1;
		rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);

		if (movingRight && transform.position.x >= initialPosition.x + patrolRange)
		{
			movingRight = false;
			Flip();
		}
		else if (!movingRight && transform.position.x <= initialPosition.x - patrolRange)
		{
			movingRight = true;
			Flip();
		}
	}

	IEnumerator ChaseAndAttack()
	{
		int attackCount = Random.Range(minAttackCount, maxAttackCount + 1);
		animator.Play("Run");

		for (int i = 0; i < attackCount; i++)
		{
			// Đuổi theo người chơi chỉ theo trục X
			while (Mathf.Abs(transform.position.x - player.transform.position.x) > attackRange)
			{
				Vector2 direction = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x), 0);
				rb.velocity = new Vector2(chaseSpeed * direction.x, rb.velocity.y);
				FacePlayer();
				yield return null;
			}

			// Tấn công người chơi
			rb.velocity = Vector2.zero; // Dừng lại để tấn công
			animator.Play("Attack"); // Play chém animation
			yield return new WaitForSeconds(0.5f); // Đợi animation hoàn thành
		}
	}

	IEnumerator BlowBubble()
	{
		// Di chuyển ra xa khỏi người chơi nếu quá gần
		if (Mathf.Abs(transform.position.x - player.transform.position.x) < minDistanceFromPlayerForBubble)
		{
			Vector2 direction = new Vector2(Mathf.Sign(transform.position.x - player.transform.position.x), 0);
			float moveDistance = minDistanceFromPlayerForBubble - Mathf.Abs(transform.position.x - player.transform.position.x);

			while (moveDistance > 0)
			{
				float moveStep = Mathf.Min(moveSpeed * Time.deltaTime, moveDistance);
				rb.velocity = new Vector2(moveSpeed * direction.x, rb.velocity.y);
				moveDistance -= moveStep;
				yield return null;
			}
		}

		// Quay mặt về phía người chơi và thổi bong bóng
		FacePlayer();
		animator.Play("Attack_Bubble");
		yield return new WaitForSeconds(0.5f); // Đợi animation bắt đầu
		Instantiate(bubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
		yield return new WaitForSeconds(1f); // Đợi animation hoàn thành
	}

	void FacePlayer()
	{
		Vector3 scale = transform.localScale;
		scale.x = (player.transform.position.x > transform.position.x) ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
		transform.localScale = scale;
	}

	void Flip()
	{
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
