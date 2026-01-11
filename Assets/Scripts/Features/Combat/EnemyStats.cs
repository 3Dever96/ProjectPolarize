using UnityEngine;

public class EnemyStats : BaseStats
{
    void OnTriggerStay2D(Collider2D collision)
    {
        PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();

        if (player != null)
        {
            player.TakeDamage(atk);
        }
    }
}
