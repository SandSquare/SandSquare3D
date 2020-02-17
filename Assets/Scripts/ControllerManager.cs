using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ControllerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gamepads = Gamepad.all;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
