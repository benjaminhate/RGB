using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour {

	public enum Colors {RED,GREEN,BLUE};
	public Colors myColor;

	private Color red = new Color (1f, 0.2f, 0.2f, 1f);
	private Color green = new Color(0f,0.8f,0f,1f);
	private Color blue = new Color (0.1f, 0.3f, 1f, 1f);

	private SpriteRenderer renderer;

	public float speed;
	public float range;
	public float timeStop;
	private bool stop=false;
	public int dir=1;

	private Vector3 currentPos;

	private float dist;

	void Start () {
		currentPos = transform.position;
		dist = range * 0.1f;
		renderer = GetComponent<SpriteRenderer> ();
		switch (myColor){
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

	void Update(){
		if(!stop)
			transform.Translate (Vector3.right * speed * dir * Time.deltaTime);
		if (Mathf.Abs (transform.position.x - (currentPos.x + range * dir)) <= dist || Mathf.Abs (transform.position.y - (currentPos.y + range * dir)) <= dist) {
			dir *= -1;
			currentPos = transform.position;
			StartCoroutine (Wait ());
		}
	}

	IEnumerator Wait(){
		stop = true;
		yield return new WaitForSeconds (timeStop);
		stop = false;
	}
}
