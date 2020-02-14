﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls controls;
    public PickUp pickUpObject;
    public bool isJumping = false;
    public bool isPickedUp = false;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
    }
    private void Update()
    {
        if (isPickedUp)
        {
            pickUpObject.PickThrowable();
        }
        else if (!isPickedUp)
        {
            pickUpObject.Throw();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
    }


    // Start is called before the first frame update
    void Start()
    {
        controls.Enable();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
    //    transform.position += new Vector3(moveInput.x, 0, moveInput.y);
    //    //GetComponent<Game.PlayerMovement>().GetInput(moveInput);
    //    //Mover.Move(moveInput);
    //}

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

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isPickedUp = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isPickedUp = false;
        }
    }
}