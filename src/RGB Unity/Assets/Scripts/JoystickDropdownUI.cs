using System.Collections;
using System.Collections.Generic;
using Joystick_Pack.Scripts.Joysticks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoystickDropdownUI : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public TMP_Dropdown dropdown;
    
    private void Start()
    {
        if (variableJoystick == null)
            return;
        
        var joystickType = variableJoystick.Mode;
        dropdown.value = GetIndexFromJoystickType(joystickType);
    }

    private static int GetIndexFromJoystickType(JoystickType joystickType)
    {
        switch (joystickType)
        {
            case JoystickType.Fixed:
                return 0;
            case JoystickType.Floating:
                return 1;
            case JoystickType.Dynamic:
                return 2;
            default:
                return -1;
        }
    }
}
