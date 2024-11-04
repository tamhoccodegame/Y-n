using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public Transform spawnPoint;
	public int enemyToSpawn = 5;
	public int enemyHasSpawned = 0;
	public bool hasTrigger = false;
	public List<GameObject> enemySpawned = new List<GameObject>(); // Khởi tạo danh sách

	void Update()
	{
		CheckAllEnemiesDefeated(); // Gọi hàm kiểm tra trong Update
	}

	public void SpawnEnemy()
	{
		if (enemyHasSpawned >= enemyToSpawn)
		{
			CancelInvoke();
			return;
		}

		GameObject go = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
		enemyHasSpawned++;
		enemySpawned.Add(go);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !hasTrigger)
		{
			hasTrigger = true;
			InvokeRepeating(nameof(SpawnEnemy), 0.1f, 5f);
		}
	}

	private void CheckAllEnemiesDefeated()
	{
		if (enemyHasSpawned < enemyToSpawn) return; // Không kiểm tra nếu chưa spawn đủ số lượng quái

		bool allEnemiesDefeated = true;
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
			GameManager.instance.TriggerEndQuest("1_1");
			enabled = false; // Vô hiệu hóa script này để tránh gọi lại TriggerEndQuest
		}
	}
}
