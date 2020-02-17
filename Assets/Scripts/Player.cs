using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput inputs;
    private GameObject pickUpObject;
    private GameObject collidingObject;
    private Health health;
    public Transform Hands;
    public GameObject tempParent;
    public bool isColliding;
    public bool isJumping = false;
    //public bool isPickedUp;
    private bool handsEmpty = true;

    private float throwForce = 300f;

    private void Awake()
    {
        inputs = GetComponent<PlayerInput>();
        health = GetComponent<Health>();
    }

    //private void Update()
    //{
    //    if (inputs != null && inputs.ActionInput.triggered)
    //    {
    //        if (handsEmpty && isColliding)
    //        {
    //            PickThrowable();
    //            pickUpObject.GetComponent<PickUp>().isPickedUp = true;
    //            handsEmpty = false;
    //        }
    //        else if (pickUpObject != null && pickUpObject.GetComponent<PickUp>().isPickedUp)  
    //        {
    //            if (pickUpObject.GetComponent<PickUp>().isPickedUp && !handsEmpty)
    //            {
    //                Throw();
    //                pickUpObject.GetComponent<PickUp>().isPickedUp = false;
    //                handsEmpty = true;
    //            }
                
    //        }
    //        //Debug.Log($"handsempty {handsEmpty} - ispickedup {pickUpObject.GetComponent<PickUp>().isPickedUp}");
    //    }
    //}

    public void HandlePickUp()
    {
        if (handsEmpty && isColliding)
        {
            PickThrowable();
            pickUpObject.GetComponent<PickUp>().isPickedUp = true;
            handsEmpty = false;
        }
        else if (pickUpObject != null && pickUpObject.GetComponent<PickUp>().isPickedUp)
        {
            if (pickUpObject.GetComponent<PickUp>().isPickedUp && !handsEmpty)
            {
                Throw();
                pickUpObject.GetComponent<PickUp>().isPickedUp = false;
                handsEmpty = true;
            }
        }
    }

    //public void HandlePickUp()
    //{
    //    if (handsEmpty && isColliding)
    //    {
    //        PickThrowable();
    //        pickUpObject.GetComponent<PickUp>().isPickedUp = true;
    //        handsEmpty = false;
    //    }
    //    else if (pickUpObject.GetComponent<PickUp>().isPickedUp && !handsEmpty)
    //    {
    //        Throw();
    //        pickUpObject.GetComponent<PickUp>().isPickedUp = false;
    //        handsEmpty = true;
    //    }

    //    Debug.Log($"handsempty {handsEmpty} - ispickedup {pickUpObject.GetComponent<PickUp>().isPickedUp}");
    //}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            isColliding = true;
            collidingObject = other.gameObject;
        }
    }

    //Reference to collidingObject is removed when player exits it's collider
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            isColliding = false;
            collidingObject = null;
        }
    }

    //The damage player takes is handled
    public void TakeDamage(int amount)
    {
        EventManager.TriggerEvent("Damage");
        health.DecreaseHealth(amount);
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
    //}

    public void PickThrowable()
    {
        if (isColliding)
        {
            pickUpObject = collidingObject;
            pickUpObject.GetComponent<PickUp>().ParentPlayer(this.gameObject);
            pickUpObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            pickUpObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pickUpObject.transform.position = Hands.transform.position;
            pickUpObject.transform.rotation = Hands.transform.rotation;
            pickUpObject.transform.parent = tempParent.transform;
            pickUpObject.GetComponent<PickUp>().isPickedUp = true;
            isColliding = false;
            //EventManager.TriggerEvent("Pickup");
        }
    }

    public void Throw()
    {
        if (pickUpObject != null)
        {
            pickUpObject.transform.parent = null;
            pickUpObject.GetComponent<Rigidbody>().useGravity = true;
            pickUpObject.GetComponent<PickUp>().isPickedUp = false;
            pickUpObject.GetComponent<PickUp>().isThrowed = true;
            isColliding = false;
            pickUpObject.GetComponent<Rigidbody>().AddForce(Hands.forward * throwForce);
            pickUpObject.GetComponent<Rigidbody>().AddForce(Hands.up * (throwForce / 2));
        }
    }

    public void RemoveChild()
    {
        pickUpObject = null;
    }
}
