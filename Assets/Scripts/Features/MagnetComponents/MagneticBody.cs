using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagneticBody : MonoBehaviour
{
    [SerializeField, Range(-1, 1)] int polarity;

    Rigidbody2D rb;

    SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (polarity == -1)
        {
            sprite.color = Color.blue;
        }
        else if (polarity == 0)
        {
            sprite.color = Color.green;
        }
        else if (polarity == 1)
        {
            sprite.color = Color.red;
        }
    }

    public void SetPolarity(int newPolarity)
    {
        polarity = newPolarity;
    }

    public void MoveBody(Vector2 direction, float strength, int pole)
    {
        int force = (polarity * pole) * -1;

        Vector2 velocity = direction.normalized;
        velocity = velocity * strength * force;

        rb.AddForce(velocity);
    }
}
