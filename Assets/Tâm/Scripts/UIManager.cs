﻿using System.Collections;
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
	public GameObject dialoguePanel;
	public TextMeshProUGUI speaker;
	public TextMeshProUGUI sentence;
	public float textSpeed;
	private int currentDialougeLineIndex;

	public GameObject buttonContainer;
	public Button dialogueButtonYes;
	public Button dialogueButtonNo;
	private Button currentButton;
	private TextMeshProUGUI buttonYesText;
	private TextMeshProUGUI buttonNoText;

	private bool isButtonClicked = false;

	private Button currentDialogueButton;

	Coroutine dialogueCoroutine;

	// Start is called before the first frame update
	void Start()
    {
		buttonYesText = dialogueButtonYes.GetComponentInChildren<TextMeshProUGUI>();
		buttonNoText = dialogueButtonNo.GetComponentInChildren<TextMeshProUGUI>();
		buttonContainer.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
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
					EndDialogue();
				});
				dialogueButtonNo.onClick.AddListener(() =>
				{
					dialogue.choice.onNo?.Invoke();
					EndDialogue();
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

		EndDialogue();
	}

	void EndDialogue()
	{
		dialoguePanel.SetActive(false);
		sentence.text = string.Empty;
	}
}
