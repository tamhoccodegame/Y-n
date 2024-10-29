using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueLine
{
	public string speaker;
	public string sentence;
}

public class DialogueChoice
{
    public string yesText;
    public string noText;
    public Action onYes;
    public Action onNo;
}

public class Dialogue
{
    public int number;
	public List<DialogueLine> lines;
	public bool hasChoice;
	public DialogueChoice choice;
}

public class DialogueDatabase : MonoBehaviour
{
    public List<Dialogue> dialogues = new List<Dialogue>()
    {
        new Dialogue
        {
            number = 0,
            lines = new List<DialogueLine>()
            {
                new DialogueLine 
                { 
                    speaker = "Cô Nhung Bán Nước", 
                    sentence = "Ê cậu trai ơi, có muốn thử tài pha chế nước trái cây không? Cô chỉ cho!"
                },
            },
            hasChoice = true,
            choice = new DialogueChoice
            {
                yesText = "Dạ, cho con thử với!",
                noText = "Hì…Dạ thui cô",
                onYes = () =>
                {
                    //GameManager.instance.LoadScene("MNG_Phachenuoc");
                    Debug.Log("Yes yes yes!");
                },
				onNo = () =>
				{
                    //GameManager.instance.LoadScene("MNG_Phachenuoc");
                    Debug.Log("No no no");
                }
			}
        }
    };

    
    public Dialogue GetDialogue(int index)
    {
        if(index >= 0 && index < dialogues.Count)
        {
            return dialogues[index];
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
