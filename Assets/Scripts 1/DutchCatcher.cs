using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DutchCatcher : MonoBehaviour
{
	public GameObject bubblePrefab;
	public Transform[] spawnPoints; // random spawn position
	public AudioClip duckSound;
	public float spawnInterval = 2.0f; // cooldown
	private int duckCount = 0;
	private float timeLeft = 60f; // time
	public KeyCode[] keys = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };
	public int requiredDuckCount = 10;
	private int currentDuckCount = 0;
	private GameObject currentBubble;
	private bool isCurrentBubbleHasDuckSound = false;

	public GameObject caughtADuckCutscene;
	public GameObject notCaughtADuckCutscene;

	// Start is called before the first frame update
	void Start()
    {
        StartGame();
    }

	void StartGame()
	{
		InvokeRepeating(nameof(SpawnBubble), 1f, spawnInterval);
	}

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
		if (timeLeft < 0)
		{
			EndGame(false);
		}

		for (int i = 0; i < keys.Length; i++)
		{
			if (Input.GetKeyDown(keys[i]))
			{
				CheckBubbleAtPosition(i);
				break;
			}
		}
	}

	void SpawnBubble()
	{
		if (currentBubble != null)
		{
			Destroy(currentBubble);
		}

		Debug.Log("Spawn Bubble");
		int randomIndex = Random.Range(0, spawnPoints.Length);
		Transform spawnPoint = spawnPoints[randomIndex];

		currentBubble = Instantiate(bubblePrefab, spawnPoint.position, Quaternion.identity);
		currentBubble.transform.SetParent(spawnPoint.transform, true);

		isCurrentBubbleHasDuckSound = true;

		if(isCurrentBubbleHasDuckSound)
		{
			//AudioSource.PlayClipAtPoint(duckSound, spawnPoint.position);

		}
		else
		{

		}
	}

	void CheckBubbleAtPosition(int index)
	{
		// Kiểm tra nếu có bong bóng tại vị trí tương ứng và đó là bong bóng hiện tại
		if (currentBubble != null && spawnPoints[index].transform == currentBubble.transform.parent)
		{
			if (isCurrentBubbleHasDuckSound)
			{
				// Người chơi bắt được con vịt
				currentDuckCount++;
				TriggerCutscene(true); // Cắt cảnh bắt vịt

				// Kiểm tra điều kiện thắng
				if (currentDuckCount >= requiredDuckCount)
				{
					EndGame(true); // Kết thúc trò chơi với chiến thắng
				}

				CancelInvoke(nameof(SpawnBubble));
			}
			else
			{
				// Người chơi chụp hụt
				TriggerCutscene(false); // Cắt cảnh hụt
				CancelInvoke(nameof(SpawnBubble));
			}

			// Hủy bong bóng sau khi xử lý
			Destroy(currentBubble);
			currentBubble = null;
		}
		//Bấm lộn nút
		else
		{
			TriggerCutscene(false);
			CancelInvoke(nameof(SpawnBubble));
			Destroy(currentBubble);
			currentBubble = null;
		}
	}

	void TriggerCutscene(bool isCaught)
	{
		StartCoroutine(PlayCutscene(isCaught));
	}

	IEnumerator PlayCutscene(bool isCaught)
	{
		if (isCaught)
		{
			caughtADuckCutscene.SetActive(true);
		}
		else
		{
			notCaughtADuckCutscene.SetActive(true);
		}

		yield return new WaitForSeconds(0.8f);

		caughtADuckCutscene.SetActive(false);
		notCaughtADuckCutscene.SetActive(false);

		StartGame();
	}

	void EndGame(bool isWin)
	{
		if(!isWin)
		{

		}
		else
		{

		}
	}
}
