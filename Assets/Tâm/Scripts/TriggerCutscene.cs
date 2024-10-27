using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TriggerCutscene : MonoBehaviour
{
    public PlayableDirector cutscene;
	public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        cutscene.stopped += Cutscene_stopped;
    }

	private void Cutscene_stopped(PlayableDirector obj)
	{
		playerController.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			cutscene.Play();
			playerController.enabled = false;

		}
	}
}
