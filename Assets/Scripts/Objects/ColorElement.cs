using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorElement : MonoBehaviour{
    public enum ColorType { RED, GREEN, BLUE };
    public ColorType color;

    private Color red = new Color(1f, 0.2f, 0.2f, 1f);
    private Color green = new Color(0f, 0.8f, 0f, 1f);
    private Color blue = new Color(0.1f, 0.3f, 1f, 1f);

    public ColorElement(ColorType type)
    {
        this.color = type;
    }

    public ColorElement()
    {
        this.color = ColorType.RED;
    }

    public ColorType GetColor()
    {
        return color;
    }

    public bool SameColor(ColorElement color)
    {
        return (int)this.color == (int)color.color;
    }

    public void ChangeColor(ColorType colorType)
    {
        color = colorType;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            switch (colorType)
            {
                case ColorType.RED:
                    spriteRenderer.color = red;
                    break;
                case ColorType.GREEN:
                    spriteRenderer.color = green;
                    break;
                case ColorType.BLUE:
                    spriteRenderer.color = blue;
                    break;
                default:
                    spriteRenderer.color = red;
                    break;
            }
        }
    }

    void Start()
    {
        ChangeColor(color);   
    }
}
