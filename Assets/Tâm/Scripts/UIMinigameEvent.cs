using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMinigameEvent : MonoBehaviour, ITriggerable
{
	public GameObject interactPrompt;
	private bool isTriggered = false;
	public TriggerType GetTriggerType() => TriggerType.Optional;

	public void HidePrompt()
	{
		interactPrompt.SetActive(false);
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		if (isTriggered) Destroy(gameObject);
		GameManager.instance.StartMNGRapidButton();
		isTriggered = true;
	}

	public void ShowPrompt()
	{
		interactPrompt.SetActive(true);
	}

	// Start is called before the first frame update
	void Start()
    {
        interactPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
