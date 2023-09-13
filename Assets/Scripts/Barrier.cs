using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (player.using_power)
            {
                GameObject.FindGameObjectWithTag("Agatha").GetComponent<AgathaScript>().Alerted(gameObject.transform.position, 1);
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

}
