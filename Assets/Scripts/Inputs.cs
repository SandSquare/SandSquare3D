using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Inputs : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls controls;
    private PlayerInput playerInput;
    private Player player;

    public Vector2 MoveInput
    {
        get;
        private set;
    }

    public bool JumpInput
    {
        get;
        private set;
    }

    public bool ActionInput
    {
        get;
        private set;
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.Player.SetCallbacks(this);
        player = GetComponent<Player>();

        //playerInput = GetComponent<PlayerInput>();
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
        MoveInput = context.ReadValue<Vector2>();
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpInput = context.performed;
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
