using UnityEngine;

[System.Serializable]
public class PlayerAirState : PlayerState
{
    [SerializeField] float fallSpeed;
    [SerializeField] float jumpBufferMultiplier;

    bool isJumping;

    public override void StartState(PlayerController player)
    {
        if (input == null)
        {
            input = InputManager.instance;
        }

        player.CanJump = false;
        isJumping = false;
    }

    public override void UpdateState(PlayerController player)
    {
        if (input.Move.x != 0f)
        {
            player.CurrentSpeed = player.maxSpeed * input.Move.x;
        }
        else
        {
            player.CurrentSpeed = 0f;
        }

        if (!input.Jump || Physics2D.OverlapBox(player.RB.position + player.headCollisionOffset, player.collisionRadius, 0f, player.collisionMask))
        {
            player.VerticalSpeed = Mathf.Min(0f, player.VerticalSpeed);
        }

        if (player.VerticalSpeed > fallSpeed)
        {
            player.VerticalSpeed += player.gravity * Time.deltaTime;
        }

        player.MovePlayer();
    }

    public override void ChangeState(PlayerController player)
    {
        if (Physics2D.OverlapBox(player.RB.position + player.groundCollisionOffset * jumpBufferMultiplier, player.collisionRadius + new Vector2(0f, 1.5f), 0f, player.collisionMask))
        {
            if (player.CanJump)
            {
                if (input.Jump)
                {
                    isJumping = true;
                    player.CanJump = false;
                }
            }

            if (!input.Jump)
            {
                isJumping = false;
            }

            if (!input.Jump && !player.CanJump)
            {
                player.CanJump = true;
            }
        }
        else
        {
            isJumping = false;
        }

        if (player.VerticalSpeed <= 0f && Physics2D.OverlapBox(player.RB.position + player.groundCollisionOffset, player.collisionRadius, 0f, player.collisionMask))
        {
            if (!isJumping)
            {
                player.SetState(player.groundState);
            }
            else
            {
                player.VerticalSpeed = player.jumpSpeed;
            }
        }
    }

    public override void ExitState(PlayerController player)
    {
        
    }
}
