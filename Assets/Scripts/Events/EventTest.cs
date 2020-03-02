using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTest : MonoBehaviour
{

    private UnityAction pickupDelegate;

    void Awake()
    {
        pickupDelegate += PickUpMessage;
    }

    void OnEnable()
    {
        EventManager.StartListening("Pickup", pickupDelegate);
    }

    void OnDisable()
    {
        EventManager.StopListening("Pickup", pickupDelegate);
    }

    void PickUpMessage()
    {
        Debug.Log("Object picked up!");
    }

}
