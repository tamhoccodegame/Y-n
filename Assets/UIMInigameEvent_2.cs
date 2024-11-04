using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMInigameEvent_2 : MonoBehaviour, ITriggerable
{
    public GameObject interactPrompt;

	public TriggerType GetTriggerType() => TriggerType.Optional;

	public void HidePrompt()
	{
		interactPrompt.SetActive(false);
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		
	}

	public void ShowPrompt()
	{
		interactPrompt.SetActive(true);
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
