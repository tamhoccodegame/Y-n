using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class CaKheo : MonoBehaviour
{
	public TextMeshPro qteText; // Text UI để hiển thị nút cần bấm
	public Transform player; // Nhân vật sẽ di chuyển
	public float moveDistance = 1f; // Khoảng cách mỗi lần tiến lên
	public float qteTimeLimit = 2.5f; // Giới hạn thời gian để bấm phím
	private List<string> currentKeys = new List<string>();
	private int currentKeyIndex = 0; // Phím hiện tại cần bấm
	private int failCount = 0; // Số lần bấm sai
	private int maxFailCount = 3; // Giới hạn số lần sai
	public bool isAI = false; // Đặt thành true để biến thành AI
	public PlayableDirector failCutscene;
	public bool isEnd = false;

	public GameObject goal;

	private List<string> keys = new List<string> { "E", "D", "W", "S", "R" }; // Danh sách các phím QTE

	private void Start()
	{
		GameManager.instance.HideUI();

		if (!isAI)
		{
			GenerateNewQTE(); // Tạo phím QTE đầu tiên
			ShowNextKey();
			StartCoroutine(QTECountdown()); // Bắt đầu đếm ngược thời gian
			StopWalkingAnim();
			failCutscene.stopped += FailCutscene_stopped;
		}
		
		if (isAI)
		{
			StartCoroutine(AIInputCoroutine()); // Bắt đầu coroutine AI nếu là AI
		}
	}


	private void Update()
	{
		if (isEnd) return;

		if (isAI) return; // Nếu là AI, không xử lý Input của người chơi

		if (Mathf.Abs(transform.position.x - goal.transform.position.x) <= 1f)
		{ Debug.Log((Mathf.Abs(transform.position.x - goal.transform.position.x)));
			GameOver(true);
		}

		if (Input.anyKeyDown)
		{
			if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) return;
			if (Input.GetKeyDown(currentKeys[currentKeyIndex].ToLower())) // Kiểm tra phím đúng
			{
				currentKeyIndex++;
				if (currentKeyIndex >= currentKeys.Count)
				{
					MovePlayer();
					GenerateNewQTE(); // Tạo phím QTE mới
					currentKeyIndex = 0;
					StopAllCoroutines();
					ShowNextKey();
					StartCoroutine(QTECountdown()); // Reset thời gian
					StartCoroutine(PlayWalkingAnim());
				}
				else
				{
					StopWalkingAnim();
					ShowNextKey();
					StopAllCoroutines();
					StartCoroutine(QTECountdown());
				}
			}
			else // Bấm phím sai
			{
				failCount++;
				if (failCount >= maxFailCount)
				{
					GameOver(false);
				}
			}
		}
	}

	private void GenerateNewQTE()
	{
		currentKeys.Clear();
		for (int i = 0; i < 3; i++)
		{
			int randomIndex = Random.Range(0, keys.Count);
			currentKeys.Add(keys[randomIndex]);
		}
	}

	private void ShowNextKey()
	{
		qteText.text = "Press " + currentKeys[currentKeyIndex];
	}

	private void MovePlayer()
	{
		player.position += new Vector3(moveDistance, 0, 0);
	}

	IEnumerator PlayWalkingAnim()
	{
		Animator animator = GetComponent<Animator>();
		animator.speed = 1;
		yield return new WaitForSeconds(0.5f);
		animator.speed = 0;
	}

	void StopWalkingAnim()
	{
		Animator animator = GetComponent<Animator>();
		animator.speed = 0;
	}

	private IEnumerator QTECountdown()
	{
		yield return new WaitForSeconds(qteTimeLimit);
		failCount++;
		if (failCount >= maxFailCount)
		{
			GameOver(false);
		}
		else
		{
			ShowNextKey(); // Hiện lại phím hiện tại nếu hết thời gian mà bấm không kịp
			StartCoroutine(QTECountdown()); // Reset thời gian
		}
	}

	private void PlayWinAnim()
	{
		Animator animator = GetComponent<Animator>();
		animator.speed = 1.5f;
	}

	private void FailCutscene_stopped(PlayableDirector obj)
	{
		GameManager.instance.LoadPreviousScene();
	}

	private void GameOver(bool isWin)
	{
		isEnd = true;
		StopAllCoroutines();
		if (!isWin)
		{
			qteText.text = "Game Over!";
			isEnd = true;
			failCutscene.gameObject.SetActive(true);
		}
		else if(isWin)
		{
			PlayWinAnim();
			GameManager.instance.LoadPreviousScene();
		}
		
		
	}

	private IEnumerator AIInputCoroutine()
	{
		while (true)
		{
			// Giả lập việc AI "bấm phím" sau một khoảng thời gian ngẫu nhiên
			yield return new WaitForSeconds(Random.Range(1f, 3f)); // Thời gian ngẫu nhiên để AI nhập
			SimulateAIKeyPress();
		}
	}

	private void SimulateAIKeyPress()
	{
		StartCoroutine(PlayWalkingAnim());
		MovePlayer();
	}

}
