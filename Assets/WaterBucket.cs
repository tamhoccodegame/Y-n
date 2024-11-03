using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour, ITriggerable
{
    public bool isFull = false;
    public bool isPickup = false;
    public Sprite waterBucketRenderer;
    public Sprite fullWaterBucketRenderer;

    public GameObject interactPrompt;

    public TriggerType GetTriggerType() => TriggerType.Optional;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(isPickup && Input.GetKeyDown(KeyCode.E))
		{
			if(isFull)
			{
				EmptyBucket();
			}
			else
			{
				FillBucket();
			}
		}
    }

    public void PickUpBucket(Transform holdingPoint)
    {
        transform.position = holdingPoint.position;
        transform.SetParent(holdingPoint);
        isPickup = true;
		GetComponent<Collider2D>().enabled = false;
	}

	public void FillBucket()
    {
		Collider2D[] waterSources = Physics2D.OverlapCircleAll(transform.position, 2f); // Bán kính 1 đơn vị
		foreach (var source in waterSources)
		{
			if (source.CompareTag("WaterPool")) // Kiểm tra tag của đối tượng
			{
				isFull = true;
				UpdateBucketSprite();
				GameManager.instance.PlayAudio("Muc_Nuoc");
				Debug.Log("Bucket filled with water!");
				return; // Thoát sau khi đã tìm thấy nguồn nước
			}
		}
		Debug.Log("No water source nearby to fill the bucket.");
	}

    public void EmptyBucket()
    {
		// Kiểm tra có nhà đang cháy gần đó không
		Collider2D[] burningHouses = Physics2D.OverlapCircleAll(transform.position, 2f); // Bán kính 1 đơn vị
		foreach (var house in burningHouses)
		{
			if (house.CompareTag("BurningHouse")) // Kiểm tra tag của đối tượng
			{
                house.GetComponent<BurningHouse>().ExtinguishFire();
				isFull = false;
				UpdateBucketSprite();
				GameManager.instance.PlayAudio("Dap_Lua");
				Debug.Log("Bucket emptied!");
				return; // Thoát sau khi đã tìm thấy nhà cháy
			}
		}
		Debug.Log("No burning house nearby to empty the bucket.");
	}

    private void UpdateBucketSprite()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.sprite = isFull ? fullWaterBucketRenderer : waterBucketRenderer;
        
    }

	public void ShowPrompt()
	{
		interactPrompt.SetActive(true);
	}

	public void HidePrompt()
	{
        interactPrompt.SetActive(false);
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
        if (!isPickup)
        {
            PickUpBucket(playerInteration.holdingPoint);
        }
	}

	private void OnDrawGizmos()
	{
		// Vẽ gizmo cho bán kính kiểm tra nguồn nước
		Gizmos.color = Color.blue; // Màu cho gizmo nước
		Gizmos.DrawWireSphere(transform.position, 2f); // Vẽ vòng tròn quanh vị trí của xô nước

		// Vẽ gizmo cho bán kính kiểm tra nhà cháy
		Gizmos.color = Color.red; // Màu cho gizmo nhà cháy
		Gizmos.DrawWireSphere(transform.position, 2f); // Vẽ vòng tròn quanh vị trí của xô nước
	}
}
