using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DutchCatcher : MonoBehaviour
{
	public GameObject bubblePrefab;
	public Transform[] spawnPoints; // random spawn position
	public AudioClip duckSound;
	public float spawnInterval = 1f; // cooldown
	private int duckCount = 0;
	private float timeLeft = 60f; // time
	private KeyCode[] keys = { KeyCode.Q, KeyCode.E, KeyCode.A, KeyCode.D, KeyCode.F, KeyCode.R };
	public int requiredDuckCount = 5;
	private int currentDuckCount = 0;
	private int legalErrorsCount = 2;
	private GameObject currentBubble;
	private bool isCurrentBubbleHasDuckSound = false;

	public GameObject caughtADuckCutscene;
	public GameObject notCaughtADuckCutscene;

	public Text time;
	public Text duckCaught;
	public Text legalError;

	private int previousIndex = 0;
	private int currentKeyIndex = 0;
	private bool isReceivedinput = false;
	private bool isEnd = false;

	// Start is called before the first frame update
	void Start()
	{
		StartGame();
		GameManager.instance.HideUI();
	}

	void StartGame()
	{
		InvokeRepeating(nameof(SpawnBubble), 2f, spawnInterval);
	}

	private void FixedUpdate()
	{
		time.text = "Thời gian: " + timeLeft.ToString("00");
		duckCaught.text = "Vịt đã bắt được: " + currentDuckCount.ToString();
		legalError.text = "Số lần được bắt hụt " + legalErrorsCount.ToString();
	}
	// Update is called once per frame
	void Update()
	{
		if (isEnd) return;
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0 && !isEnd)
		{
			StopAllCoroutines();
			EndGame(false);
		}

		if (legalErrorsCount == 0) EndGame(false);

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
		CancelInvoke(nameof(SpawnBubble));
		// Gán giá trị ngẫu nhiên cho currentKeyIndex để chọn phím bấm
		currentKeyIndex = Random.Range(0, keys.Length); // Chọn ngẫu nhiên nút bấm cho bong bóng
		int randomSpawnIndex = Random.Range(0, spawnPoints.Length); // Chọn ngẫu nhiên vị trí spawn

		Transform spawnPoint = spawnPoints[randomSpawnIndex];

		Debug.Log("Phím yêu cầu: " + keys[currentKeyIndex]);

		isCurrentBubbleHasDuckSound = Random.value > 0.5f;

		if (isCurrentBubbleHasDuckSound)
		{
			StartCoroutine(PlayDuckSound(spawnPoint));
		}


		// Tạo bong bóng tại vị trí spawnPoint với nút bấm ngẫu nhiên
		currentBubble = Instantiate(bubblePrefab, spawnPoint.position, Quaternion.identity);
		currentBubble.GetComponentInChildren<TextMeshPro>().text = keys[currentKeyIndex].ToString(); // Hiển thị phím bấm
		currentBubble.transform.SetParent(spawnPoint.transform, true);

		StartCoroutine(DestroyBubble());
	}


	IEnumerator DestroyBubble()
	{
		yield return new WaitForSeconds(spawnInterval);
		Destroy(currentBubble);
		isReceivedinput = false;
		StartGame();
	}

	IEnumerator PlayDuckSound(Transform spawnPoint)
	{
		AudioSource.PlayClipAtPoint(duckSound, spawnPoint.position);
		yield return new WaitForSeconds(0.1f);
		AudioSource.PlayClipAtPoint(duckSound, spawnPoint.position);
	}

	void CheckBubbleAtPosition(int inputIndex)
	{
		CancelInvoke(nameof(SpawnBubble));
		isReceivedinput = true;

		// Kiểm tra xem người chơi bấm có trùng với phím ngẫu nhiên không
		if (inputIndex == currentKeyIndex)
		{
			if (isCurrentBubbleHasDuckSound)
			{
				currentDuckCount++;
				TriggerCutscene(true);

				if (currentDuckCount >= requiredDuckCount)
				{
					EndGame(true); // Thắng
				}
			}
			else
			{
				TriggerCutscene(false);
				legalErrorsCount--;
			}

			Destroy(currentBubble);
			currentBubble = null;
		}
		else
		{
			// Người chơi bấm sai phím
			legalErrorsCount--;
			TriggerCutscene(false);
			Destroy(currentBubble);
			currentBubble = null;
		}

		// Bắt đầu lại sau khi xử lý

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

		yield return new WaitForSeconds(1.0f);

		caughtADuckCutscene.SetActive(false);
		notCaughtADuckCutscene.SetActive(false);
		isReceivedinput = false;

		StartGame();
	}

	void EndGame(bool isWin)
	{
		isEnd = true;
		Debug.Log("Endgame");
		if (!isWin)
		{
			Debug.Log("Mày ngu! Mày ngu! Mày ngu");
			GameManager.instance.LoadPreviousScene();
		}
		else
		{
			Debug.Log("Idol!!");
			GameManager.instance.LoadPreviousScene();
		}
	}
}

