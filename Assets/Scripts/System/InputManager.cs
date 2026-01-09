using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public Vector2 Move { get { return move; } }
    public bool Jump { get { return jump; } }
    public bool Attack { get { return attack; } }
    public bool Beam { get { return beam; } }
    public int Polarity { get { return polarity; } }
    public bool Pause { get {  return pause; } }

    public static InputManager instance;
    PlayerInput input;

    Vector2 move;
    bool jump;
    bool attack;
    bool beam;
    int polarity;
    bool pause;

    void Awake() 
    { 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        input = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        input.onActionTriggered += OnAction;
    }

    void OnDisable()
    {
        input.onActionTriggered -= OnAction;
    }

    void OnAction(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                move = context.ReadValue<Vector2>();
                break;
            case "Jump":
                SetBool(context, ref jump);
                break;
            case "Attack":
                SetBool(context, ref attack);
                break;
            case "Beam":
                SetBool(context, ref beam);
                break;
            case "Polarity":
                polarity = (int)context.ReadValue<float>();
                break;
            case "Pause":
                SetBool(context, ref pause);
                break;
        }
    }

    void SetBool(InputAction.CallbackContext context, ref bool value)
    {
        if (context.performed)
        {
            value = true;
        }

        if (context.canceled)
        {
            value = false;
        }
    }
}
