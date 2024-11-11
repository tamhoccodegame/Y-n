using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossCrocodile : MonoBehaviour
{
	public float speed;
	public int maxHealth;
	private int currentHealth;
	private Transform player;

	Animator animator;
	Rigidbody2D rb;

	private bool isCoroutineRunnning = false;
	private int currentSequenceIndex = -1;
	private int currentCountTouchWall;

	public float attackRange;

	[Header("======SkillSpawnPoint======")]
	public Transform waveSlashPoint;
	public Transform bubblePoint;

	[Header("======SkillPrefabs======")]
	public GameObject waveSlashPrefab;
	public GameObject bubblePrefab;

	private void OnEnable()
	{
		player = GameObject.Find("Player").transform;
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Debug.Log(currentSequenceIndex);
		if (isCoroutineRunnning) return;
		RandomSequence();
		isCoroutineRunnning = true;
		StartCoroutine($"Sequence_{currentSequenceIndex}");
	}

	void RandomSequence()
	{
		//currentSequenceIndex = Random.Range(1,4);
		currentSequenceIndex = 3;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Wall"))
		{
			speed = -speed;
			currentCountTouchWall++;
		}
	}

	float DistanceFromTarget(Transform target)
	{
		return Mathf.Abs(transform.position.x - target.position.x);
	}

	void LookAtTarget(Transform target)
	{
		Vector3 currentLocalScale = transform.localScale;
		float direction = target.position.x - transform.position.x;

		currentLocalScale.x = Mathf.Sign(direction) * Mathf.Abs(transform.localScale.x);
		transform.localScale = currentLocalScale;
	}

	void LookAtTarget(float targetDirection)
	{
		Vector3 currentLocalScale = transform.localScale;
		currentLocalScale.x = Mathf.Sign(targetDirection) * Mathf.Abs(transform.localScale.x);
		transform.localScale = currentLocalScale;
	}

	void Move()
	{
		animator.Play("CST_Walk");
		rb.velocity = new Vector2(speed, 0);
		LookAtTarget(speed);
	}

	//Chạy 2 bên chọc tức
	IEnumerator Sequence_1()
	{
		int countTouchWall = Random.Range(1, 4);
		currentCountTouchWall = 0;

		while(currentCountTouchWall < countTouchWall)
		{
			Move();
			yield return null;
		}

		isCoroutineRunnning = false;

	}

	public void SpawnWaveSlash()
	{
		Vector2 playerPosition = player.position;
		
		Rigidbody2D rb = Instantiate(waveSlashPrefab, waveSlashPoint.position, Quaternion.identity, waveSlashPoint).GetComponent<Rigidbody2D>();
		Vector2 direction = (playerPosition - (Vector2)waveSlashPoint.position).normalized;

		// Tăng tốc độ của bong bóng với vận tốc ban đầu và áp dụng lực
		float initialSpeed = 20f;  // Tốc độ ban đầu có thể thay đổi tùy nhu cầu
		Vector2 initialVelocity = new Vector2(direction.x * initialSpeed, direction.y * initialSpeed + 20f); // Cộng thêm độ cong hướng lên

		rb.velocity = initialVelocity;

		// Bật trọng lực để bong bóng sẽ rơi xuống sau khi đạt đỉnh
		rb.gravityScale = 3.0f;  
	}
	//Chém sóng xung kích
	IEnumerator Sequence_2()
	{
		while (DistanceFromTarget(player) < attackRange)
		{
			Move();
			yield return null;
		}

		rb.velocity = Vector2.zero;

		for(int i = 0; i < 4; i++)
		{
			LookAtTarget(player);
			animator.Play("CST_Attack");
			//Chờ play anim attack
			yield return new WaitForSeconds(1f);
			animator.Play("CST_Idle");
			yield return new WaitForSeconds(2f);
		}

		isCoroutineRunnning = false;
	}

	public void SpawnBubble()
	{
		Rigidbody2D rb = Instantiate(bubblePrefab, bubblePoint.position, Quaternion.identity, bubblePoint).GetComponent<Rigidbody2D>();
		float direction = Mathf.Sign(player.position.x - transform.position.x);
		Debug.Log(direction);
		float randomScaleValue = Random.Range(2, 4);
		rb.gameObject.transform.localScale = new Vector3(randomScaleValue, randomScaleValue, 0);
		rb.velocity = new Vector2(Random.Range(5,11) * direction, Random.Range(3,8));
	}

	//Thổi bong bóng
	IEnumerator Sequence_3()
	{
		while (DistanceFromTarget(player) < attackRange)
		{
			Move();
			yield return null;
		}
		rb.velocity = Vector2.zero;
		LookAtTarget(player);

		animator.Play("CST_ThoiBong");
		yield return new WaitForSeconds(4f);
		animator.Play("CST_Idle");
		yield return new WaitForSeconds(4f);

		isCoroutineRunnning = false;

	}

}