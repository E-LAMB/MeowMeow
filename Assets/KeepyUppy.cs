using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KeepyUppy : MonoBehaviour
{

    public Transform door_a;
    public Transform door_b;

    public float door_a_rotation;
    public float door_b_rotation;

    public bool currently_open;

    public float speed = 20f;

    public bool currently_playing;

    public float health_disc_a;
    public float health_disc_b;

    public float button_cooldown;

    public float difficulty = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameSetup()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (currently_playing)
        {

            height_disc_a



        }

        if (currently_open)
        {
            door_a_rotation -= Time.deltaTime * speed; if (door_a_rotation < -90f) { door_a_rotation = -90f; }
            door_b_rotation += Time.deltaTime * speed; if (door_b_rotation > 88f) { door_b_rotation = 88f; }

        } else
        {
            door_a_rotation += Time.deltaTime * speed; if (door_a_rotation > 0f) { door_a_rotation = 0f; }
            door_b_rotation -= Time.deltaTime * speed; if (door_b_rotation < -0f) { door_b_rotation = -0f; }
        }

        door_a.transform.eulerAngles = new Vector3(-180f, door_a_rotation, -90f);
        door_b.transform.eulerAngles = new Vector3(-180f, door_b_rotation, -90f);
    }
}
