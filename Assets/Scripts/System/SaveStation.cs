using UnityEngine;

public class SaveStation : MonoBehaviour
{
    public SceneField scene;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                SaveManager.instance.ShowMenu(this);
            }
        }
    }
}
