using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackCooldown = 2f;
    public float attackDuration = 0.5f;
    public float cooldownTimer = 0f;

    public Transform[] patrolPoints; // Các điểm để tuần tra
    public float patrolSpeed = 2f; // Tốc độ khi tuần tra
    public float chaseSpeed = 4f; // Tốc độ khi đuổi theo
    public float detectionRange = 5f; // Phạm vi phát hiện người chơi
    public float chaseRange = 10f; // Phạm vi tối đa để đuổi theo
    public float attackRange = 1.5f; // Phạm vi tấn công
    public Transform player; // Tham chiếu đến đối tượng người chơi

    private int currentPatrolIndex = 0;
    private Vector3 startingPosition;
    private enum State { Patrolling, Chasing, Attacking, Returning }
    private State currentState;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private bool isFacingRight = true; // Biến lưu trạng thái hướng

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        currentState = State.Patrolling;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        HandleFlip();

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

    void HandleFlip()
    {
        // Lấy hướng di chuyển của kẻ thù
        Vector3 direction = player.position - transform.position;

        // Kiểm tra nếu đang đi về bên phải nhưng chưa hướng mặt về bên phải
        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        // Kiểm tra nếu đang đi về bên trái nhưng chưa hướng mặt về bên trái
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Đảo ngược giá trị isFacingRight
        isFacingRight = !isFacingRight;

        // Đảo hướng scale trên trục X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void PatrolBehavior(float distanceToPlayer)
    {
        MoveTo(patrolPoints[currentPatrolIndex].position, patrolSpeed);

        // Kiểm tra nếu đã tới điểm tuần tra hiện tại
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        // Chuyển sang trạng thái Chasing nếu người chơi trong phạm vi phát hiện
        if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chasing;
        }
    }

    void ChaseBehavior(float distanceToPlayer)
    {
        // Kiểm tra nếu người chơi trong tầm đánh
        if (distanceToPlayer <= attackRange)
        {
            currentState = State.Attacking;
            rb.velocity = Vector2.zero; // Dừng lại để tấn công
        }
        else
        {
            MoveTo(player.position, chaseSpeed); // Đuổi theo nếu chưa đủ gần
        }

        // Nếu người chơi ra khỏi phạm vi chaseRange, quay về tuần tra
        if (distanceToPlayer > chaseRange)
        {
            currentState = State.Returning;
        }
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
        // Nếu người chơi ra khỏi phạm vi đuổi theo
        else if (distanceToPlayer > chaseRange)
        {
            currentState = State.Returning;
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

    //IEnumerator Attack()
    //{
    //    Animator walkAnimation = GetComponent<Animator>();
    //    walkAnimation.SetBool("isWalk", false);
    //    isAttacking = true;

    //    //Bat dau don danh 
    //    Animator attackAnimation = GetComponent<Animator>();
    //    attackAnimation.SetTrigger("isAttack");
    //    Debug.Log("Enemy attacked!");
    //    //Trong luc don danh dien ra
    //    yield return new WaitForSeconds(attackDuration);
    //    //Ket thuc don danh, bat dau cooldown
    //    isAttacking = false;
    //    cooldownTimer = attackCooldown;
    //    Debug.Log("Enemy finished attacking and is now on cooldown");
    //}

    IEnumerator PerformAttack()
    {
        Animator walkAnimation = GetComponent<Animator>();
        walkAnimation.SetBool("isWalk", false);
        isAttacking = true;
        // Giả lập đòn tấn công (thời gian delay giữa các đòn tấn công)
        Animator attackAnimation = GetComponent<Animator>();
        attackAnimation.SetTrigger("isAttack");
        Debug.Log("Enemy attacked!");

        //yield return new WaitForEndOfFrame();
        //attackAnimation.ResetTrigger("isAttack");
        yield return new WaitForSeconds(2f); // Thời gian delay giữa các đòn tấn công

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
        if (Vector2.Distance(transform.position, startingPosition) < 0.2f)
        {
            currentState = State.Patrolling;
        }
    }

    void MoveTo(Vector3 target, float speed)
    {
        Animator walkAnimation = GetComponent<Animator>();
        walkAnimation.SetBool("isWalk", true);
        Vector3 direction = (target - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }


    //public Transform[] patrolPoints; // Các điểm để tuần tra
    //public float patrolSpeed = 2f; // Tốc độ khi tuần tra
    //public float chaseSpeed = 4f; // Tốc độ khi đuổi theo
    //public float detectionRange = 5f; // Phạm vi phát hiện người chơi
    //public float chaseRange = 10f; // Phạm vi tối đa để đuổi theo
    //public Transform player; // Tham chiếu đến đối tượng người chơi

    //private int currentPatrolIndex = 0;
    //private Vector3 startingPosition;
    //private enum State { Patrolling, Chasing, Returning }
    //private State currentState;
    //private Rigidbody2D rb;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    startingPosition = transform.position;
    //    currentState = State.Patrolling;
    //}

    //void Update()
    //{
    //    float distanceToPlayer = Vector2.Distance(transform.position, player.position);

    //    switch (currentState)
    //    {
    //        case State.Patrolling:
    //            PatrolBehavior(distanceToPlayer);
    //            break;
    //        case State.Chasing:
    //            ChaseBehavior(distanceToPlayer);
    //            break;
    //        case State.Returning:
    //            ReturnBehavior();
    //            break;
    //    }
    //}

    //void PatrolBehavior(float distanceToPlayer)
    //{
    //    MoveTo(patrolPoints[currentPatrolIndex].position, patrolSpeed);

    //    // Kiểm tra nếu đã tới điểm tuần tra hiện tại
    //    if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.2f)
    //    {
    //        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    //    }

    //    // Chuyển sang trạng thái Chasing nếu người chơi trong phạm vi phát hiện
    //    if (distanceToPlayer <= detectionRange)
    //    {
    //        currentState = State.Chasing;
    //    }
    //}

    //void ChaseBehavior(float distanceToPlayer)
    //{
    //    MoveTo(player.position, chaseSpeed);

    //    // Nếu người chơi ra khỏi phạm vi chaseRange, quay về tuần tra
    //    if (distanceToPlayer > chaseRange)
    //    {
    //        currentState = State.Returning;
    //    }
    //}

    //void ReturnBehavior()
    //{
    //    MoveTo(startingPosition, patrolSpeed);

    //    // Nếu đã trở lại vị trí ban đầu, chuyển về tuần tra
    //    if (Vector2.Distance(transform.position, startingPosition) < 0.2f)
    //    {
    //        currentState = State.Patrolling;
    //    }
    //}

    //void MoveTo(Vector3 target, float speed)
    //{
    //    Vector3 direction = (target - transform.position).normalized;
    //    rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    //}
}
