using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerHeath playerHeath = collision.GetComponent<PlayerHeath>();
		EnemyNghi enemy = collision.GetComponent<EnemyNghi>();

		if (playerHeath)
		{
			playerHeath.TakeDamage();
		}

		if (enemy)
		{
			enemy.TakeDamge();
		}
	}
}
