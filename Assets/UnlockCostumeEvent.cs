using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCostumeEvent : MonoBehaviour, ITriggerable
{
    public string costumeName;

	public void TriggerAction()
	{
		GameManager.instance.UnlockCostume(costumeName);
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
