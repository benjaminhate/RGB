using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorController : MonoBehaviour {

	public float speed;
	public float timeStop;

	private int dir=1;
	private Vector3 initialScale;
	private bool stop=false;

	void Start () {
		if (speed > 0) {
			initialScale = transform.localScale;
		}
	}

	void Update(){
        Transform t = transform;
		if (dir == 1) {
			if (t.localScale.x >= initialScale.x && t.localScale.y >= initialScale.y && t.localScale.z >= initialScale.z) {
				dir = -1;
				StartCoroutine (Wait ());
			}
		} else {
			if (t.localScale.x <= 0f && t.localScale.y <= 0f && t.localScale.z <= 0f) {
				dir = 1;
				StartCoroutine (Wait ());
			}
		}
		if (!stop) {
			float add = dir * Time.deltaTime * speed;
			t.localScale = new Vector3 (t.localScale.x + add
                ,t.localScale.y + add,t.localScale.z + add);
		}
	}

	IEnumerator Wait(){
		stop = true;
		yield return new WaitForSeconds (timeStop);
		stop = false;
	}
}
