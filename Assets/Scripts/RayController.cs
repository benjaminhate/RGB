using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour {

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
