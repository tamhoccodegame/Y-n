using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucketPickUp : MonoBehaviour
{
    private Transform playerBucketPostition;
    private PlayerStateManager stateManager;
    public GameObject bucketPrefab;
    private bool isPlayerInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SpawnBucket();
        }
    }

    private void SpawnBucket()
    {
        Instantiate(bucketPrefab, playerBucketPostition.position, Quaternion.identity, playerBucketPostition);
        stateManager.SwitchState(stateManager.carryingIdleState);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
			isPlayerInRange = true;
            playerBucketPostition = collision.gameObject.transform.Find("WaterBucketPosition");
            stateManager = collision.GetComponent<PlayerStateManager>();
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        isPlayerInRange = false;
        playerBucketPostition = null;
	}
}
