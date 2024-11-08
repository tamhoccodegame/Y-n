using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, ITriggerable
{
	public TriggerType GetTriggerType() => TriggerType.Auto;

	public void HidePrompt()
	{
		
	}

	public void OnInteract(PlayerInteraction playerInteration)
	{
		Vector3 playerPosition = playerInteration.transform.position;
		int playerHeart = playerInteration.gameObject.GetComponent<PlayerHeath>().currentHearts;
		GameManager.instance.Save(playerPosition, playerHeart);
	}

	public void ShowPrompt()
	{
		;
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
