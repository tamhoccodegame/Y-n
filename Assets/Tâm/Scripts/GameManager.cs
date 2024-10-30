using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uiManager;
    private AudioManager audioManager;
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
        audioManager = GetComponentInChildren<AudioManager>();
	}

	public void StartDialogue(string dialgueName)
    {   
        Dialogue dialogue = dialogueDatabase.GetDialogue(dialgueName);
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

    public void PlayAudio(string soundName)
    {
        audioManager.PlayAudio(soundName);
    }

    public void StopAllAudio()
    {
        audioManager.StopAllAudio();
    }

}
