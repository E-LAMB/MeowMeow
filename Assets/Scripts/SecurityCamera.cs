using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{

    public GameObject swivel;
    public Camera my_cam;

    public bool is_visible;

    public GameObject target;

    public LayerMask wall_layer;

    public float player_distance;
    public Vector3 player_direction;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Agatha");    
    }

    // Update is called once per frame
    void Update()
    {

        player_distance = Vector3.Distance(swivel.transform.position, target.transform.position);
        player_direction = (swivel.transform.position - target.transform.position);
        player_direction = player_direction * -1f;

        is_visible = !Physics.Raycast(swivel.transform.position, player_direction, player_distance, wall_layer);

        if (is_visible && player_distance < 20f && Mind.remaining_shards != 0)
        {
            swivel.transform.LookAt(target.transform.position);
            my_cam.enabled = true;
        } else
        {
            my_cam.enabled = false;
        }

    }
}
