using UnityEngine;

[System.Serializable]
public class PlayerGroundState : PlayerState
{
    [SerializeField] float accel;
    [SerializeField] float decel;
    [SerializeField] float fric;
    [SerializeField] float coyoteTime;

    float currentCoyoteTime;

    public override void StartState(PlayerController player)
    {
        if (input == null)
        {
            input = InputManager.instance;
        }

        player.VerticalSpeed = 0f;
        player.CanJump = false;

        currentCoyoteTime = coyoteTime;
    }

    public override void UpdateState(PlayerController player)
    {
        if (input.Move.x > 0f)
        {
            if (player.CurrentSpeed < 0f)
            {
                player.CurrentSpeed += decel * Time.deltaTime;
            }
            else
            {
                if (player.CurrentSpeed < player.maxSpeed)
                {
                    player.CurrentSpeed += accel * Time.deltaTime;
                }
                else
                {
                    player.CurrentSpeed = player.maxSpeed;
                }
            }
        }
        else if (input.Move.x < 0f)
        {
            if (player.CurrentSpeed > 0f)
            {
                player.CurrentSpeed -= decel * Time.deltaTime;
            }
            else
            {
                if (player.CurrentSpeed > -player.maxSpeed)
                {
                    player.CurrentSpeed -= accel * Time.deltaTime;
                }
                else
                {
                    player.CurrentSpeed = -player.maxSpeed;
                }
            }
        }
        else
        {
            player.CurrentSpeed -= Mathf.Min(fric * Time.deltaTime, Mathf.Abs(player.CurrentSpeed)) * Mathf.Sign(player.CurrentSpeed);
        }

        if (input.Jump && player.CanJump)
        {
            player.VerticalSpeed = player.jumpSpeed;
        }

        if (!input.Jump && !player.CanJump)
        {
            player.CanJump = true;
        }

        player.MovePlayer();
    }

    public override void ChangeState(PlayerController player)
    {
        if (!Physics2D.OverlapBox(player.RB.position + player.groundCollisionOffset, player.collisionRadius, 0f, LayerMask.GetMask("Solid")))
        {
            currentCoyoteTime -= Time.deltaTime;
        }
        else
        {
            currentCoyoteTime = coyoteTime;
        }

        if (player.VerticalSpeed > 0f || currentCoyoteTime <= 0f)
        {
            player.SetState(player.airState);
        }
    }

    public override void ExitState(PlayerController player)
    {
        
    }
}
