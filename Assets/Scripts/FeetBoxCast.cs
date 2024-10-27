using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetBoxCast : MonoBehaviour
{
	public PlayerStateManager player;
	public Animator animator;
	private void OnTriggerStay2D(Collider2D collision)
	{
		player.isGrounded = true;
        //animator.SetBool("isJumping", false);
    }

    private void OnTriggerExit2D(Collider2D collision)
	{
		player.isGrounded = false;
        //animator.SetBool("isJumping", true);
    }
}
