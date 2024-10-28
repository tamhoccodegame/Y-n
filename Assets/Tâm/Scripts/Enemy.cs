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

    protected State currentState = State.Patrol;
    public Transform player;
    public float chaseRange;
    public float attackRange;
    public float moveSpeed;

    protected Rigidbody2D rb;
    protected Animator animator;

    public GameObject[] patrolPoints;
    private int currentPatrolIndex = 0;
    private int lastPatrolIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		UpdateState();
        Debug.Log(GetDirection(player));
		switch (currentState)
        {
            case State.Patrol:
                break;
            case State.Chase:
                break;
            case State.Attack:
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
		rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
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

    void Chase()
    {
        animator.Play("Walk");
        MoveToTarget(player);
	}

    void Attack()
    {
        animator.Play("Attack");
    }
	protected virtual IEnumerator Skill()
	{
		yield return null;
	}

	void UpdateState()
    {
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);

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
