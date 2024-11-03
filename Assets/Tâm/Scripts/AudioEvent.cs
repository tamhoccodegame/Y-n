using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : MonoBehaviour, ITriggerable
{
    public string audioName;

	public TriggerType GetTriggerType() => TriggerType.Auto;

	public void HidePrompt()
	{
		
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		GameManager.instance.PlayAudio(audioName);
		Destroy(gameObject);
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
