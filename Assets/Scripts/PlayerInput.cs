using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls controls;
    private Player player;

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

    public InputAction ActionInput
    {
        get
        {
            return controls.Player.PickUp;
        }
    }

    private void Awake()
    {
        controls = new PlayerControls();        
        controls.Enable();
        controls.Player.SetCallbacks(this);
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        //controls.Player.PickUp.performed += OnPickUp;
    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.HandlePickUp();
        }
    }
}
