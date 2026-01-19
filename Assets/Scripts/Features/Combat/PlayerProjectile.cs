using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField] float speed;

    [SerializeField] float lifeTime;
    int atk;

    PlayerProjectilePool pool;

    void Start()
    {
        pool = FindFirstObjectByType<PlayerProjectilePool>();
    }

    void OnEnable()
    {
        if (pool == null)
        {
            pool = FindFirstObjectByType<PlayerProjectilePool>();
        }

        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
    }

    public void Shoot(Vector3 spawnPoint, Vector2 direction, int strength)
    {
        transform.position = spawnPoint;

        if (body != null)
        {
            body.linearVelocity = direction * speed;
        }

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

                pool.ResetProjectile(this);
            }
        }
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        pool.ResetProjectile(this);
    }
}
