using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomEdge : MonoBehaviour
{
    [SerializeField] List<SceneField> scenesToLoad;
    [SerializeField] List<SceneField> scenesToUnload;
    [SerializeField] bool alignY;
    [SerializeField] Vector2 offset;

    public void AlignPosition()
    {
        Vector3 pos = new Vector3();
        Transform player = FindFirstObjectByType<PlayerController>().transform;

        if (!alignY)
        {
            pos.x = transform.position.x + offset.x;
            pos.y = player.position.y;
            pos.z = player.position.z;
        }
        else
        {
            pos.x = player.position.x;
            pos.y = transform.position.y + offset.y;
            pos.z = player.position.z;
        }

        player.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            RoomManager.instance.LoadNewScenes(scenesToLoad, scenesToUnload, this);
        }
    }
}
