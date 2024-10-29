using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeEvent : MonoBehaviour, ITriggerable
{
    public CameraShake cameraShake;
    public float amplitude;
    public float frequency;
    public float duration;
	public void TriggerAction()
	{
        cameraShake.ShakeCamera(amplitude, frequency, duration);
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
