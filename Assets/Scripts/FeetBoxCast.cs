using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetBoxCast : MonoBehaviour
{
	public PlayerController playerController;
	public Animator animator;
	private void OnTriggerStay2D(Collider2D collision)
	{
		playerController.canJump = true;
        //animator.SetBool("isJumping", false);
		playerController.ChangeState(PlayerController.PlayerState.Idle);
    }

    private void OnTriggerExit2D(Collider2D collision)
	{
		playerController.canJump = false;
        //animator.SetBool("isJumping", true);
    }
}
