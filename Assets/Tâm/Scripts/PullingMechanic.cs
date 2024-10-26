using UnityEngine;
using UnityEngine.UI;

public class PullingMechanic : MonoBehaviour
{
	public GameObject npc; // NPC đang bị kéo
	public float pullDistance = 1.0f; // Khoảng cách kéo mỗi lần đạt ngưỡng nhấn
	public int requiredPressCount = 10; // Số lần nhấn cần thiết để kéo NPC
	public float timeWindow = 1.0f; // Thời gian tính số lần nhấn

	private float pressCount = 0;

	private Animator playerAnimator;
	private Animator npcAnimator;
	private bool isPulling = false;

	public Slider pullingPower;

	Camera camera;

	private void Start()
	{
		playerAnimator = GetComponent<Animator>();
		npcAnimator = npc.GetComponent<Animator>();
		pullingPower.maxValue = requiredPressCount;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			pressCount++;
			//playerAnimator.SetTrigger("Pull"); // Chạy animation kéo
			//npcAnimator.SetTrigger("BeingPulled"); // Chạy animation NPC bị kéo
		}
		pullingPower.value = pressCount;
		pressCount -= 3f * Time.deltaTime;

		if (pressCount >= requiredPressCount)
		{
			PullNPC();
			pressCount = 0;
		}
	}

	private void PullNPC()
	{
		// Di chuyển NPC ra xa vị trí hiện tại
		npc.transform.position += Vector3.right * pullDistance;
	}
}
