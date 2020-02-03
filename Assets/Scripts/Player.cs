using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls controls;
    public PickUp pickUpObject;
    public Transform Hands;
    public GameObject tempParent;
    public bool isColliding;
    public bool isJumping = false;
    public bool isPickedUp = false;
    private bool handsEmpty = true;

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
    private void Update()
    {
        if (isPickedUp)
        {
            PickThrowable();
        }
        else if (!isPickedUp)
        {
            Throw();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Throwable")
        {
            isColliding = true;
        }

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
        if (context.phase == InputActionPhase.Performed && handsEmpty)
        {
            isPickedUp = true;
            handsEmpty = false;
        }
        else if (context.phase == InputActionPhase.Performed && !handsEmpty)
        {
            isPickedUp = false;
            handsEmpty = true;
        }

    }

    public void PickThrowable()
    {
        if (isColliding)
        {
            pickUpObject.transform.position = Hands.transform.position;
            pickUpObject.transform.rotation = Hands.transform.rotation;
            pickUpObject.transform.parent = tempParent.transform;
            isPickedUp = true;
            isColliding = false;
        }
    }

    public void Throw()
    {
        pickUpObject.transform.parent = null;
        pickUpObject.GetComponent<Rigidbody>().useGravity = true;
        isPickedUp = false;
    }
}
