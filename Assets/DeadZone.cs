using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerHeath player = collision.GetComponent<PlayerHeath>();
		if (player)
		{
			player.Die();
		}
	}
}
