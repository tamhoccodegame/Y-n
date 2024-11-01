using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartQuestEvent : MonoBehaviour, ITriggerable
{
    public string questName;
    public List<GameObject> components;
	public void TriggerAction()
	{
		GameManager.instance.ActivateQuest(questName, components);
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
