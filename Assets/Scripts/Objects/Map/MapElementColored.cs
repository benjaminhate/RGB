using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapElementColored : MapElement {
    // Colorer, LevelStart, Obstacles
    private ColorElement.ColorType color;

    public MapElementColored(GameObject obj) : base(obj)
    {
        ColorElement color = obj.GetComponent<ColorElement>();
        if(color!=null)
            this.color = color.GetColor();
    }

    public MapElementColored() : base()
    {

    }

    public void ChangeColorType(ColorElement.ColorType colorType)
    {
        this.color = colorType;
    }

    public ColorElement.ColorType GetColor()
    {
        return this.color;
    }
}
