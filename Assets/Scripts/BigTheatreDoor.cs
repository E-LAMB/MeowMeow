using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTheatreDoor : MonoBehaviour
{

    public bool player_entered;
    public TheatreManager my_manager;

    public bool should_be_closed;

    public Transform door;
    public Transform closed_position;
    public Vector3 open_position;

    public float opening_speed;
    public float closing_speed;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player_entered = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        open_position = closed_position.position;
        open_position.y += 7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_entered && my_manager.current_stage == 0)
        {
            my_manager.current_stage = 1;
            should_be_closed = true;
        }

        if (!should_be_closed)
        {
            door.position = Vector3.MoveTowards(door.position, open_position, Time.deltaTime * opening_speed);
        } else
        {
            door.position = Vector3.MoveTowards(door.position, closed_position.position, Time.deltaTime * closing_speed);
        }

    }
}
