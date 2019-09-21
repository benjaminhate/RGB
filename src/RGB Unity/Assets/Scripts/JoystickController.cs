using UnityEngine;

public class JoystickController : MonoBehaviour {

    public GameObject joystick;

	private void Start () {
		Activate(false);
	}
	
	public void Activate(bool active)
    {
        joystick.SetActive(active);
    }
}
