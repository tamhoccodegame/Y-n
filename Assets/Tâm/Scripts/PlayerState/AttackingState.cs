using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IPlayerState
{
	void IPlayerState.EnterState(PlayerStateManager player)
	{
		player.animator.Play("Attack");
	}

	void IPlayerState.UpdateState(PlayerStateManager player)
	{
		if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) player.SwitchState(player.idleState);
	}

	void IPlayerState.ExitState(PlayerStateManager player)
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
