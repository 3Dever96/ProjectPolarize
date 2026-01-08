using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    Transform target;

    [SerializeField] Vector3 minBounds;
    [SerializeField] Vector3 maxBounds;

    void Update()
    {
        if (target != null)
        {
            float x = Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x);
            float y = Mathf.Clamp(target.position.y, minBounds.y, maxBounds.y);

            transform.position = new Vector3(x, y, 0);
        }
        else
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            target = player != null ? player.transform : null;
        }
    }
}
