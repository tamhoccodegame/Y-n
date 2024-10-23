using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                StartCoroutine(ProcessResult(true));
            }
            else
            {
				StartCoroutine(ProcessResult(false));
			}
		}
    }

    IEnumerator ProcessResult(bool isCorrect)
    {
        if (isCorrect)
        {
			line.GetComponent<Image>().color = Color.green;
		}
        else
        {
			line.GetComponent<Image>().color = Color.red;
		}
        yield return new WaitForSeconds(.1f);
        line.GetComponent <Image>().color = Color.black;
    }

    bool IsInTargetArea()
    {
        RectTransform checkLine = Instantiate(line.gameObject, line.transform.position, Quaternion.identity, safeRange).GetComponent<RectTransform>();
        bool result = checkLine.anchoredPosition.x >= 0 && checkLine.anchoredPosition.x <= safeRange.rect.width;

        Destroy(checkLine.gameObject);

		return result;

    }
}
