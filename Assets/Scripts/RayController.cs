using System.Collections;
using UnityEngine;

public class RayController : MonoBehaviour {

	public float speed;
	public float range;
	public float timeStop;
	private bool stop;
	public int dir=1;

	private Vector3 currentPos;

	private float dist;

	private void Start () {
		currentPos = transform.position;
		dist = range * 0.1f;
	}

	private void Update(){
		if(!stop)
			transform.Translate (Time.deltaTime * speed * dir * Vector3.right);
		if (!(Mathf.Abs(transform.position.x - (currentPos.x + range * dir)) <= dist) &&
		    !(Mathf.Abs(transform.position.y - (currentPos.y + range * dir)) <= dist)) return;
		
		dir *= -1;
		currentPos = transform.position;
		StartCoroutine (Wait ());
	}

	private IEnumerator Wait(){
		stop = true;
		yield return new WaitForSeconds (timeStop);
		stop = false;
	}
}
