using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject smallBullet;

    bool canShoot;

    Vector2 direction;
    Vector2 lastDirection;

    int currentAtk;

    PlayerProjectilePool pool;

    void Start()
    {
        lastDirection = Vector2.right;
        pool = FindFirstObjectByType<PlayerProjectilePool>();
    }

    void Update()
    {
        if (InputManager.instance.Move != Vector2.zero)
        {
            if (Vector2.Angle(InputManager.instance.Move, Vector2.right) > 22.5f && Vector2.Angle(InputManager.instance.Move, Vector2.right) < 157.5f)
            {
                direction = new Vector2(0f, InputManager.instance.Move.y).normalized;
            }
            else
            {
                direction = new Vector2(InputManager.instance.Move.x, 0f).normalized;
                lastDirection = direction;
            }
        }
        else
        {
            direction = lastDirection;
        }

        if (InputManager.instance.Attack && canShoot)
        {
            currentAtk = 1;
            PlayerProjectile bullet = pool.SpawnProjectile();
            bullet.Shoot(transform.position, direction, currentAtk);
            canShoot = false;
        }

        if (!InputManager.instance.Attack && !canShoot)
        {
            canShoot = true;
        }
    }
}
