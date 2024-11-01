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
	public string prerequisiteQuest;

	public List<GameObject> requiredComponentsKeys;

	public Action<List<GameObject>> onActive;
    public Action onCompleted;

    public void CompleteQuest()
    {
        isCompleted = true;
    }
}

public class QuestDatabase : MonoBehaviour
{
    public static List<GameObject> tempGameObject = new List<GameObject>();
    private List<Quest> quests = new List<Quest>()
    {
        new Quest
        {
            name = "1_1",
            prerequisiteQuest = "",
            description = "Đánh bại kẻ địch",
            isCompleted = false,
            isActive = false,

            requiredComponentsKeys = new List<GameObject>(),
            onActive = (List<GameObject> g) =>
            {
                if(g.Count > 0) QuestDatabase.tempGameObject = g;
                GameObject wall = g.Find(g => g.name == "Wall");
                wall.SetActive(true);
            },
            onCompleted = () =>
            {
                GameObject wall = QuestDatabase.tempGameObject.Find(w => w.name == "Wall");
				wall.SetActive(false);
                QuestDatabase.tempGameObject.Clear();
			},
        },


		new Quest
		{
			name = "1_2",
            prerequisiteQuest = "1_1",
			description = "Chữa cháy cho ngôi làng",
			isCompleted = false,
			isActive = false,

			requiredComponentsKeys = new List<GameObject>(),
			onActive = (List<GameObject> g) =>
			{
				//GameObject wall = g.Find(g => g.name == "Wall");
				//wall.SetActive(true);
			},
			onCompleted = () =>
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
	public List<Quest> GetAllQuests()
	{
		return quests;
	}
}
