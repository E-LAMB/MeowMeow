using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheatreAgatha : MonoBehaviour
{

    public TheatreManager my_manager;

    public NavMeshAgent my_agent;
    public AgathaScript agatha_script;

    public bool movement_done;
    public bool activated_ai;

    public Transform player_destination;

    public TheatreAgatha myself;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (movement_done && !activated_ai)
        {
            my_agent.speed = 2f;
            my_agent.SetDestination(player_destination.position);
        }

        if (activated_ai)
        {
            agatha_script.enabled = true;
            myself.enabled = false;
        }
    }
}
