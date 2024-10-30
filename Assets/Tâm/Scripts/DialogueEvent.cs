using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour, ITriggerable
{
    public string dialogueName;
	public void TriggerAction()
	{
        GameManager.instance.StartDialogue(dialogueName);
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
