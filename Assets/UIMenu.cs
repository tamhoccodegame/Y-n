using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIMenu : MonoBehaviour
{
    public GameObject[] tabContents;
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
        foreach(GameObject tab in tabContents)
        {
           tab.SetActive(tab.name == tabName);
        }
	}
}
