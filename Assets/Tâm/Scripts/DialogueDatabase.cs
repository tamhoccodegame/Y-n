using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
	public string speaker;
	public string sentence;
}

[System.Serializable]
public class Dialogue
{
	public List<DialogueLine> lines;
}

public class DialogueDatabase : MonoBehaviour
{
    public List<Dialogue> dialogues;
    public List<DialogueLine> GetDialogue(int index)
    {
        if(index >= 0 && index < dialogues.Count)
        {
            return dialogues[index].lines;
        }
        return null;
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
