using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeath : MonoBehaviour
{
    public int maxHearts;
    public int currentHearts;

	private void Start()
	{
        currentHearts = GameManager.instance.playerHearts;
		if(currentHearts <= 0)
        {
            currentHearts = maxHearts;
        }
		Invoke(nameof(DelayDisplayHeart), 0.1f);
	}

    private void DelayDisplayHeart()
    {
        GameManager.instance.UpdateHealthUI(currentHearts);
    }

	[ContextMenu("Substract Health")]
	public void TakeDamage()
    {
        currentHearts--;
        currentHearts = Mathf.Max(currentHearts, 0);
        GetComponent<SimpleFlash>().Flash();
        GameManager.instance.UpdateHealthUI(currentHearts);
        HitStop.instance.Stop();
        if(currentHearts <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        GameManager.instance.GameOver();
    }

	private void OnDestroy()
	{
        //Lưu lại máu khi chuyển scene
        GameManager.instance.playerHearts = currentHearts;
	}
}
