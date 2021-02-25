﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM : MonoBehaviour
{
    const int IDLE = 1;
    const int PATROL = 2;
    const int CHASE = 3;

    int current_State;
    float current_time;
    float previous_time;

    public Transform[] points;
    private int destPoint = 0;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        current_State = CHASE;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (current_State)
        {
            case IDLE:
                idle_Do();
                if(timeToPatrol() == true)
                {
                    idle_Exit();
                    current_State = PATROL;
                    patrol_Entry();
                }
                else if(playerNear() == true)
                {
                    idle_Exit();
                    current_State = CHASE;
                    chase_Entry();
                }
                break;

            case PATROL:
                patrol_Do();
                if(playerNear() == true)
                {
                    patrol_Exit();
                    current_State = CHASE;
                    chase_Entry();
                }
                else if(timeToIdle() == true)
                {
                    patrol_Exit();
                    current_State = IDLE;
                    idle_Entry();
                }
                break;

            case CHASE:
                chase_Do();
                if(playerOutOfRange() == true)
                {
                    chase_Exit();
                    current_State = PATROL;
                    patrol_Entry();
                }
                break;
        }
    }

    void idle_Entry()
    {
        
    }
    
    void idle_Do()
    {
        idle();
        playerNear();
        timeToPatrol();
    }

    void idle_Exit()
    {

    }

    void patrol_Entry()
    {

    }

    void patrol_Do()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrol();
        }
        playerNear();
        timeToIdle();
    }

    void patrol_Exit()
    {

    }

    void chase_Entry()
    {

    }

    void chase_Do()
    {
        chase();
        playerOutOfRange();
    }

    void chase_Exit()
    {

    }

    private void idle()
    {

    }

    private void patrol()
    {
        if(points.Length == 0)
        {
            return;
        }
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    private void chase()
    {
        GameObject playerPosition = GameObject.Find("Player");
        agent.SetDestination(playerPosition.transform.position);
    }

    private bool playerNear()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            float distance = hit.distance;
            if (distance == 2)
            {
                return true;
            }
        }
        return false;
    }

    private bool timeToPatrol()
    {
        if(Time.time - previous_time >= 5)
        {
            previous_time = Time.time;
            return true;
        }
        return false;
    }

    private bool timeToIdle()
    {
        if (Time.time - previous_time >= 7)
        {
            previous_time = Time.time;
            return true;
        }
        return false;
    }

    private bool playerOutOfRange()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            float distance = hit.distance;
            if (distance > 2)
            {
                return true;
            }
        }
        return false;
    }
}
