using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNghi : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float attackCooldown = 2f;
    public float attackDuration = 0.5f;
    public float cooldownTimer = 0f;

    public Transform[] patrolPoints; // Các điểm để tuần tra
    public float patrolSpeed = 2f; // Tốc độ khi tuần tra
    public float chaseSpeed = 4f; // Tốc độ khi đuổi theo
    public float detectionRange = 5f; // Phạm vi phát hiện người chơi
    public float chaseRange = 10f; // Phạm vi tối đa để đuổi theo
    public float attackRange = 1.5f; // Phạm vi tấn công
    private Transform player; // Tham chiếu đến đối tượng người chơi

    private int currentPatrolIndex = 0;
    private Vector3 startingPosition;
    private enum State { Patrolling, Chasing, Attacking, Returning }
    private State currentState;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isAttacking = false;
    private bool isFacingRight = true; // Biến lưu trạng thái hướng

    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startingPosition = transform.position;
        currentState = State.Patrolling;
        maxHealth = currentHealth;
    }

    void Update()
    {
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);
        switch (currentState)
        {
            case State.Patrolling:
                PatrolBehavior(distanceToPlayer);
                break;
            case State.Chasing:
                ChaseBehavior(distanceToPlayer);
                break;
            case State.Attacking:
                AttackBehavior(distanceToPlayer);
                break;
            case State.Returning:
                ReturnBehavior();
                break;
        }
	}

    [ContextMenu("TakeDamage")]
    public void TakeDamge()
	{
        GetComponent<SimpleFlash>().Flash();
        currentHealth--;
        currentHealth = Mathf.Max(currentHealth, 0);
        if(currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void HandleFlip()
    {
        if (rb.velocity.x != 0 && !isAttacking)
        {
			Vector3 currentLocalScale = transform.localScale;
			currentLocalScale.x = Mathf.Abs(currentLocalScale.x) * Mathf.Sign(rb.velocity.x);
			transform.localScale = currentLocalScale;
		}
	}


    void PatrolBehavior(float distanceToPlayer)
    {
        MoveTo(patrolPoints[currentPatrolIndex].position, patrolSpeed);
		// Kiểm tra nếu đã tới điểm tuần tra hiện tại
		if (Mathf.Abs(transform.position.x - patrolPoints[currentPatrolIndex].position.x) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        // Chuyển sang trạng thái Chasing nếu người chơi trong phạm vi phát hiện
        if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chasing;
		}
		HandleFlip();

	}

	void ChaseBehavior(float distanceToPlayer)
	{
		MoveTo(player.position, chaseSpeed); 
											 
		if (distanceToPlayer <= attackRange)
        {
            currentState = State.Attacking;
            //rb.velocity = Vector2.zero; // Dừng lại để tấn công
        }
        // Nếu người chơi ra khỏi phạm vi chaseRange, quay về tuần tra
        if (distanceToPlayer > chaseRange)
        {
            currentState = State.Returning;
		}
		HandleFlip();

	}

	void AttackBehavior(float distanceToPlayer)
    {
        // Nếu đang ở tầm đánh, thực hiện đòn tấn công
        if (!isAttacking)
        {
            CheckTime();
            //StartCoroutine(PerformAttack());
        }

        // Nếu người chơi ra khỏi tầm đánh nhưng vẫn trong phạm vi đuổi theo
        if (distanceToPlayer > attackRange && distanceToPlayer <= chaseRange)
        {
            currentState = State.Chasing;
        }
    }

    public void CheckTime()
    {
        //Kiem tra neu con thoi gian cooldown, giam bo dem
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        //Neu dang tan cong va bo dem thoi gian cooldown da ket thuc
        //float distanceToPlayer = Vector3.Distance(transform.position, player.position); distanceToPlayer <= attackRange && 
        if (!isAttacking && cooldownTimer <= 0)
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        rb.velocity = Vector2.zero;
        isAttacking = true;
        // Giả lập đòn tấn công (thời gian delay giữa các đòn tấn công)
        animator.SetTrigger("isAttack");
        Debug.Log("Enemy attacked!");

        //Trong luc don danh dien ra
        yield return new WaitForSeconds(attackDuration);
        //Ket thuc don danh, bat dau cooldown
        isAttacking = false;
        cooldownTimer = attackCooldown;
        Debug.Log("Enemy finished attacking and is now on cooldown");
    }



    void ReturnBehavior()
    {
        MoveTo(startingPosition, patrolSpeed);

        // Nếu đã trở lại vị trí ban đầu, chuyển về tuần tra
        if (Mathf.Abs(transform.position.x - startingPosition.x) < 0.5f)
        {
            currentState = State.Patrolling;
        }
    }

    void MoveTo(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        rb.velocity = new Vector2(speed * direction.x, rb.velocity.y);
    }

	private void OnDrawGizmos()
	{
        Gizmos.DrawWireSphere(transform.position, detectionRange);
	}
}
