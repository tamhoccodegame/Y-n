using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
	public static HitStop instance;
	public float duration = 0.5f; // Thời gian tạm dừng (có thể tùy chỉnh)
	public float slowAmount;
	private bool isStopping = false; // Kiểm tra trạng thái HitStop

	private void Awake()
	{
		instance = this;
	}

	// Hàm gọi để thực hiện hiệu ứng HitStop
	public void Stop()
	{
		if (!isStopping)
		{
			isStopping = true;
			StartCoroutine(HitStopCoroutine());
		}
	}

	private IEnumerator HitStopCoroutine()
	{
		// Lưu lại thời gian TimeScale gốc để khôi phục lại sau đó
		float originalTimeScale = Time.timeScale;

		// Dừng thời gian (hoặc làm chậm)
		Time.timeScale = slowAmount; // hoặc thử Time.timeScale = 0.2f để làm chậm thay vì dừng hẳn
		yield return new WaitForSecondsRealtime(duration); // Sử dụng WaitForSecondsRealtime để không bị ảnh hưởng bởi Time.timeScale

		// Khôi phục lại TimeScale
		Time.timeScale = originalTimeScale;

		isStopping = false;
	}
}
