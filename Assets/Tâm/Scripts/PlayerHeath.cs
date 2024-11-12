using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeath : MonoBehaviour
{
    public int maxHearts;
    public int currentHearts;
    public GameObject hitEffect;
    private bool isVulnerable = true;
    public float vulnerableCooldown;
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
        if (!isVulnerable) return;
        StartCoroutine(TakeDamageCoroutine());
        

    }

    IEnumerator TakeDamageCoroutine()
    {
        currentHearts--;
        currentHearts = Mathf.Max(currentHearts, 0);
        GetComponent<SimpleFlash>().Flash();
        GameManager.instance.UpdateHealthUI(currentHearts);
        HitStop.instance.Stop();
        Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        if (currentHearts <= 0)
        {
            Die();
        }

        isVulnerable = false;

        float vulnerableTimer = vulnerableCooldown;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        while (vulnerableTimer > 0)
        {
            renderer.enabled = !renderer.enabled;

            yield return new WaitForSeconds(0.1f);

			vulnerableTimer -= 0.1f;
		}
    
        renderer.enabled = true;
        isVulnerable = true;
	}

    public void Die()
    {
        GameManager.instance.LoadPreviousScene();
    }

	private void OnDestroy()
	{
        //Lưu lại máu khi chuyển scene
        GameManager.instance.playerHearts = currentHearts;
	}
}
