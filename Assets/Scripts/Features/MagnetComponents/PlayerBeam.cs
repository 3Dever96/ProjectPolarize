using UnityEngine;

public class PlayerBeam : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float range;

    public GameObject lastHit;
    public MagneticSource source;

    PlayerPolarity polarity;

    PlayerController player;

    Vector2 lastHoriztonalAngle = Vector2.right;
    Vector2 direction = new Vector2();

    void Start()
    {
        polarity = GetComponent<PlayerPolarity>();
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (InputManager.instance.Move != Vector2.zero)
        {
            if (Vector2.Angle(InputManager.instance.Move, Vector2.right) > 11.25f && Vector2.Angle(InputManager.instance.Move, Vector2.right) < 168.75f)
            {
                direction = new Vector2(0f, InputManager.instance.Move.y).normalized;
            }
            else
            {
                direction = new Vector2(InputManager.instance.Move.x, 0f).normalized;
                lastHoriztonalAngle = direction;
            }
        }
        else
        {
            direction = lastHoriztonalAngle;
        }

        if (InputManager.instance.Beam)
        {
            sprite.gameObject.SetActive(true);
            float distance = range;

            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 0.25f, direction, range, LayerMask.GetMask("Solid"));

            if (hit != false)
            {
                if (!hit.collider.isTrigger)
                {
                    if (lastHit != hit.collider.gameObject)
                    {
                        lastHit = hit.collider.gameObject;
                        source = lastHit.GetComponentInParent<MagneticSource>();
                    }

                    distance = hit.distance;
                }
            }
            else
            {
                lastHit = null;
                source = null;
            }

            sprite.size = new Vector2(distance, 1f);
            sprite.transform.right = direction;

            if (source != null)
            {
                int force = (polarity.polarity * source.polarity) * -1;

                player.MagneticForce = direction * force * source.strength * 5f;
            }
            else
            {
                player.MagneticForce = Vector2.zero;
            }
        }
        else
        {
            sprite.gameObject.SetActive(false);
            lastHit = null;
            source = null;

            player.MagneticForce = Vector2.zero;
        }
    }
}
