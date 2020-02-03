﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls controls;

    public bool isJumping = false;

    public Vector2 MoveInput
    {
        get
        {
            return controls.Player.Move.ReadValue<Vector2>();
        } 
    }

    public InputAction JumpInput
    {
        get
        {
            return controls.Player.Jump;
        }
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        controls.Enable();
    }

    public Vector2 GetInput()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        return moveInput;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isJumping = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isJumping = false;
        }
    }


}
