using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_BitMat : MonoBehaviour
{
    public float speed;
    private float lastSpeed;
    Coroutine checkSpeedCoroutine;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkSpeedCoroutine = StartCoroutine(CheckSpeedChange());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        Vector3 currentLocalScale = transform.localScale;
        currentLocalScale.x = Mathf.Abs(currentLocalScale.x) * Mathf.Sign(speed);
        transform.localScale = currentLocalScale;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Limit"))
        {
            speed = -speed;
        }
	}

    IEnumerator CheckSpeedChange()
    {
        while(true)
        {
            lastSpeed = speed;
            yield return new WaitForSeconds(120f);
            if(Mathf.Approximately(lastSpeed, speed))
            {
                Destroy(gameObject);
            }
        }
    }
}
