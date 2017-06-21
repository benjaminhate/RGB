using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorerController : MonoBehaviour {

	public enum Colors {RED,GREEN,BLUE};
	public Colors myColor;

	private Color red = new Color (1f, 0.2f, 0.2f, 1f);
	private Color green = new Color(0f,0.8f,0f,1f);
	private Color blue = new Color (0.1f, 0.3f, 1f, 1f);

	private SpriteRenderer renderer;

	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		switch (myColor) {
		case Colors.RED:
			renderer.color = red;
			break;
		case Colors.GREEN:
			renderer.color = green;
			break;
		case Colors.BLUE:
			renderer.color = blue;
			break;
		default:
			renderer.color = red;
			break;
		}
	}
}
