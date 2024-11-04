using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public Transform holdingPoint;

	private ITriggerable currentTriggerable;
	private void Update()
	{
		if(currentTriggerable != null && currentTriggerable.GetTriggerType() == TriggerType.Optional && Input.GetKeyDown(KeyCode.E))
		{
			currentTriggerable.OnInteract(this);
		}	
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		ITriggerable triggerable = collision.GetComponent<ITriggerable>();
		if (triggerable != null)
		{
			currentTriggerable = triggerable;

			if (triggerable.GetTriggerType() == TriggerType.Auto)
			{
				Debug.Log("Auto trigger activated");
				triggerable.OnInteract(this);
			}
			else if (triggerable.GetTriggerType() == TriggerType.Optional)
			{
				Debug.Log("Optional trigger prompt shown");
				triggerable.ShowPrompt();
			}
		}
	}


	private void OnTriggerExit2D(Collider2D collision)
	{
		if(currentTriggerable != null && collision.GetComponent<ITriggerable>() == currentTriggerable)
		{
			if(currentTriggerable.GetTriggerType() == TriggerType.Optional)
			{
				currentTriggerable.HidePrompt();
			}
			currentTriggerable = null;
		}
	}
}
