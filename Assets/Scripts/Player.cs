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
    //private Health health;
    public Transform Hands;
    public GameObject tempParent;
    public bool isColliding;
    public bool isJumping = false;
    private bool handsEmpty = true;

    [SerializeField]
    private float throwForceX = 300f;
    [SerializeField]
    private float throwForceY = 150f;



    [SerializeField]
    private SphereCollider jabCollider;
    [SerializeField]
    private float jabTime = 2;
    private float time = 0;

    [SerializeField]
    private Types.PlayerState playerState;

    public IHealth Health
    {
        get;
        private set;
    }

    private void Awake()
    {
        inputs = GetComponent<PlayerInput>();
        //health = GetComponent<Health>();
        Health = GetComponent<IHealth>();
        if (Health == null)
        {
            Debug.LogError($"Health component could not be found from {this.name}");
        }
        jabCollider.gameObject.SetActive(false);
    }

    public void HandleJab()
    {
        Debug.Log("Jab handled");
        jabCollider.gameObject.SetActive(true);
        playerState = Types.PlayerState.Jab;
    }

    private void Update()
    {
        if (playerState == Types.PlayerState.Jab)
        {
            time += Time.deltaTime;
            if (time > jabTime)
            {
                time = 0;
                jabCollider.gameObject.SetActive(false);
                playerState = Types.PlayerState.Idle;
            }
        }

        if (Health.CurrentHealth <= Health.MinHealth)
        {
           Die();
        }
    }

    public void HandlePickUp()
    {
        Debug.Log("Pickup handled");
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
                handsEmpty = true;
            }
        }
        //Debug.Log($"handsempty {handsEmpty} - ispickedup {pickUpObject.GetComponent<PickUp>().isPickedUp}");
    }


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
        Health.DecreaseHealth(amount);
    }

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
            pickUpObject.GetComponent<Rigidbody>().AddForce(Hands.forward * throwForceX);
            pickUpObject.GetComponent<Rigidbody>().AddForce(Hands.up * (throwForceY));
            pickUpObject = null;
        }
    }

    public void RemoveChild()
    {
        pickUpObject = null;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
