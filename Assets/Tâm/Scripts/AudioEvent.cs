using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : MonoBehaviour, ITriggerable
{
    public string audioName;
	public void TriggerAction()
	{
		GameManager.instance.PlayAudio(audioName);
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
