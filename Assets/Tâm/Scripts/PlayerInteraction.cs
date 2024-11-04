using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public float interactionRange = 1f; // Khoảng cách tương tác
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
			Debug.Log(triggerable.GetTriggerType().ToString());
			if(triggerable.GetTriggerType() == TriggerType.Optional)
			{
				triggerable.ShowPrompt();
			}
			else if(triggerable.GetTriggerType() == TriggerType.Auto) 
			{
				triggerable.OnInteract(this);
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
