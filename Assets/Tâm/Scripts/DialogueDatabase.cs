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
    public string name;
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
			name = "Chuot_Khoc",
			lines = new List<DialogueLine>()
			{
				new DialogueLine
				{
					speaker = "Cậu bé chuột",
					sentence = "Chít chít.... Cứu béee..."
				},
			},
			hasChoice = false,
		},

		new Dialogue
        {
            name = "MNG_NuocTraiCay",
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
					GameManager.instance.LoadScene("MNG_Phachenuoc");
					Debug.Log("Yes yes yes!");
                },
				onNo = () =>
				{
                    //GameManager.instance.LoadScene("MNG_Phachenuoc");
                    Debug.Log("No no no");
                }
			}
        },

		new Dialogue
		{
			name = "MNG_DuaGhe",
			lines = new List<DialogueLine>()
			{
				new DialogueLine
				{
					speaker = "Chú Ba",
					sentence = "Này cậu ơi, muốn thử sức với bọn tôi trong cuộc đua ghe không? Có thưởng lớn lắm đó nhe!"
				},
			},
			hasChoice = true,
			choice = new DialogueChoice
			{
				yesText = "Đua ghe hả? Chơi luôn chứ!",
				noText = "Con bị sợ tốc độ cao chú ơi...",
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
		},

		new Dialogue
		{
			name = "MNG_CaKheo",
			lines = new List<DialogueLine>()
			{
				new DialogueLine
				{
					speaker = "Chú Tư",
					sentence = "Này cậu trai ơiii! Ra đây chỉ này chơi vui lắm nè"
				},
			},
			hasChoice = true,
			choice = new DialogueChoice
			{
				yesText = "Chơi luôn!",
				noText = "Thôi mình cũng hơi mệt rồi...",
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
		},

		new Dialogue
		{
			name = "MNG_BitMatBatVit",
			lines = new List<DialogueLine>()
			{
				new DialogueLine
				{
					speaker = "Cô Tươi",
					sentence = "Vô đây bắt vịt thử không em trai?"
				},
			},
			hasChoice = true,
			choice = new DialogueChoice
			{
				yesText = "Chơi luôn!",
				noText = "Thôi, mệt quá rồi",
				onYes = () =>
				{
					GameManager.instance.LoadScene("MNG_BitMatBatVit");
					Debug.Log("Yes yes yes!");
				},
				onNo = () =>
				{
                    //GameManager.instance.LoadScene("MNG_Phachenuoc");
                    Debug.Log("No no no");
				}
			}
		},

	};

    
    public Dialogue GetDialogue(string dialogueName)
    {
        Dialogue dialogue = dialogues.Find(d => d.name == dialogueName);

        if(dialogue != null)
        {
            return dialogue;
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
