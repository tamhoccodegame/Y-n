using UnityEngine;

public class JumpingState : IPlayerState
{
	public void EnterState(PlayerStateManager player)
	{
		player.isGrounded = false;
		player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
		player.jumpTimeCounter = player.maxJumpTime;
		GameManager.instance.PlayAudio("Jump");
	}

	public void UpdateState(PlayerStateManager player)
	{
		player.HandleMovement();

		// Nếu vẫn giữ phím nhảy và còn thời gian nhảy
		if (Input.GetKey(KeyCode.Space) && player.jumpTimeCounter > 0)
		{
			player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
			player.jumpTimeCounter -= Time.deltaTime;
		}

		// Dừng nhảy khi thả phím hoặc hết thời gian nhảy
		if (Input.GetKeyUp(KeyCode.Space) || player.jumpTimeCounter <= 0)
		{
			player.jumpTimeCounter = 0;
		}

		if(player.rb.velocity.y == 0)
		{
			if (player.MoveInput != 0) player.SwitchState(player.walkingState);
			else player.SwitchState(player.idleState);
		}

	}

	public void ExitState(PlayerStateManager player)
	{
		// Không cần xử lý gì thêm khi rời khỏi trạng thái nhảy
		player.isGrounded = true;
		GameManager.instance.PlayAudio("Land");
	}
}
