using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeEvent : MonoBehaviour, ITriggerable
{
    public CameraShake cameraShake;
    public float amplitude;
    public float frequency;
    public float duration;

	public TriggerType GetTriggerType() => TriggerType.Auto;

	public void HidePrompt()
	{
		
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		cameraShake.ShakeCamera(amplitude, frequency, duration);
		Destroy(gameObject);
	}

	public void ShowPrompt()
	{
        
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
