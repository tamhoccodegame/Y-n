using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectMachine : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
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
        bool isTopOrBottom= Random.value > 0.5f;
        int obstacleIndex = Random.Range(0, obstaclePrefab.Length);

        Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, isTopOrBottom ? 0 : 1, Camera.main.nearClipPlane));
        spawnPosition.z = 0;

        Rigidbody2D obstacle = Instantiate(obstaclePrefab[obstacleIndex], spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();

        float obstacleHeight = obstacle.GetComponent<SpriteRenderer>().bounds.size.y;

        obstacle.transform.position += new Vector3(0, isTopOrBottom ? obstacleHeight/2 : -obstacleHeight/2, 0);

        obstacle.velocity = new Vector2(-20, 0);

        Destroy(obstacle.gameObject, 3f);
    }
}
