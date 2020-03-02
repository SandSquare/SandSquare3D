using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputs : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerControls controls;
    // Start is called before the first frame update

    public Vector2 MoveInput
    {
        get
        {
            return controls.Player.Move.ReadValue<Vector2>();
        }
    }
    void Start()
    {
        controls = new PlayerControls();
        playerMovement = GetComponent<PlayerMovement>();   
    }

    private void OnMove(InputValue value)
    {
        if (!playerMovement) return;
    }

    private void OnJump(InputValue value)
    {
        if (!playerMovement) return;

        playerMovement.JumpInput(true);
    }
}
