using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput inputs;
    private GameObject pickUpObject;
    private GameObject collidingObject;
    public Transform Hands;
    public GameObject tempParent;
    public bool isColliding;
    public bool isJumping = false;
    public bool isPickedUp;
    private bool handsEmpty = true;

    private float throwForce = 300f;

    private void Awake()
    {
        inputs = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        if (inputs.ActionInput.triggered)
        {
            if (handsEmpty && isColliding)
            {
                PickThrowable();
                isPickedUp = true;
                handsEmpty = false;
            }
            else if (isPickedUp && !handsEmpty)
            {
                Throw();
                isPickedUp = false;
                handsEmpty = true;
            }

            Debug.Log($"handsempty {handsEmpty} - ispickedup {isPickedUp}");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            isColliding = true;
            collidingObject = other.gameObject;
        }    
    }

    //public void OnPickUp(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Performed && handsEmpty && isColliding)
    //    {
    //        handsEmpty = false;
    //    }
    //    else if (context.phase == InputActionPhase.Performed && !handsEmpty)
    //    {
    //        handsEmpty = true;
    //    }
    //}w

    public void PickThrowable()
    {
        if (isColliding)
        {
            pickUpObject = collidingObject;
            pickUpObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            pickUpObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pickUpObject.transform.position = Hands.transform.position;
            pickUpObject.transform.rotation = Hands.transform.rotation;
            pickUpObject.transform.parent = tempParent.transform;
            isPickedUp = true;
            isColliding = false;
        }
    }

    public void Throw()
    {
        if(pickUpObject != null)
        {
            pickUpObject.transform.parent = null;
            pickUpObject.GetComponent<Rigidbody>().useGravity = true;
            isPickedUp = false;
            isColliding = false;
            pickUpObject.GetComponent<Rigidbody>().AddForce(Hands.forward * throwForce);
            pickUpObject.GetComponent<Rigidbody>().AddForce(Hands.up * (throwForce / 2));
        }
    }
}
