using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public QuestDatabase questDatabase; // Tham chiếu đến cơ sở dữ liệu nhiệm vụ

	public List<GameObject> triggerStartQuests;
	public List<GameObject> triggerEndQuests;
	public int currentQuestIndex = 0;

	private void Start()
	{
		// Khởi động với các nhiệm vụ đầu tiên
		//ActivateQuest("1_1");
		foreach(GameObject start in triggerStartQuests)
		{
			start.SetActive(false);
		}
		foreach(GameObject end in triggerEndQuests)
		{
			end.SetActive(false);
		}
		triggerStartQuests[0].SetActive(true);
	}

	public void ActiveQuest(Quest quest)
	{
		if (quest != null && !quest.isActive)
		{
			quest.isActive = true; // Đánh dấu nhiệm vụ là đang hoạt động
			// Gọi phương thức onActive của nhiệm vụ
			quest.onActive?.Invoke();
			Debug.Log($"Nhiệm vụ '{quest.name}' đã được kích hoạt: {quest.description}");
		}
	}

	public void CompleteQuest(Quest quest)
	{
		if (quest != null && quest.isActive && !quest.isCompleted)
		{
			quest.CompleteQuest(); // Đánh dấu nhiệm vụ là hoàn thành
			quest.onCompleted?.Invoke(); // Gọi phương thức onCompleted của nhiệm vụ
			Debug.Log($"Nhiệm vụ '{quest.name}' đã hoàn thành!");
			currentQuestIndex++; 

			if(currentQuestIndex <= triggerStartQuests.Count - 1) 
			triggerStartQuests[currentQuestIndex].SetActive(true);
		}
	}

	public void TriggerEndQuest(Quest quest)
	{
		if(quest != null)
		{
			triggerEndQuests[currentQuestIndex].SetActive(true);
		}
	}

}
