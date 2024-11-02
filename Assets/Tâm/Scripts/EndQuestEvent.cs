using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndQuestEvent : MonoBehaviour, ITriggerable
{
    public string questName;

	public void TriggerAction()
	{
		GameManager.instance.CompletedQuest(questName);
	}
}
