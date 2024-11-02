using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMinigameEvent : MonoBehaviour, ITriggerable
{
	public void TriggerAction()
	{
		GameManager.instance.StartMNGRapidButton();
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
