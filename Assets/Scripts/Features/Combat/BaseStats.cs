using System.Collections;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currentHp;
    [SerializeField] protected int atk;

    protected bool isInv;
    [SerializeField] protected float iFrames;
    [SerializeField] protected float blinkTime;
    protected SpriteRenderer sprite;

    protected virtual void Start()
    {
        currentHp = maxHp;
        isInv = false;

        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void RecoverDamage(int recover)
    {
        currentHp = Mathf.Clamp(currentHp + recover, 0, maxHp);
    }

    public virtual void TakeDamage(int damage)
    {
        if (!isInv)
        {
            currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);
            isInv = true;
            StartCoroutine(Invincible());
        }

        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

    protected IEnumerator Invincible()
    {
        float iFrame = 0;
        float blink = 0;
        int blinkScale = 1;
        
        while (iFrame < iFrames)
        {
            blink += blinkScale * 10f * Time.deltaTime;

            if (blink >= 1 || blink <= 0)
            {
                sprite.enabled = !sprite.enabled;
                blinkScale *= -1;
            }

            iFrame += Time.deltaTime;

            yield return null;
        }

        isInv = false;
        sprite.enabled = true;
    }
}
