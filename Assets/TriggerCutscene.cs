using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public PlayableDirector cutescene;

	private void Start()
	{
		cutescene.stopped += Cutescene_stopped;
	}

	private void Cutescene_stopped(PlayableDirector obj)
	{
		GameManager.instance.LoadScene("Scene_4");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			cutescene.Play();
		}
	}
}
