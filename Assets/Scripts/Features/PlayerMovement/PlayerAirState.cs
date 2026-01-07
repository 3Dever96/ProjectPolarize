using UnityEngine;

[System.Serializable]
public class PlayerAirState : PlayerState
{
    [SerializeField] float fallSpeed;

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
        if (player.VerticalSpeed <= 0f && Physics2D.OverlapBox(player.RB.position + player.groundCollisionOffset, player.collisionRadius, 0f, player.collisionMask))
        {
            player.SetState(player.groundState);
        }
    }

    public override void ExitState(PlayerController player)
    {
        
    }
}
