using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameObject dialoguePanel;
	public TextMeshProUGUI textComponent;
	public string[] lines;
	public float textSpeed;
	private int currentDialougeLineIndex;

	public GameObject buttonContainer;
	public Button dialogueButtonYes;
	public Button dialogueButtonNo;

	private Button currentDialogueButton;

	Coroutine dialogueCoroutine;

	// Start is called before the first frame update
	void Start()
    {

	}

	// Update is called once per frame
	void Update()
    {
        
    }

    public void StartDialogue(List<DialogueLine> dialogues)
    {
		textComponent.text = string.Empty;
		dialoguePanel.SetActive(true);
		currentDialougeLineIndex = 0;

		if (dialogueCoroutine != null) StopCoroutine(dialogueCoroutine);
		dialogueCoroutine = StartCoroutine(TypeLine(dialogues));

		//dialogueButtonYes.onClick.AddListener(() => Decision(true));
		//dialogueButtonNo.onClick.AddListener(() => Decision(false));
	}

	IEnumerator TypeLine(List<DialogueLine> dialogues)
	{
		foreach(DialogueLine dialogue in dialogues)
		{
			Debug.Log(dialogue.speaker);

			for(int i = 0; i <= dialogue.sentence.Length; i++)
			{
				textComponent.text = dialogue.sentence.Substring(0, i);
				yield return new WaitForSeconds(textSpeed);
			}

			while (!Input.GetMouseButtonDown(0)) yield return null;

		}

		EndDialogue();
	}

	void EndDialogue()
	{
		dialoguePanel.SetActive(false);
		textComponent.text = string.Empty;
	}
}
