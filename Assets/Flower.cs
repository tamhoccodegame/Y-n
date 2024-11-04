using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour, ITriggerable
{
	public GameObject interactPrompt;
	public bool isPickedup = false;
	public TriggerType GetTriggerType() => TriggerType.Optional;

	private void Start()
	{
		interactPrompt.SetActive(false);
	}

	public void HidePrompt()
	{
		interactPrompt.SetActive(false);
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		if (!isPickedup)
		{
			PickUpFlower(playerInteration.holdingPoint);
		}
	}

	public void ShowPrompt()
	{
		interactPrompt.SetActive(true);	
	}

	void PickUpFlower(Transform holdingPoint)
	{
		transform.position = holdingPoint.position;
		transform.SetParent(holdingPoint);
		isPickedup = true;
		GetComponent<Collider2D>().enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        if(isPickedup && Input.GetKeyDown(KeyCode.E))
		{
			ProcessFlower();
		}
    }

	void ProcessFlower()
	{
		Collider2D[] woman = Physics2D.OverlapCircleAll(transform.position, 4f);
		foreach(var w in woman)
		{
			if (w.CompareTag("Woman"))
			{
				GameManager.instance.StartDialogue("GiupDoThaiPhu");
				Destroy(gameObject);
				return;
			}
		}
		Debug.Log("Khong the dung o day");
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, 4f);
	}
}
