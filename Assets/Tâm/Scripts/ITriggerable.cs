using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
	Auto,
	Optional,
}

public interface ITriggerable
{
    TriggerType GetTriggerType();
    void ShowPrompt();
    void HidePrompt();
    void OnInteract(PlayerInteraction playerInteration);
}
