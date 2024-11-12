using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#nullable enable

public class DialogueEvent : MonoBehaviour, ITriggerable
{
	public bool isFirstTrigger = true;
    public string dialogueName;
	public bool isOTPDialouge;

	public GameObject? interactPrompt;
	public UnityEvent onDialogueCompleted;

	public TriggerType GetTriggerType() => (isFirstTrigger || isOTPDialouge) ? TriggerType.Auto : TriggerType.Optional;

	public void HidePrompt()
	{
		interactPrompt?.SetActive(false);
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		if (isOTPDialouge && !isFirstTrigger) Destroy(gameObject);
		GameManager.instance.StartDialogue(dialogueName);
		isFirstTrigger = false;
	}

	public void ShowPrompt()
	{
		interactPrompt?.SetActive(true);
	}

	// Start is called before the first frame update
	void Start()
    {
		interactPrompt?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
