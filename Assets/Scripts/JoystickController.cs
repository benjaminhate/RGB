﻿using UnityEngine;

public class JoystickController : MonoBehaviour {

    public GameObject joystick;

	// Use this for initialization
	private void Start () {
        // joystick = GameObject.FindGameObjectWithTag("Joystick");
	}
	
	public void Activate(bool active)
    {
        joystick.SetActive(active);
    }
}
