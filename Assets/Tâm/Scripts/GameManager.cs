using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uiManager;
    private DialogueDatabase dialogueDatabase;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

	void Start()
	{
        uiManager = GetComponentInChildren<UIManager>();
        dialogueDatabase = GetComponentInChildren<DialogueDatabase>();
	}

	public void StartDialogue(int dialougeIndex)
    {   
        Dialogue dialogue = dialogueDatabase.GetDialogue(dialougeIndex);
        if (dialogue != null)
        {
           uiManager.StartDialogue(dialogue);
        }
        else
        {
            Debug.LogError("DialogueDatabase is not assigned");
        }
    }

    public void LoadScence(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
