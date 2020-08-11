using UnityEngine;

public class MainCameraController : MonoBehaviour {

	public GameObject player;
    private float _basePosZ = -10f;

    private void Start ()
    {
	    var position = player.transform.position;
	    transform.position = new Vector3(position.x, position.y, _basePosZ);
    }

    private void LateUpdate ()
    {
	    var position = player.transform.position;
	    transform.position = new Vector3(position.x, position.y, _basePosZ);
    }
}
