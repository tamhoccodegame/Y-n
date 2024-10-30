using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class Costume
{
	public Sprite costumeSprite;
	//public string costumeDescription;
}
public class UI_CostumeTab : MonoBehaviour
{
    public List<Costume> costumeList;
    private int currentButtonIndex;

    public Image costumeImage;
    public string costumeDescription;
    public List<Button> button;

    // Start is called before the first frame update
    void Start()
    {
        StartButton();
    }

    void StartButton()
    {
		currentButtonIndex = 0;
		EventSystem.current.SetSelectedGameObject(button[currentButtonIndex].gameObject);
        costumeImage.sprite = costumeList[currentButtonIndex].costumeSprite;
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(currentButtonIndex < button.Count - 1)
            {
                currentButtonIndex++;

                foreach (Button b in button) b.onClick.RemoveAllListeners();

                EventSystem.current.SetSelectedGameObject(button[currentButtonIndex].gameObject);

                button[currentButtonIndex].onClick.AddListener(() =>
                {
                    //Logic mặc đồ
                    //Thay đổi sprite của player
                    //Thay đổi cả animController
                });

				costumeImage.sprite = costumeList[currentButtonIndex].costumeSprite;
			}
			else
			{
				StartButton();
			}
		}
      
    }
}
