using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingPosition : MonoBehaviour
{

    public AgathaScript my_agatha;
    public bool is_safe;

    public LayerMask floor_layer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, 8f, floor_layer))
        {
            is_safe = true;
            my_agatha.can_teleport_infront = is_safe;
            my_agatha.looking_location = transform.position;
            my_agatha.looking_location.y = my_agatha.self_transform.position.y;
        } else
        {
            is_safe = false;
        }
    }
}
