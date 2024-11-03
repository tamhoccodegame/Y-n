using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartQuestEvent : MonoBehaviour, ITriggerable
{
	public string questName;

	public TriggerType GetTriggerType() => TriggerType.Auto;

	public void HidePrompt()
	{
		
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		GameManager.instance.ActiveQuest(questName);
		Destroy(gameObject);
	}

	public void ShowPrompt()
	{
		
	}
}
