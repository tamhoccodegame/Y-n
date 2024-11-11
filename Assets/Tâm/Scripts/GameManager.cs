using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

	public Image fadeImage;
	public float fadeDuration;

	public int skillPoint = 0;

	public Vector3 playerPosition;
	public int playerHearts;
	public string previousSceneName;
	public bool hasSavedState = false;

	public bool isLoadPrevious = false;
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

	public void PlayAudio(string soundName)
	{
		audioManager.PlayAudio(soundName);
	}

	public void StopAllAudio()
	{
		audioManager.StopAllAudio();
	}

	public void AddSkillPoint(int ammount)
	{
		skillPoint += ammount;
		uiManager.UpdateSkillPoint(skillPoint);
	}

	public  void RemoveSkillPoint(int ammount)
	{
		skillPoint -= ammount;
		uiManager.UpdateSkillPoint(skillPoint);
	}

	public List<Diary> GetUnlockedDiary()
	{
		return unlockedDiaryList;
	}
	public void UnlockedDiary(int diaryIndex)
	{
		Diary diary = diaryDatabase.GetDiary(diaryIndex);
		if (diary != null)
		{
			unlockedDiaryList.Add(diary);
			uiManager.UpdateVisual();
			uiManager.isDiaryUnlocked = true;
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
			uiManager.isCostumeUnlocked = true;
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

	public void ActiveQuest(string questName)
	{
		Quest quest = questDatabase.GetQuest(questName);
		questManager.ActiveQuest(quest);
		uiManager.StartQuest(quest);
	}

	public void CompletedQuest(string questName)
	{
		Quest quest = questDatabase.GetQuest(questName);
		questManager.CompleteQuest(quest);
		uiManager.EndQuest();
	}

	public void TriggerEndQuest(string questName)
	{
		Quest quest = questDatabase.GetQuest(questName);
		questManager.TriggerEndQuest(quest);
	}

	public void StartMNGRapidButton()
	{
		SetIsControllable(false);
		uiManager.StartRapidButtonMNG();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			uiManager.UpdateUIMenu();
		}
	}

	public void Save(Vector3 playerPosition, int playerHeats)
	{
		this.playerPosition = playerPosition;
		this.playerHearts = playerHeats;
		this.previousSceneName = SceneManager.GetActiveScene().name;
		hasSavedState = true;
	}

	private IEnumerator FadeOutIn(string sceneName)
	{
		yield return StartCoroutine(Fade(1));
		SceneManager.LoadScene(sceneName);
		yield return StartCoroutine(Fade(0));
		isLoadPrevious = false;
	}

	private IEnumerator Fade(float targetAlpha)
	{
		float startAlpha = fadeImage.color.a;
		float time = 0;

		while (time < fadeDuration)
		{
			time += Time.deltaTime;
			float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
			yield return null;
		}

		fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
	}

	public void LoadScene(string sceneName)
	{
		PlayerHeath player = FindObjectOfType<PlayerHeath>();
		if (player != null)
		{
			int playerCurrentHearts = player.currentHearts;
			Vector3 playerCurrentPosition = player.gameObject.transform.position;
			Save(playerCurrentPosition, playerCurrentHearts);
		}
		StopAllCoroutines();
		StartCoroutine(FadeOutIn(sceneName));
	}

	public void HideUI()
	{
		uiManager.gameObject.SetActive(false);
	}

	public void LoadPreviousScene()
	{
		isLoadPrevious = true;
		StopAllCoroutines();
		StartCoroutine(FadeOutIn(previousSceneName));
		uiManager.gameObject.SetActive(true);
		uiManager.StartScene();
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
