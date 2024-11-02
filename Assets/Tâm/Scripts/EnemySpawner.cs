using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int enemyToSpawn = 5;
    public int enemyHasSpawned = 0;
    public bool hasTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        if(enemyHasSpawned >= enemyToSpawn)
        {
            CancelInvoke();
            return;
        }

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemyHasSpawned++;

    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && !hasTrigger)
        {
            hasTrigger = true;
            InvokeRepeating(nameof(SpawnEnemy), 0.1f, 5f);
        }
	}
}
