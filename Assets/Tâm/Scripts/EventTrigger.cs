using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour, ITriggerable
{
    public string audioName;
    public string dialogueName;
	public bool isFirstInteract = true;

	public TriggerType GetTriggerType() => isFirstInteract ? TriggerType.Auto : TriggerType.Optional;

	public void HidePrompt()
	{
		
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		
	}

	public void ShowPrompt()
	{
		
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
