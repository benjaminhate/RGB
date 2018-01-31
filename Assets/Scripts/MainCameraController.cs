using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

	public GameObject player;
    private float basePosZ = -10f;

	void Start () {
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, basePosZ);
	}

    void LateUpdate () {
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, basePosZ);
	}
}
