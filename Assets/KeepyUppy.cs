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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currently_open)
        {
            door_a_rotation -= Time.deltaTime * speed; if (door_a_rotation < 0f) { door_a_rotation = 0f; }

        } else
        {
            door_a_rotation += Time.deltaTime * speed; if (door_a_rotation > 90f) { door_a_rotation = 90f; }
        }

        door_a.transform.eulerAngles = new Vector3(-0f, door_a_rotation, -90f);
    }
}
