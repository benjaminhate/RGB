using System;
using System.Collections;
using Objects;
using UnityEngine;

public class CameraController : ObstacleController {
	
	[Range(0,360)]public int degA;
	[Range(0,360)]public int degB;
	public int rotSpeed;
	public int dir; // = 1 || -1
	/* degA is the first angle and degB the second
	 * it will go from degA to degB clockwise
	 * then wait timeStop time
	 * then go from degB to degA counter-clockwise
	 * */

	private bool _directionA;

	private void Start () {
        _directionA = true;
		transform.rotation = Quaternion.Euler (0, 0, degA);
	}

	private void Update(){
		if (_directionA)
			Rotate (-dir, degA);
		else
			Rotate (dir, degB);
	}

	private void Rotate(int direction,int degrees){
		var target = new Vector3 (0, 0, degrees);
		if (!stop)
			transform.Rotate (Vector3.forward * direction, rotSpeed * Time.deltaTime);
		if (Vector3.Distance(transform.eulerAngles, target) < 1f) {
			_directionA = !_directionA;
			transform.eulerAngles = target;
			StartCoroutine(Wait ());
		}
	}
}
