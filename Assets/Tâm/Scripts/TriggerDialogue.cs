using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public int dialogueIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.instance.StartDialogue(dialogueIndex);
        }
	}
}
