using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public float interactionRange = 1f; // Khoảng cách tương tác
	private WaterBucket waterBucket;
	public Transform holdingPoint;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			Interact();
		}
	}

	private void Interact()
	{
		// Kiểm tra va chạm trong phạm vi tròn nhỏ quanh người chơi
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange);

		foreach (var hit in hits)
		{
			if (hit.CompareTag("WaterBucket") && waterBucket == null)
			{
				waterBucket = hit.GetComponent<WaterBucket>();
				waterBucket.PickUpBucket(holdingPoint);
				PlayerStateManager player = GetComponent<PlayerStateManager>();
				player.PlayerState = player.carryingIdleState;
			}
			else if (hit.CompareTag("WaterPool") && waterBucket != null && !waterBucket.isFull)
			{
				waterBucket.FillBucket();
			}
			else if (hit.CompareTag("BurningHouse") && waterBucket != null && waterBucket.isFull)
			{
				BurningHouse house = hit.GetComponent<BurningHouse>();
				if (house != null)
				{
					house.ExtinguishFire();
					waterBucket.EmptyBucket();
				}
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		// Vẽ một vòng tròn để dễ dàng thấy phạm vi tương tác trong trình chỉnh sửa
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, interactionRange);
	}
}
