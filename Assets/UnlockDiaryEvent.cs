using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDiaryEvent : MonoBehaviour, ITriggerable
{
    public int diaryOrder;

	public void TriggerAction()
	{
		GameManager.instance.UnlockedDiary(diaryOrder);
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
