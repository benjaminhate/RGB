using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	[Range(0,360)]public int degA;
	[Range(0,360)]public int degB;
	public int rotSpeed;
	public float timeStop;
	public int way; // = 1 || -1
	/* degA is the first angle and degB the second
	 * it will go from degA to degB clockwise
	 * then wait timeStop time
	 * then go from degB to degA counter-clockwise
	 * */

	public enum Colors {RED,GREEN,BLUE};
	public Colors myColor;

	private Color red = new Color (1f, 0.2f, 0.2f, 1f);
	private Color green = new Color(0f,0.8f,0f,1f);
	private Color blue = new Color (0.1f, 0.3f, 1f, 1f);

	private bool directionA;
	private bool stop = false;

	private SpriteRenderer spriteRenderer;

	void Start () {
        directionA = true;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		transform.rotation = Quaternion.Euler (0, 0, degA);
		switch (myColor){
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
	}

	void Update(){
		if (directionA)
			Rotate (-way, degA);
		else
			Rotate (way, degB);
	}

	void Rotate(int dir,int deg){
		Vector3 target = new Vector3 (0, 0, deg);
		if (!stop)
			transform.Rotate (Vector3.forward * dir, rotSpeed * Time.deltaTime);
		if (Vector3.Distance(transform.eulerAngles, target) < 1f) {
			directionA = !directionA;
			transform.eulerAngles = target;
			StartCoroutine(Wait ());
		}
	}

	IEnumerator Wait(){
		stop = true;
		yield return new WaitForSeconds (timeStop);
		stop = false;
	}
}
