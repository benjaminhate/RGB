using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorController : MonoBehaviour {

	public enum Colors {RED,GREEN,BLUE};
	public Colors myColor;

	private Color red = new Color (1f, 0.2f, 0.2f, 1f);
	private Color green = new Color(0f,0.8f,0f,1f);
	private Color blue = new Color (0.1f, 0.3f, 1f, 1f);

	private SpriteRenderer spriteRenderer;

	public float speed;
	public float timeStop;

	private int dir=1;
	private Vector3 initialScale;
	private bool stop=false;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		switch (myColor) {
		case Colors.RED:
			spriteRenderer.color = red;
			break;
		case Colors.GREEN:
			spriteRenderer.color = green;
			break;
		case Colors.BLUE:
			spriteRenderer.color = blue;
			break;
		default:
			spriteRenderer.color = red;
			break;
		}
		if (speed > 0) {
			initialScale = transform.localScale;
		}
	}

	void Update(){
		if (dir == 1) {
			if (transform.localScale.x >= initialScale.x && transform.localScale.y >= initialScale.y && transform.localScale.z >= initialScale.z) {
				dir = -1;
				StartCoroutine (Wait ());
			}
		} else {
			if (transform.localScale.x <= initialScale.x * 0.01f && transform.localScale.y <= initialScale.y * 0.01f && transform.localScale.z <= initialScale.z * 0.01f) {
				dir = 1;
				StartCoroutine (Wait ());
			}
		}
		if (!stop) {
			float add = dir * Time.deltaTime * speed;
			transform.localScale = new Vector3 (transform.localScale.x + add,transform.localScale.y + add,transform.localScale.z + add);
		}
	}

	IEnumerator Wait(){
		stop = true;
		yield return new WaitForSeconds (timeStop);
		stop = false;
	}
}
