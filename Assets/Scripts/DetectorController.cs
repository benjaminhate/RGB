using System.Collections;
using UnityEngine;

public class DetectorController : MonoBehaviour {

	public float speed;
	public float timeStop;

	private int dir=1;
	private Vector3 initialScale;
	private bool stop;

	private void Start () {
		if (speed > 0) {
			initialScale = transform.localScale;
		}
	}

	private void Update(){
        var t = transform;
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

		if (stop) return;
		
		var add = dir * Time.deltaTime * speed;
		var localScale = t.localScale;
		localScale = new Vector3 (localScale.x + add
			,localScale.y + add,localScale.z + add);
		t.localScale = localScale;
	}

	private IEnumerator Wait(){
		stop = true;
		yield return new WaitForSeconds (timeStop);
		stop = false;
	}
}
