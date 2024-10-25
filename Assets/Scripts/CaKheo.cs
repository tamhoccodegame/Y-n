using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaKheo : MonoBehaviour
{
    public TextMeshProUGUI qteText; // Text UI để hiển thị nút cần bấm
    public Transform player; // Nhân vật sẽ di chuyển
    public float moveDistance = 1f; // Khoảng cách mỗi lần tiến lên
    public float qteTimeLimit = 2.5f; // Giới hạn thời gian để bấm phím
    private List<string> currentKeys = new List<string>();
    private int currentKeyIndex = 0; // Phím hiện tại cần bấm
    private int failCount = 0; // Số lần bấm sai
    private int maxFailCount = 3; // Giới hạn số lần sai

    private List<string> keys = new List<string> { "E", "D", "W", "S", "R" }; // Danh sách các phím QTE

    private void Start()
    {
        GenerateNewQTE(); // Tạo phím QTE đầu tiên
        ShowNextKey();
        StartCoroutine(QTECountdown()); // Bắt đầu đếm ngược thời gian
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) return;
            if (Input.GetKeyDown(currentKeys[currentKeyIndex].ToLower())) // Kiểm tra phím đúng
            {
                currentKeyIndex++;
                if(currentKeyIndex >= currentKeys.Count)
                {
                    MovePlayer();
                    GenerateNewQTE(); // Tạo phím QTE mới
                    currentKeyIndex = 0;
                    StopAllCoroutines();
                    ShowNextKey();
                    StartCoroutine(QTECountdown()); // Reset thời gian
                }
                else
                {
                    ShowNextKey();
                    StopAllCoroutines();
                    StartCoroutine(QTECountdown());
                }
               
            }
            else //Bấm phím sai
            {
                failCount++;
                if (failCount >= maxFailCount)
                {
                    GameOver();
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

    private IEnumerator QTECountdown()
    {
        yield return new WaitForSeconds(qteTimeLimit);
        failCount++;
        if (failCount >= maxFailCount)
        {
            GameOver();
        }
        else
        {
            ShowNextKey(); // Hiện lại phím hiện tại nếu hết thời gian mà bấm không kịp
            StartCoroutine(QTECountdown()); // Reset thời gian
        }
    }

    private void GameOver()
    {
        qteText.text = "Game Over!";
        StopAllCoroutines();
    }
}
