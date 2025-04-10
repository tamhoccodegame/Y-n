using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected enum State
    {
        Patrol,
        Chase,
        Attack,
        Skill,
    }
	public float attackCooldown = 1.5f;  // Thời gian hồi chiêu sau mỗi đòn tấn công
	private float nextAttackTime = 0f;  // Biến để kiểm tra thời gian cho đòn tấn công tiếp theo

	protected State currentState = State.Patrol;
    protected Transform player;
    public float chaseRange;
    public float attackRange;
    public float moveSpeed;

    protected Rigidbody2D rb;
    protected Animator animator;

    public GameObject[] patrolPoints;
    private int currentPatrolIndex = 0;
    private int lastPatrolIndex = -1;

    protected bool isCoroutineRunning = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    public virtual void Update()
	{
		if (isCoroutineRunning) return;

		UpdateState();
        Debug.Log(GetDirection(player));
		switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Skill:
                break; ;
        }

    }

    protected float GetDirection(Transform destination)
    {
        return Mathf.Sign(destination.position.x - transform.position.x);
    }

    protected void RandomPatrolIndex()
    {
		lastPatrolIndex = currentPatrolIndex; // Cập nhật lastPatrolIndex
		currentPatrolIndex = Random.Range(0, patrolPoints.Length);
		while (currentPatrolIndex == lastPatrolIndex)
		{
			currentPatrolIndex = Random.Range(0, patrolPoints.Length); // Chọn lại nếu giống last
		}
	}

    protected void MoveToTarget(Transform target)
    {
		float direction = GetDirection(target);
		rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
	}

    void Patrol()
    {
		animator.Play("Walk");
		MoveToTarget(patrolPoints[currentPatrolIndex].transform);

        if(Mathf.Abs(transform.position.x - patrolPoints[currentPatrolIndex].transform.position.x) <= 0.1f)
        {
            RandomPatrolIndex();
        }

    }

    protected void Chase()
    {
        animator.Play("Walk");
        MoveToTarget(player);
	}

	protected void Attack()
	{
		// Kiểm tra nếu đã đến thời gian có thể tấn công tiếp theo
		if (Time.time >= nextAttackTime)
		{
			isCoroutineRunning = true;
			StartCoroutine(AttackCoroutine());
			nextAttackTime = Time.time + attackCooldown;  // Đặt thời gian cho đòn tấn công tiếp theo
		}
	}

	private IEnumerator AttackCoroutine()
    {
        animator.Play("Attack");
        yield return new WaitForSeconds(1f);
        isCoroutineRunning = false;
    }

	protected virtual IEnumerator Skill()
	{
		yield return null;
	}

	protected virtual void UpdateState()
    {
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);

        if(rb.linearVelocity.x != 0 )
        {
			Vector3 currentLocalScale = transform.localScale;
			currentLocalScale.x = Mathf.Sign(rb.linearVelocity.x) * Mathf.Abs(currentLocalScale.x);
			transform.localScale = currentLocalScale;
		}
        
        switch (currentState)
        {
            case State.Patrol:
                if(distanceToPlayer <= chaseRange)
                {
                    currentState = State.Chase;
                }
                break;
            case State.Chase:
                if(distanceToPlayer <= attackRange)
                {
                    currentState = State.Attack;
                }
                else if(distanceToPlayer > chaseRange)
                {
                    currentState = State.Patrol;
                }
                break;
            case State.Attack:
                if(distanceToPlayer > attackRange)
                {
                    currentState = State.Chase;
                }
                break;
            case State.Skill:
                StartCoroutine(Skill());
                break;
        }
    }


}
