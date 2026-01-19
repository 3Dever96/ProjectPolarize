using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilePool : MonoBehaviour
{
    List<PlayerProjectile> list = new List<PlayerProjectile>();
    Queue<PlayerProjectile> projectiles = new Queue<PlayerProjectile>();

    void Awake()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            list.Add(transform.GetChild(i).GetComponent<PlayerProjectile>());
            projectiles.Enqueue(transform.GetChild(i).GetComponent<PlayerProjectile>());
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        RoomManager.instance.sceneDelegate += ResetAllProjectiles;
    }

    void OnDisable()
    {
        RoomManager.instance.sceneDelegate -= ResetAllProjectiles;
    }

    public PlayerProjectile SpawnProjectile()
    {
        PlayerProjectile projectile = projectiles.Dequeue();
        projectile.gameObject.SetActive(true);
        return projectile;
    }

    public void ResetProjectile(PlayerProjectile projectile)
    {
        projectiles.Enqueue(projectile);
        projectile.gameObject.SetActive(false);
    }

    void ResetAllProjectiles()
    {
        for(var i = 0; i < list.Count; i++)
        {
            projectiles.Enqueue(list[i]);
            list[i].gameObject.SetActive(false);
        }
    }
}
