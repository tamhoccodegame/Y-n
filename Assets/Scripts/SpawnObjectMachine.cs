using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SpawnObjectMachine : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public Transform topPosition;
    public Transform bottomPosition;

    public PlayableDirector completeCutscene;

    public float currentTime;
    public float completeTime;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 3f, 3.5f);
		completeCutscene.stopped += CompleteCutscene_stopped;
    }

	private void CompleteCutscene_stopped(PlayableDirector obj)
	{
		GameManager.instance.LoadPreviousScene();
	}

	// Update is called once per frame
	void Update()
    {
        if (Time.time < 4f) return;

        slider.value = currentTime / completeTime;

        currentTime += Time.deltaTime;

        if(currentTime >= completeTime)
        {
            completeCutscene.Play();
        }
    }

    void SpawnObject()
    {
        bool isTopOrBottom = Random.value > 0.5f;
        int obstacleIndex = Random.Range(0, obstaclePrefab.Length);
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(topPosition.position.y, bottomPosition.position.y), 0);

        Rigidbody2D obstacle = Instantiate(obstaclePrefab[obstacleIndex], spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();
        obstacle.gameObject.transform.rotation = obstaclePrefab[obstacleIndex].transform.rotation;
        obstacle.linearVelocity = new Vector2(-30, 0);

        Destroy(obstacle.gameObject, 3f);
    }
}
