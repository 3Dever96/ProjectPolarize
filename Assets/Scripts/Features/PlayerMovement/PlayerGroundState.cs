using UnityEngine;

[System.Serializable]
public class PlayerGroundState : PlayerState
{
    [SerializeField] float accel;
    [SerializeField] float decel;
    [SerializeField] float fric;

    public override void StartState(PlayerController player)
    {
        if (input == null)
        {
            input = InputManager.instance;
        }
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

        player.MovePlayer();
    }

    public override void ChangeState(PlayerController player)
    {
        
    }

    public override void ExitState(PlayerController player)
    {
        
    }
}
