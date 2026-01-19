using System.Threading;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D RB { get; private set; }

    public float CurrentSpeed { get; set; }
    public float VerticalSpeed { get; set; }

    public Vector2 MagneticForce { get; set; }

    public bool CanJump {  get; set; }

    public PlayerState CurrentState { get; private set; }

    [Header("Universal Movement Variables")]
    public float maxSpeed;
    public float jumpSpeed;
    public float gravity;

    public PlayerGroundState groundState = new PlayerGroundState();
    public PlayerAirState airState = new PlayerAirState();

    [Header("Collision Information")]
    public Vector2 groundCollisionOffset;
    public Vector2 headCollisionOffset;
    public Vector2 collisionRadius;
    public LayerMask collisionMask;

    void Start()
    {
        transform.position = PlayerManager.instance.startingPosition;

        RB = GetComponent<Rigidbody2D>();

        SetState(groundState);
    }

    void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState(this);
            CurrentState.ChangeState(this);
        }
    }

    public void SetState(PlayerState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState(this);
        }

        CurrentState = newState;

        if (CurrentState != null)
        {
            CurrentState.StartState(this);
        }
    }

    public void MovePlayer()
    {
        Vector2 magnet = MagneticForce;
        Vector2 velocity = Vector2.right * CurrentSpeed;

        velocity.x += magnet.x;

        if (magnet.y != 0f)
        {
            VerticalSpeed = magnet.y;
        }

        velocity.y = VerticalSpeed;

        RB.MovePosition(RB.position + velocity * Time.deltaTime);
    }
}
