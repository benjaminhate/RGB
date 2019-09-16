using UnityEngine;

public class MainCameraController : MonoBehaviour {

	public GameObject player;
    private float basePosZ = -10f;

    private void Start ()
    {
	    var position = player.transform.position;
	    transform.position = new Vector3(position.x, position.y, basePosZ);
    }

    private void LateUpdate ()
    {
	    var position = player.transform.position;
	    transform.position = new Vector3(position.x, position.y, basePosZ);
    }
}
