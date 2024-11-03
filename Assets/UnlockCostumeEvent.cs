using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCostumeEvent : MonoBehaviour, ITriggerable
{
    public string costumeName;

	public TriggerType GetTriggerType() => TriggerType.Auto;

	public void HidePrompt()
	{
	
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		GameManager.instance.UnlockCostume(costumeName);
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
