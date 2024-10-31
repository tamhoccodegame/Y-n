using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ComicCutscene : MonoBehaviour
{
    public PlayableDirector cutscene;
	public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
		cutscene.stopped += Cutscene_stopped; ;
    }

	private void Cutscene_stopped(PlayableDirector obj)
	{
		GameManager.instance.LoadScence(sceneToLoad);
	}

}
