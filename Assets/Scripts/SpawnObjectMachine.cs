using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnObjectMachine : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public Transform topPosition;
    public Transform bottomPosition;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 1f, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObject()
    {
        bool isTopOrBottom = Random.value > 0.5f;
        int obstacleIndex = Random.Range(0, obstaclePrefab.Length);
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(topPosition.position.y, bottomPosition.position.y), 0);

        Rigidbody2D obstacle = Instantiate(obstaclePrefab[obstacleIndex], spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();

        obstacle.velocity = new Vector2(-20, 0);

        Destroy(obstacle.gameObject, 3f);
    }
}
