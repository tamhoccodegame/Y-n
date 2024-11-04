using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossCrocodile : MonoBehaviour
{
	[SerializeField] private GameObject effectPrefabs;
	[SerializeField] private GameObject rockPrefabs;
	public Transform effectPoint;

	private int currentComboStrikes;

	private int speed = 25;
	private int countTouchWall = 0;
	private int countChasePlayer = 0;
	private float attackRange = 5f;
	private bool isRage = false;

	[SerializeField] private float maxHealth;
	private float currentHealth;
	public Slider healthBar_slider;
	private bool isCoroutineRunning = false;

	Transform player;
	Rigidbody2D rb;
	Animator animator;
	Vector2 direction;

	public void Awake()
	{
		player = GameObject.Find("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		//currentComboStrikes = Random.Range(1, 4);
		currentComboStrikes = 1;
		direction = Vector2.right;
	}

	private void Start()
	{
		direction = direction = new Vector3(player.position.x - transform.position.x, 0, 0);
	}
	public void Update()
	{
		if (isCoroutineRunning) return;

		isCoroutineRunning = true;
		StartCoroutine("Combo" + currentComboStrikes);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Wall"))
		{
			direction.Normalize();
			direction.x *= -1;
			transform.localScale = new Vector3(direction.x * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

			isCoroutineRunning = false;
		}
	}

	private void RandomComboStrike()
	{
		currentComboStrikes = Random.Range(1, 4);
	}

	//Running left and right agressively
	private IEnumerator Combo1()
	{
		if (countTouchWall < 4)
		{
			direction.Normalize();
			animator.Play("CST_Walk");
			rb.velocity = new Vector3(speed * direction.x, 0, 0);
		}
		else
		{
			animator.Play("CST_Idle");
			rb.velocity = Vector2.zero;
			yield return new WaitForSeconds(2f);
			RandomComboStrike();
			yield return new WaitForSeconds(.5f);
			Debug.Log("Combo1 done, wait for random currentComboStrike");
			countTouchWall = 0;
			isCoroutineRunning = false;
		}

	}


	public void Chase()
	{
		animator.Play("Move");
		rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

	}


	//Chase Player and Attack
	private IEnumerator Combo2()
	{
		if (countChasePlayer < 4)
		{
			if (Mathf.Abs(player.position.x - transform.position.x) <= attackRange)
			{
				countChasePlayer++;
				rb.velocity = Vector2.zero;
				animator.Play("CST_Attack");
				yield return new WaitForSeconds(.5f);
				animator.Play("CST_Idle");
				yield return new WaitForSeconds(1f);
				isCoroutineRunning = false;
			}
			else
			{
				Chase();
				isCoroutineRunning = false;
			}

		}
		else
		{
			animator.Play("CST_Idle");
			rb.velocity = Vector2.zero;
			yield return new WaitForSeconds(1f);
			RandomComboStrike();
			yield return new WaitForSeconds(.5f);
			countChasePlayer = 0;
			isCoroutineRunning = false;
		}

	}

	//CastSkill Thunder
	private IEnumerator Combo3()
	{
		yield return null;
	}

	public  void TakeDamage(float damage)
	{

	}


	public bool Die()
	{
		return false;
	}

}