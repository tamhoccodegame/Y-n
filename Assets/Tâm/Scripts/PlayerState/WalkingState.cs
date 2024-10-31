using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : IPlayerState
{
	public void EnterState(PlayerStateManager player)
	{
		player.animator.Play("Walk");
	}

	public void UpdateState(PlayerStateManager player)
	{
		player.HandleMovement();

		Vector3 currentLocalScale = player.transform.localScale;
		if (player.MoveInput > 0) currentLocalScale.x =  Mathf.Abs(currentLocalScale.x);
		else				      currentLocalScale.x = -Mathf.Abs(currentLocalScale.x);

		if (player.MoveInput == 0) player.SwitchState(player.idleState);

		if (Input.GetKeyDown(KeyCode.Space)) player.SwitchState(player.jumpingState);
	}

	public void ExitState(PlayerStateManager player)
	{
	}


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
