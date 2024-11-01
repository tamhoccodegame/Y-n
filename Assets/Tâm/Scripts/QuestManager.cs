using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public QuestDatabase questDatabase; // Tham chiếu đến cơ sở dữ liệu nhiệm vụ
	private List<Quest> activeQuests = new List<Quest>(); // Danh sách nhiệm vụ đang hoạt động

	private void Start()
	{
		// Khởi động với các nhiệm vụ đầu tiên
		//ActivateQuest("1_1");
	}

	public void ActiveQuest(string questName)
	{
		Quest quest = questDatabase.GetQuest(questName);

		if (quest != null && !quest.isActive && (string.IsNullOrEmpty(quest.prerequisiteQuest) || IsQuestCompleted(quest.prerequisiteQuest)))
		{
			quest.isActive = true; // Đánh dấu nhiệm vụ là đang hoạt động
			activeQuests.Add(quest); // Thêm nhiệm vụ vào danh sách nhiệm vụ đang hoạt động

			// Gọi phương thức onActive của nhiệm vụ
			quest.onActive?.Invoke(QuestDatabase.tempGameObject);
			Debug.Log($"Nhiệm vụ '{quest.name}' đã được kích hoạt: {quest.description}");
		}
		else if (quest != null && quest.isActive)
		{
			Debug.Log($"Nhiệm vụ '{quest.name}' đã hoạt động trước đó.");
		}
		else if (quest != null && !IsQuestCompleted(quest.prerequisiteQuest))
		{
			Debug.Log($"Nhiệm vụ '{quest.name}' yêu cầu nhiệm vụ '{quest.prerequisiteQuest}' hoàn thành trước.");
		}
	}

	public void CompleteQuest(string questName)
	{
		Quest quest = questDatabase.GetQuest(questName);

		if (quest != null && quest.isActive && !quest.isCompleted)
		{
			quest.CompleteQuest(); // Đánh dấu nhiệm vụ là hoàn thành
			quest.onCompleted?.Invoke(); // Gọi phương thức onCompleted của nhiệm vụ
			Debug.Log($"Nhiệm vụ '{quest.name}' đã hoàn thành!");

			// Kiểm tra và kích hoạt nhiệm vụ tiếp theo nếu có
			ActivateNextQuest(questName);
		}
	}

	private void ActivateNextQuest(string completedQuestName)
	{
		foreach (Quest quest in questDatabase.GetAllQuests())
		{
			if (quest.prerequisiteQuest == completedQuestName)
			{
				ActiveQuest(quest.name); // Kích hoạt nhiệm vụ tiếp theo
			}
		}
	}

	private bool IsQuestCompleted(string questName)
	{
		Quest quest = questDatabase.GetQuest(questName);
		return quest != null && quest.isCompleted; // Kiểm tra trạng thái nhiệm vụ
	}
}
