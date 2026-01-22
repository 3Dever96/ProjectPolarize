using UnityEngine;

public class PlayerStats : BaseStats
{
    [SerializeField] PlayerHealthbar healthbar;

    protected override void Start()
    {
        base.Start();

        healthbar.UpdateHealthbar(currentHp);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        healthbar.UpdateHealthbar(currentHp);
    }

    public override void RecoverDamage(int recover)
    {
        base.RecoverDamage(recover);

        healthbar.UpdateHealthbar(currentHp);
    }

    protected override void Die()
    {
        GameManager.instance.OnGameOver();

        base.Die();
    }
}
