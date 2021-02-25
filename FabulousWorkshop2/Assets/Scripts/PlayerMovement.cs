using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float giveRadius;
    [SerializeField] GameObject hatPrefab;

    NavMeshAgent agent;
    bool hasHat;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);
            }
        }
        if (hasHat) 
        {
            HandleHatGift();
        }
    }

    void HandleHatGift() 
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, giveRadius);
        foreach (Collider collider in nearbyColliders) 
        {
            if (!collider.CompareTag("Guest")) 
            {
                continue;
            }
            Guest guest = collider.GetComponent<Guest>();
            if (guest.HasHat) 
            {
                continue;
            }
            guest.GiveHat(hatPrefab);
            hasHat = false;
        }
    }
}
