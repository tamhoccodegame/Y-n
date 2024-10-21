using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingMNG : MonoBehaviour
{
    public float speed = 5f;
    public RectTransform range;
    public RectTransform safeRange;
    public RectTransform line; // Vạch
    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = line.position;
    }

    void Update()
    {
        MoveLine();
        CheckInput();
    }

    void MoveLine()
    {
        if (movingRight)
        {
            line.position += Vector3.right * speed * Time.deltaTime;
            if (line.anchoredPosition.x >= range.rect.width)
                movingRight = false;
        }
        else
        {
            line.position -= Vector3.right * speed * Time.deltaTime;
            if (line.anchoredPosition.x <= startPos.x)
                movingRight = true;
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsInTargetArea())
            {
                Debug.Log("Đúng! Bạn đã ghi điểm.");
                // Tăng điểm
            }
            else
            {
                Debug.Log("Sai! Cố gắng lần sau.");
            }
        }
    }

    bool IsInTargetArea()
    {

        RectTransform checkLine = Instantiate(line.gameObject, line.transform.position, Quaternion.identity).GetComponent<RectTransform>();
        checkLine.SetParent(safeRange.transform);
        checkLine.anchoredPosition = Vector2.zero;

        return false;

    }
}
