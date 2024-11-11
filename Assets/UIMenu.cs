using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIMenu : MonoBehaviour
{
    public GameObject[] tabContents;
    public string[] tabNames;
    public Text tabTitle;
    private int currentTabIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTab(string tabName)
    {
        for(int i = 0; i < tabContents.Length; i++)
        {
            GameObject tab = tabContents[i];
            if (tab.name == tabName)
            {
                tab.SetActive(true);
				tabTitle.text = tabNames[i];
                continue;
			}
            tab.SetActive(false);
		}
	}
}
