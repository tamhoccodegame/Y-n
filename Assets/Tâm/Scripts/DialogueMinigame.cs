using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    Transform buttonContainer;
	public Button buttonYes;
    public Button buttonNo;

    private Button currentButton;

    public string sceneToLoad;
	// Start is called before the first frame update
	private void OnEnable()
	{
		textComponent.text = string.Empty;
		StartCoroutine(TypeLine());
		index = 0;

		buttonYes.onClick.AddListener(() => Decision(true));
		buttonNo.onClick.AddListener(() => Decision(false));

		buttonContainer = transform.Find("ButtonContainer");
		buttonContainer.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		index = 0;
		textComponent.text = string.Empty;
        lines = null;
	}

	// Update is called once per frame
	void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentButton = currentButton == buttonYes ? buttonNo : buttonYes;
			EventSystem.current.SetSelectedGameObject(currentButton.gameObject);
		}

        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentButton.onClick.Invoke();
        }
	}

    void Decision(bool isYes)
    {
        if(isYes)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine()
	{
		foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            buttonContainer.gameObject.SetActive(true);
            currentButton = buttonYes;
			EventSystem.current.SetSelectedGameObject(currentButton.gameObject);
		}
	}
}
