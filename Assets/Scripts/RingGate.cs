using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGate : MonoBehaviour
{

    public Transform myself;

    public LayerMask player_layer;
    public bool player_was_close;

    public AgathaScript my_agatha;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        my_agatha = GameObject.FindGameObjectWithTag("Agatha").GetComponent<AgathaScript>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (!player_was_close && Physics.CheckSphere(myself.position, 50f, player_layer) && Mind.has_ring)
        {
            my_agatha.Alerted(player.transform.position, 2);
            player_was_close = true;
        }
        */

        if (Mind.has_ring && myself.position.y < 15f)
        {
            myself.position += Vector3.up * (Time.deltaTime / 2f);
        }
    }
}
