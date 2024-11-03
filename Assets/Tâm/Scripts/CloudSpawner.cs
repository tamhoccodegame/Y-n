using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnCloud), 1f, 5f);
    }

    void SpawnCloud()
    {
        Rigidbody2D rb = Instantiate(cloudPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
    }
}
