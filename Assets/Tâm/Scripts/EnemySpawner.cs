using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public Transform spawnPoint;
	public int enemyToSpawn = 5;
	public int enemyHasSpawned = 0;
	public List<GameObject> enemySpawned = new List<GameObject>(); // Khởi tạo danh sách

	void Update()
	{
		CheckAllEnemiesDefeated(); // Gọi hàm kiểm tra trong Update
	}

	private void CheckAllEnemiesDefeated()
	{
		bool allEnemiesDefeated = true;
		Debug.Log(allEnemiesDefeated);
		foreach (var enemy in enemySpawned)
		{
			if (enemy != null) // Nếu còn ít nhất một quái còn sống
			{
				allEnemiesDefeated = false;
				break;
			}
		}

		if (allEnemiesDefeated)
		{
			GameManager.instance.CompletedQuest("1_1");
			enabled = false; // Vô hiệu hóa script này để tránh gọi lại TriggerEndQuest
		}
	}
}
