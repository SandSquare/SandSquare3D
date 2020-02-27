using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        //if (GameUIManager.Instance.menuOn)
        //{
        //    controls.Player.Menu.started += OnMenu;
        //}
    }

    public void OnMove(InputAction.CallbackContext context)
    {
    }

    public void OnMove(InputValue value)
    {
    }


    public void OnJump(InputAction.CallbackContext context)
    {
    }


    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.HandlePickUp();
        }
    }

    public void OnJab(InputAction.CallbackContext context)
    {
        player.HandleJab();
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        GameUIManager.Instance.ToggleWinPanel();
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
