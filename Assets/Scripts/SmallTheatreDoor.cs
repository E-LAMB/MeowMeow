using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTheatreDoor : MonoBehaviour
{

    public bool is_open;

    public TheatreManager my_manager;

    public Transform door;
    public Transform closed_position;
    public Vector3 open_position;

    public int opening_stage;

    public float opening_speed;
    public float closing_speed;

    // Start is called before the first frame update
    void Start()
    {
        open_position = closed_position.position;
        open_position.y += 7f;
    }

    // Update is called once per frame
    void Update()
    {

        if (is_open)
        {
            door.position = Vector3.MoveTowards(door.position, open_position, Time.deltaTime * opening_speed);

        } else
        {
            door.position = Vector3.MoveTowards(door.position, closed_position.position, Time.deltaTime * closing_speed);
        }
    }
}
