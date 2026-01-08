using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class MagneticSource : MonoBehaviour
{
    [SerializeField] float field;
    [SerializeField] float strength;
    [SerializeField, Range(-1, 1)] int polarity;

    MagneticBody myBody;
    CircleCollider2D myCollider;

    public List<MagneticBody> bodies = new List<MagneticBody>();

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
    }

    void FixedUpdate()
    {
        foreach (MagneticBody b in bodies)
        {
            Vector2 direction = new Vector2(transform.position.x, transform.position.y) - new Vector2(b.transform.position.x, b.transform.position.y);
            b.MoveBody(direction, strength, polarity);
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
