using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class MagneticSource : MonoBehaviour
{
    [SerializeField] float field;
    public float strength;
    [Range(-1, 1)]  public int polarity;

    [SerializeField] Color positiveColor;
    [SerializeField] Color neutralColor;
    [SerializeField] Color negativeColor;

    MagneticBody myBody;
    CircleCollider2D myCollider;
    SpriteRenderer sprite;

    List<MagneticBody> bodies = new List<MagneticBody>();

    void Start()
    {
        myBody = GetComponent<MagneticBody>();

        if (myBody != null)
        {
            myBody.SetPolarity(polarity);
        }

        myCollider = GetComponent<CircleCollider2D>();

        myCollider.radius = field;
        myCollider.isTrigger = true;

        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        foreach (MagneticBody b in bodies)
        {
            Vector2 direction = new Vector2(transform.position.x, transform.position.y) - new Vector2(b.transform.position.x, b.transform.position.y);
            b.MoveBody(direction, strength, polarity);
        }

        switch (polarity)
        {
            case -1:
                sprite.color = negativeColor;
                break;
            case 0:
                sprite.color = neutralColor;
                break;
            case 1:
                sprite.color = positiveColor;
                break;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            MagneticBody newBody = collision.gameObject.GetComponent<MagneticBody>();

            if (newBody != null)
            {
                if (myBody != null)
                {
                    if (newBody != myBody)
                    {
                        if (!bodies.Contains(newBody))
                        {
                            bodies.Add(newBody);
                        }
                    }
                }
                else
                {
                    if (!bodies.Contains(newBody))
                    {
                        bodies.Add(newBody);
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            MagneticBody newBody = collision.gameObject.GetComponent<MagneticBody>();

            if (newBody != null)
            {
                if (bodies.Contains(newBody))
                {
                    bodies.Remove(newBody);
                }
            }
        }
    }
}
