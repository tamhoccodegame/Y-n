using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingIdleState : IPlayerState
{
	public void EnterState(PlayerStateManager player)
	{
		player.animator.Play("Idle");
	}


	public void UpdateState(PlayerStateManager player)
	{
		if (player.MoveInput != 0) player.SwitchState(player.carryingWalkingState);
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
