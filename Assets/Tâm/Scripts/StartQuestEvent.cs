using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartQuestEvent : MonoBehaviour, ITriggerable
{
	public string questName;
	public void TriggerAction()
	{
		GameManager.instance.ActiveQuest(questName);
	}
}
