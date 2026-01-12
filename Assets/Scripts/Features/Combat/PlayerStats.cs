using UnityEngine;

public class PlayerStats : BaseStats
{
    protected override void Die()
    {
        GameManager.instance.OnGameOver();

        base.Die();
    }
}
