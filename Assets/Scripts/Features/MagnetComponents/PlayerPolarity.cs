using UnityEngine;

public class PlayerPolarity : MonoBehaviour
{
    public int polarity;
    [SerializeField] Color[] colors;

    SpriteRenderer[] sprites;

    bool canChangePolarity;

    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();

        for (var i = 0; i < sprites.Length; i++)
        {
            Color color = colors[1];
            color.a = 1 - (i * 0.5f);
            sprites[i].color = color;
        }
    }

    void Update()
    {
        int value = InputManager.instance.Polarity;

        if (canChangePolarity)
        {
            if (value != 0)
            {
                SetPolarity(value);
            }
            canChangePolarity = false;
        }

        if (value == 0 && !canChangePolarity)
        {
            canChangePolarity = true;
        }
    }

    void SetPolarity(int value)
    {
        if (value != polarity)
        {
            polarity = value;
        }
        else
        {
            polarity = 0;
        }

        for (var i = 0; i < sprites.Length; i++)
        {
            Color color = colors[polarity + 1];
            color.a = 1 - (i * 0.5f);
            sprites[i].color = color;
        }
    }
}
