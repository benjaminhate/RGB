using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorerController : MonoBehaviour {

	private Color red = new Color (1f, 0.2f, 0.2f, 1f);
	private Color green = new Color(0f,0.8f,0f,1f);
	private Color blue = new Color (0.1f, 0.3f, 1f, 1f);

	private SpriteRenderer spriteRenderer;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		switch (GetComponent<ColorElement>().GetColor()) {
		case ColorElement.ColorType.RED:
			spriteRenderer.color = red;
			break;
		case ColorElement.ColorType.GREEN:
			spriteRenderer.color = green;
			break;
		case ColorElement.ColorType.BLUE:
			spriteRenderer.color = blue;
			break;
		default:
			spriteRenderer.color = red;
			break;
		}
	}
}
