using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform Hands;
    public GameObject tempParent;
    private bool isPickedUp;

    public void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        isPickedUp = false;
    }

    //public void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.M) && !isPickedUp)
    //    {
    //        this.transform.position = Hands.transform.position;
    //        this.transform.rotation = Hands.transform.rotation;
    //        this.transform.parent = tempParent.transform;
    //        isPickedUp = true;
    //    }

    //    if (Input.GetKeyDown(KeyCode.M) && isPickedUp == true)
    //    {
    //        this.transform.parent = null;
    //        GetComponent<Rigidbody>().useGravity = true;
    //        isPickedUp = false;
    //    }
    //}

    public void PickThrowable()
    {
        this.transform.position = Hands.transform.position;
        this.transform.rotation = Hands.transform.rotation;
        this.transform.parent = tempParent.transform;
        isPickedUp = true;
    }

    public void Throw()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        isPickedUp = false;
    }

}
