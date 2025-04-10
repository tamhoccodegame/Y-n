using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCST : MonoBehaviour
{	
	public float moveSpeed = 5f;             // Tốc độ di chuyển
	public float attackCooldown = 2f;        // Thời gian hồi chiêu cho đòn tấn công
	public GameObject energyWavePrefab;      // Prefab của sóng năng lượng
	public Transform player;                  // Tham chiếu đến người chơi
	public float chaseRange = 10f;           // Khoảng cách để boss bắt đầu theo đuổi người chơi
	Animator animator;
	private Rigidbody2D rb;
	private bool isAttacking = false;        // Kiểm tra trạng thái tấn công

	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player").transform;
		StartCoroutine(MoveCrazy());  // Bắt đầu coroutine di chuyển điên cuồng
	}

	void Update()
	{
		// Nếu boss ở trong khoảng cách chấp nhận, nó có thể thực hiện các hành động tấn công
		float distanceToPlayer = Vector2.Distance(transform.position, player.position);
		if (distanceToPlayer <= chaseRange)
		{
			if (!isAttacking)
			{
				StartCoroutine(AttackSequence());
			}
		}
	}

	// Coroutine để di chuyển điên cuồng qua hai bên
	private IEnumerator MoveCrazy()
	{
		while (true)
		{
			float direction = Random.Range(-1f, 1f);
			rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
			yield return new WaitForSeconds(1f); // Thay đổi hướng mỗi giây
		}
	}

	// Coroutine để chém ra sóng năng lượng
	private IEnumerator SlashEnergyWave()
	{
		while (true)
		{
			isAttacking = true;
			animator.Play("Attack"); // Gọi animation chém (nếu có)
			yield return new WaitForSeconds(0.5f); // Thời gian thực hiện động tác chém

			// Tạo sóng năng lượng
			Instantiate(energyWavePrefab, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(attackCooldown); // Thời gian hồi chiêu
			isAttacking = false;
			break; // Kết thúc coroutine này sau khi thực hiện một lần
		}
	}

	// Coroutine để nhảy và dậm chân tại chỗ
	private IEnumerator JumpAndStomp()
	{
		isAttacking = true;
		rb.AddForce(Vector2.up * 300f); // Nhảy lên cao
		yield return new WaitForSeconds(0.5f); // Chờ một chút trước khi dậm chân

		// Dậm chân
		rb.linearVelocity = new Vector2(rb.linearVelocity.x, -moveSpeed);
		yield return new WaitForSeconds(0.5f); // Thời gian dậm chân

		isAttacking = false;
	}

	// Coroutine để quản lý chuỗi tấn công
	private IEnumerator AttackSequence()
	{
		yield return StartCoroutine(SlashEnergyWave());
		yield return StartCoroutine(JumpAndStomp());
		yield return new WaitForSeconds(1f); // Thời gian chờ trước khi có thể tấn công lại
	}
}

