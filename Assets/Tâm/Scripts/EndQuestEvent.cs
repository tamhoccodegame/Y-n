using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndQuestEvent : MonoBehaviour, ITriggerable
{
    public string questName;
	public void TriggerAction()
	{
        GameManager.instance.EndQuest(questName);
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
