using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField] float speed;

    [SerializeField] float lifeTime;
    int atk;

    public void Shoot(Vector2 direction, int strength)
    {
        body = GetComponent<Rigidbody2D>();

        body.linearVelocity = direction * speed;

        atk = strength;

        StartCoroutine(LifeTime());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.isTrigger)
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();

            if (player == null)
            {
                EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();

                if (enemy != null)
                {
                    enemy.TakeDamage(atk);
                }

                Destroy(gameObject);
            }
        }
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
