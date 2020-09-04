using UnityEngine;

public class RayController : ObstacleController {

	public float speed;
	public float range;
	public int dir=1;

	private Vector3 _currentPos;

	private float _dist;

	private void Start () {
		_currentPos = transform.position;
		_dist = range * 0.1f;
	}

	private void Update(){
		if(!stop)
			transform.Translate (Time.deltaTime * speed * dir * Vector3.right);
		if (!(Mathf.Abs(transform.position.x - (_currentPos.x + range * dir)) <= _dist) &&
		    !(Mathf.Abs(transform.position.y - (_currentPos.y + range * dir)) <= _dist)) return;
		
		dir *= -1;
		_currentPos = transform.position;
		StartCoroutine (Wait ());
	}
}
