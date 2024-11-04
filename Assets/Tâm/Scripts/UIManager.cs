using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[Header("==========Dialogue==========")]
	public GameObject dialoguePanel;
	public Text speaker;
	public Text sentence;
	public float textSpeed;
	private int currentDialougeLineIndex;
	public GameObject buttonContainer;
	public Button dialogueButtonYes;
	public Button dialogueButtonNo;
	private Button currentButton;
	private Text buttonYesText;
	private Text buttonNoText;
	private bool isButtonClicked = false;
	private Button currentDialogueButton;
	Coroutine dialogueCoroutine;

	public Text questText;
	public Text notifyText;

	[Header("==========Tabs==========")]
	public UIDiaryTab uiDiary;
	public UICostumeTab uiCostume;
	public GameObject menu;

	public GameObject gameOverMenu;

	public GameObject[] hearts;

	public GameObject rapidButtonPanel;

	public bool isDiaryUnlocked = false;
	public bool isCostumeUnlocked = false;


	// Start is called before the first frame update
	void Start()
    {
		buttonYesText = dialogueButtonYes.GetComponentInChildren<Text>();
		buttonNoText = dialogueButtonNo.GetComponentInChildren<Text>();
		buttonContainer.SetActive(false);
		menu.SetActive(false);

		//gameOverMenu.SetActive(false);
	}

	public void StartScene()
	{
		dialoguePanel.SetActive(false);
		buttonContainer.SetActive(false);
		menu.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
		GameManager.instance.SetIsControllable(false);
		speaker.text = string.Empty;
		sentence.text = string.Empty;
		dialoguePanel.SetActive(true);
		currentDialougeLineIndex = 0;
		isButtonClicked = false;

		if (dialogueCoroutine != null) StopCoroutine(dialogueCoroutine);
		dialogueCoroutine = StartCoroutine(TypeLine(dialogue));

		//dialogueButtonYes.onClick.AddListener(() => Decision(true));
		//dialogueButtonNo.onClick.AddListener(() => Decision(false));
	}

	IEnumerator TypeLine(Dialogue dialogue)
	{
		foreach(DialogueLine line in dialogue.lines)
		{
			speaker.text = line.speaker;

			for(int i = 0; i <= line.sentence.Length; i++)
			{
				sentence.text = line.sentence.Substring(0, i);
				yield return new WaitForSeconds(textSpeed);
			}

			while (!Input.GetMouseButtonDown(0)) yield return null;

			if(line == dialogue.lines.Last() && dialogue.hasChoice)
			{
				EventSystem.current.SetSelectedGameObject(dialogueButtonYes.gameObject);
				currentButton = dialogueButtonYes;
				buttonContainer.SetActive(true);

				buttonYesText.text = dialogue.choice.yesText;
				buttonNoText.text = dialogue.choice.noText;

				dialogueButtonYes.onClick.AddListener(() =>
				{
					dialogue.choice.onYes?.Invoke();
					EndDialogue(dialogue);
				});
				dialogueButtonNo.onClick.AddListener(() =>
				{
					dialogue.choice.onNo?.Invoke();
					EndDialogue(dialogue);
				});

				while (!isButtonClicked)
				{
					if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
					{
						currentButton = currentButton == dialogueButtonYes ? dialogueButtonNo : dialogueButtonYes;
						EventSystem.current.SetSelectedGameObject(currentButton.gameObject);
					}

					yield return null;
				}

			}
		}

		EndDialogue(dialogue);
	}

	void EndDialogue(Dialogue dialogue)
	{
		dialoguePanel.SetActive(false);
		buttonContainer.SetActive(false);
		speaker.text = string.Empty;
		sentence.text = string.Empty;
		GameManager.instance.SetIsControllable(true);
		dialogue.onCompleted?.Invoke();
	}

	public void UpdateUIMenu()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		menu.SetActive(!menu.activeSelf);
	}

	public void UpdateVisual()
	{
		uiDiary.UpdateVisual();
		uiCostume.UpdateVisual();
		StartCoroutine(UpdateNotify(notifyText));
	}

	IEnumerator UpdateNotify(Text text)
	{
		if (isCostumeUnlocked)
		{
			text.gameObject.SetActive(true);
			uiCostume.UpdateNotify(text);
			yield return new WaitForSeconds(3f);
			text.gameObject.SetActive(false);
			yield return new WaitForSeconds(1f);
			isCostumeUnlocked = false;
		}

		if (isDiaryUnlocked)
		{
			text.gameObject.SetActive(true);
			uiDiary.UpdateNotify(text);
			yield return new WaitForSeconds(3f);
			text.gameObject.SetActive(false);
			yield return new WaitForSeconds(1f);
			isDiaryUnlocked = false;
		}
	}

	public void UpdateHealthUI(int currentHealth)
	{
		for(int i = 0; i < hearts.Length; i++)
		{
			hearts[i].SetActive(i < currentHealth);
		}
	}


	public void StartQuest(Quest quest)
	{
		//Cập nhật hiện UI mô tả Quest
		questText.text = quest.description;
		questText.gameObject.SetActive(true);
	}

	public void EndQuest()
	{
		questText.text = string.Empty;
		questText.gameObject.SetActive(false);
	}

	public void StartRapidButtonMNG()
	{
		rapidButtonPanel.SetActive(true);
	}

	public void EndRapidButtonBNG()
	{
		rapidButtonPanel.SetActive(false);
	}

	public void GameOver()
	{
		transform.Find("Menu")?.gameObject.SetActive(false);
		//gameOverMenu.SetActive(true);
	}
}
