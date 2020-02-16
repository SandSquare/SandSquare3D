using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //public List<GameObject> currentHitObjects;
    public GameObject currentHitObject;
    private GameObject pickUpPlayer;

    public bool isPickedUp;
    public bool isColliding;
    public bool isThrowed;

    public bool boxHitsPlayer;
    public bool boxHitsGround;

    //public bool isGrounded = true;
    private float sphereRadius = 0.7f;
    public LayerMask layerMask;
    public LayerMask groundLayer;

    private Vector3 origin;

    private float currentHitDistance;

    //Damage inflicted to player
    [SerializeField]
    private int damage = 1;

    public void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        isPickedUp = false;
    }

    void Update()
    {
        origin = transform.position;

        Collider[] hits = Physics.OverlapSphere(origin, sphereRadius, layerMask, QueryTriggerInteraction.UseGlobal);


        // TODO: osuu heittäessä aina ensin heittävään pelaajaan, ei kuulu osua!
        foreach (var hit in hits)
        {
            //print(hit.transform.gameObject.tag);

            // If box is throwed and hits another player
            if (hit.transform.gameObject.layer == 10 && isThrowed == true && hit.transform.gameObject != pickUpPlayer
                && hit.transform.gameObject != pickUpPlayer.transform.GetChild(0).gameObject
                && hit.transform.gameObject != pickUpPlayer.transform.GetChild(1).gameObject
                && hit.transform.gameObject != pickUpPlayer.transform.GetChild(2).gameObject
                && hit.transform.gameObject != pickUpPlayer.transform.GetChild(2).GetChild(0).gameObject) // Layer 10 = player
            {
                boxHitsPlayer = true;
                isThrowed = false;
                OnBoxHitsPlayer();
                //print("BOX HITS PLAYER");
                Debug.Log($"BOX HITS {hit.transform.gameObject}");
                //Player handles the damage it takes
                hit.transform.gameObject.GetComponent<Player>().TakeDamage(damage);
                break;
                
            }
            // If box is throwed and hits ground
            else if (hit.transform.gameObject.layer == 8 && isThrowed==true) // Layer 10 = ground
            {
                boxHitsGround = true;
                isThrowed = false;
                OnBoxHitsGround();
                print("BOX HITS GROUND");
                break;
            }

        }
    }

    private void OnBoxHitsPlayer()
    {
        gameObject.SetActive(false);
        Destroy(this);
    }

    private void OnBoxHitsGround()
    {
        // Freezes box when it hits the ground
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        gameObject.SetActive(false);
        Destroy(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Debug.DrawLine(origin, origin * currentHitDistance);
        //Gizmos.DrawWireSphere(origin * currentHitDistance, sphereRadius);
    }

    public void ParentPlayer(GameObject player)
    {
        pickUpPlayer = player;
    }

}
