using Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

public class JoystickController : MonoBehaviour {

    public GameObject joystick;

    private VariableJoystick variableJoystick;

	private void Start () {
		Activate(false);
		variableJoystick = joystick.GetComponent<VariableJoystick>();
	}
	
	public void Activate(bool active)
    {
        joystick.SetActive(active);
    }
	
	public void ModeChanged(int index)
	{
		if (variableJoystick == null)
			return;
		switch(index)
		{
			case 0:
				variableJoystick.SetMode(JoystickType.Fixed);
				break;
			case 1:
				variableJoystick.SetMode(JoystickType.Floating);
				break;
			case 2:
				variableJoystick.SetMode(JoystickType.Dynamic);
				break;
			default:
				break;
		}     
	}
}
