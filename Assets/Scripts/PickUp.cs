using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool isPickedUp;
    public bool isColliding;


    public void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        isPickedUp = false;
    }

}
