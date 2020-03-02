using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerEvents : MonoBehaviour
{
    private UnityAction pickupDelegate;
    private UnityAction damageDelegate;

    private void Awake()
    {
        pickupDelegate += OnPickUp;
        damageDelegate += OnDamage;
    }

    //Player starts listening events when enabled
    void OnEnable()
    {
        EventManager.StartListening("Pickup", pickupDelegate);
        EventManager.StartListening("Damage", damageDelegate);
    }

    //Player quits listening events when disabled
    void OnDisable()
    {
        EventManager.StopListening("Pickup", pickupDelegate);
        EventManager.StopListening("Damage", damageDelegate);
    }

    // Called when Pickup event is triggered (by any player)
    void OnPickUp()
    {
        Debug.Log("Object picked up.");
    }

    // Called when Damage event is triggered (by any player)
    public void OnDamage()
    {
        Debug.Log("Player took damage.");
        //TODO: Camera shake or something universal
    }

}
