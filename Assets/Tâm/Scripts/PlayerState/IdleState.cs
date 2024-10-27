using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerState
{
	public void EnterState(PlayerStateManager player)
	{

	}

	public void UpdateState(PlayerStateManager player)
	{
		if(player.MoveInput != 0) player.SwitchState(player.walkingState);

		if (Input.GetKeyDown(KeyCode.Space)) player.SwitchState(player.jumpingState);
	}
	public void ExitState(PlayerStateManager player)
	{
		
	}
}
