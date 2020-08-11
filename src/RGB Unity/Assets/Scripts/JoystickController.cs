using Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

public class JoystickController : MonoBehaviour {

    public GameObject joystick;

    private VariableJoystick _variableJoystick;

	private void Start () {
		Activate(false);
		_variableJoystick = joystick.GetComponent<VariableJoystick>();
	}
	
	public void Activate(bool active)
    {
        joystick.SetActive(active);
    }
	
	public void ModeChanged(int index)
	{
		if (_variableJoystick == null)
			return;
		switch(index)
		{
			case 0:
				_variableJoystick.SetMode(JoystickType.Fixed);
				break;
			case 1:
				_variableJoystick.SetMode(JoystickType.Floating);
				break;
			case 2:
				_variableJoystick.SetMode(JoystickType.Dynamic);
				break;
			default:
				break;
		}     
	}
}
