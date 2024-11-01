using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestDatabase questDatabase;

	public void StartQuest(string questName)
	{
		Quest quest = questDatabase.GetQuest(questName);
		quest.isActive = true;
		quest.isCompleted = false;
	}

	public void ActivateQuest(string questName, List<GameObject> components)
	{
		Quest quest = questDatabase.GetQuest(questName);
		if (quest != null && !quest.isActive)
		{
			quest.isActive = true;
			quest.onActive?.Invoke(components);
			Debug.Log($"Quest '{questName}' activated!");
		}
	}

	private object[] ResolveRequiredComponents(string[] requiredComponentsKeys)
	{
		List<object> components = new List<object>();
		foreach (string key in requiredComponentsKeys)
		{
			components.Add(GameManager.instance.GetComponentByKey(key));
		}
		return components.ToArray();
	}

	public void CompleteQuest(string questName)
	{
		Quest quest = questDatabase.GetQuest(questName);
		if (quest != null && quest.isActive && !quest.isCompleted)
		{
			quest.isCompleted = true;
			List<GameObject> components = quest.requiredComponentsKeys;
			quest.onCompleted?.Invoke(components);
			Debug.Log($"Quest '{questName}' completed!");
		}
	}
}
