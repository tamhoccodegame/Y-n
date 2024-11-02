using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Attack_Delay : MonoBehaviour
{
    public float attackCooldown = 2f;
    public float attackDuration = 0.5f;
    public float cooldownTimer = 0f;
    public bool isAttacking = false;

    public Transform player;
    public float attackRange = 5f;
    //public Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        //animation = GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckTime();
    }

    public void CheckTime()
    {
        //Kiem tra neu con thoi gian cooldown, giam bo dem
        if (cooldownTimer > 0)
        {
            cooldownTimer-=Time.deltaTime;
        }
        //Neu dang tan cong va bo dem thoi gian cooldown da ket thuc
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer<=attackRange&& !isAttacking && cooldownTimer <= 0)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        
        //Bat dau don danh 
        Animator attackAnimation = GetComponent<Animator>();
        attackAnimation.SetTrigger("isAttack");
        Debug.Log("Enemy attacked!");
        //Trong luc don danh dien ra
        yield return new WaitForSeconds(attackDuration);
        //Ket thuc don danh, bat dau cooldown
        isAttacking = false;
        cooldownTimer = attackCooldown;
        Debug.Log("Enemy finished attacking and is now on cooldown");
    }
}
