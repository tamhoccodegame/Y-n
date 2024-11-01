using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string name;
    public string description;
    public bool isCompleted;
    public bool isActive;

	public List<GameObject> requiredComponentsKeys;

	public Action<List<GameObject>> onActive;
    public Action<List<GameObject>> onCompleted;

    public void CompleteQuest()
    {
        isCompleted = true;
    }
}

public class QuestDatabase : MonoBehaviour
{
    private List<Quest> quests = new List<Quest>()
    {
        new Quest
        {
            name = "Quai_Scene_1",
            description = "Đánh bại kẻ địch",
            isCompleted = false,
            isActive = false,

            requiredComponentsKeys = new List<GameObject>(),
            onActive = (List<GameObject> g) =>
            {
                GameObject camGo = g.Find(g => g.GetComponent<CinemachineVirtualCamera>() != null);
                CinemachineVirtualCamera cam = camGo.GetComponent<CinemachineVirtualCamera>();
                List<GameObject> walls = g.FindAll(g => g.name.Contains("Wall"));

                
            },
            onCompleted = (List<GameObject> g) =>
            {

            },
        }
    };

    

    public Quest GetQuest(string questName)
    {
        Quest quest = quests.Find(q => q.name == questName);
        if (quest != null) return quest;
        return null;
    }

    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
    }
}
