using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetBoxCast : MonoBehaviour
{
	public PlayerController playerController;
	private void OnTriggerStay2D(Collider2D collision)
	{
		playerController.canJump = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		playerController.canJump = false;
	}
}
