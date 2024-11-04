using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTutorial : MonoBehaviour
{
    public GameObject UITutorial;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnDisable()
	{
        Time.timeScale = 1;
	}
}
