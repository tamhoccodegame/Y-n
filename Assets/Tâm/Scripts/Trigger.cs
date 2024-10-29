using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			ITriggerable[] trigger = GetComponents<ITriggerable>();
			if(trigger.Length > 0)
			{
				foreach(ITriggerable t in trigger)
				{
					t.TriggerAction();
				}
			}
			else Debug.LogError("Triggerable components is not assigned");
		}
	}
}
