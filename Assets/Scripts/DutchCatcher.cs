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
	private KeyCode[] keys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
	public int requiredDuckCount = 10;
	private int currentDuckCount = 0;
	private GameObject currentBubble;
	private bool isCurrentBubbleHasDuckSound = false;

	public GameObject caughtADuckCutscene;
	public GameObject notCaughtADuckCutscene;

	private int previousIndex = 0;
	private bool isReceivedinput = false;

	// Start is called before the first frame update
	void Start()
    {
        StartGame();
    }

	void StartGame()
	{
		InvokeRepeating(nameof(SpawnBubble), 2f, spawnInterval);
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
			if (Input.GetKeyDown(keys[i]) && !isReceivedinput)
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

		int randomIndex;
		do
		{
			randomIndex = Random.Range(0, spawnPoints.Length);
		}
		while(randomIndex == previousIndex);

		previousIndex = randomIndex;

		Transform spawnPoint = spawnPoints[randomIndex];

		Debug.Log(keys[randomIndex]);

		isCurrentBubbleHasDuckSound = Random.value > 0.5f;

		if (isCurrentBubbleHasDuckSound)
		{
			StartCoroutine(PlayDuckSound(spawnPoint));
		}



		currentBubble = Instantiate(bubblePrefab, spawnPoint.position, Quaternion.identity);
		currentBubble.transform.SetParent(spawnPoint.transform, true);

		StartCoroutine(DestroyBubble());

	}

	IEnumerator DestroyBubble()
	{
		yield return new WaitForSeconds(1.5f);
		Destroy(currentBubble);
	}

	IEnumerator PlayDuckSound(Transform spawnPoint)
	{
		AudioSource.PlayClipAtPoint(duckSound, spawnPoint.position);
		yield return new WaitForSeconds(0.1f);
		AudioSource.PlayClipAtPoint(duckSound, spawnPoint.position);
	}

	void CheckBubbleAtPosition(int index)
	{
		isReceivedinput = true;
		CancelInvoke(nameof(SpawnBubble));
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
			}
			else
			{
				// Người chơi chụp hụt
				TriggerCutscene(false); // Cắt cảnh hụt
			}

			// Hủy bong bóng sau khi xử lý
			Destroy(currentBubble);
			currentBubble = null;
		}
		//Bấm lộn nút
		else
		{
			TriggerCutscene(false);
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
		isReceivedinput = false;

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
