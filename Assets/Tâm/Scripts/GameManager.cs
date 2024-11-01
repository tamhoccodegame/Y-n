using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("==========Manager==========")]
    private UIManager uiManager;
    private AudioManager audioManager;
    private QuestManager questManager;

	[Header("==========Database==========")]
	private DialogueDatabase dialogueDatabase;
    private DiaryDatabase diaryDatabase;
    private CostumeDatabase costumeDatabase;
    private QuestDatabase questDatabase;

    [Header("==========List==========")]
	private List<Diary> unlockedDiaryList = new List<Diary>();
    private List<Costume> unlockedCostumeList = new List<Costume>();

    [Header("==========Player==========")]
	private bool isPlayerControllable = true;
    public int currentPlayerHearts;


    [Header("==========Scenes==========")]
    private string previousScene = null;

	private Dictionary<string, object> components = new Dictionary<string, object>();
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
        dialogueDatabase = GetComponentInChildren<DialogueDatabase>();
        diaryDatabase = GetComponentInChildren<DiaryDatabase>();
        costumeDatabase = GetComponentInChildren<CostumeDatabase>();
        questDatabase = GetComponentInChildren<QuestDatabase>();

		uiManager = GetComponentInChildren<UIManager>();
		audioManager = GetComponentInChildren<AudioManager>();
        questManager = GetComponentInChildren<QuestManager>();

		RegisterComponent("MainCamera", Camera.main);
		RegisterComponent("PlayerStateManager", FindObjectOfType<PlayerStateManager>());
	}

    public bool IsControllable()
    {
        return isPlayerControllable;
    }

    public void SetIsControllable(bool _isControllable)
    {
        isPlayerControllable = _isControllable;
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

    public void StartQuest(string questName)
    {
        Quest quest = questDatabase.GetQuest(questName);
        uiManager.StartQuest(quest);
        questManager.StartQuest(questName);
    }

    public void EndQuest(string questName)
    {
        Quest quest = questDatabase.GetQuest(questName);
        //uiManager.En
    }

    public void PlayAudio(string soundName)
    {
        audioManager.PlayAudio(soundName);
    }

    public void StopAllAudio()
    {
        audioManager.StopAllAudio();
    }

    public List<Diary> GetUnlockedDiary()
    {
        return unlockedDiaryList;
    }
    public void UnlockedDiary(int diaryIndex)
    {
        Diary diary = diaryDatabase.GetDiary(diaryIndex);
        if(diary != null)
        {
			unlockedDiaryList.Add(diary);
			uiManager.UpdateVisual();
		}
        else
        {
            Debug.LogError("Couldn't find diary");
        }
    }

    public void UnlockCostume(string costumeName)
    {
        Costume costume = costumeDatabase.GetCostume(costumeName);
        if (costume != null)
        {
            unlockedCostumeList.Add(costume);
            uiManager.UpdateVisual();
        }
        else
        {
            Debug.LogError("Couldn't find costume");
        }
    }
    
    public List<Costume> GetCostumeList()
    {
        return unlockedCostumeList;
    }

	public void RegisterComponent(string key, object component)
	{
		if (!components.ContainsKey(key))
		{
			components.Add(key, component);
		}
	}

	public object GetComponentByKey(string key)
	{
		components.TryGetValue(key, out var component);
		return component;
	}

	public void ActivateQuest(string questName, List<GameObject> components) // Nhận danh sách đối tượng
	{
		Quest quest = questDatabase.GetQuest(questName); // Truy cập quest từ QuestDatabase
		if (quest != null)
		{
			questManager.ActivateQuest(questName, components); // Gọi phương thức để kích hoạt quest và truyền các đối tượng
			Debug.Log($"Quest '{questName}' activated!");
		}
		else
		{
			Debug.LogWarning($"Quest '{questName}' not found!");
		}
	}


	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.P))
        {
            uiManager.UpdateUIMenu();
        }
	}

	public void LoadScene(string sceneName)
	{
        previousScene = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(sceneName);
        uiManager.StartScene();
	}

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScene);
    }

    public void UpdateHealthUI(int currentHealth)
    {
        uiManager.UpdateHealthUI(currentHealth);
    }

    public void GameOver()
    {
        uiManager.GameOver();
    }
}
