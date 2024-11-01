using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBoat : MonoBehaviour
{
	Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		animator.enabled = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			animator.enabled = true;
			animator.Play("Floating");
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		animator.enabled = false;
	}
}
