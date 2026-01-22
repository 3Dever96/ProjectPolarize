using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Image shield1Image;
    [SerializeField] Image shield2Image;

    [Header("Healthbar values")]
    [SerializeField] Sprite[] shield1;
    [SerializeField] Sprite[] shield2;

    public void UpdateHealthbar(int value)
    {
        shield1Image.sprite = shield1[value];
        shield2Image.sprite = shield2[value];
    }
}
