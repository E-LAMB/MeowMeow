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

    public string rising_disc;

    public Transform disc_a_trans;
    public Transform disc_b_trans;

    public Transform disc_marker_up;
    public Transform disc_marker_down;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameBegin()
    {
        if (!currently_playing)
        {
            health_disc_a = 100f;
            health_disc_b = 100f;
            currently_playing = true;
            rising_disc = "none";
        }
    }

    public void PushDisc(string disc_name)
    {
        if (currently_playing && rising_disc == "none")
        {
            rising_disc = disc_name;
        }
    }

    public float CalculateHeight(float dist)
    {
        float whole = disc_marker_up.transform.localPosition.y - disc_marker_down.transform.localPosition.y;
        return disc_marker_down.transform.localPosition.y + (whole * (dist/ 100f));
    }

    public void Failed()
    {
        currently_playing = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (currently_playing)
        {

            if (rising_disc != "A") { health_disc_a -= Random.Range(Time.deltaTime * ((difficulty * 5f) + 15f), Time.deltaTime * ((difficulty * 5f) + 15f * 1.5f)); }
            else { health_disc_a += Time.deltaTime * 20f; if (health_disc_a > 100f) { rising_disc = "none"; } }

            if (rising_disc != "B") { health_disc_b -= Random.Range(Time.deltaTime * ((difficulty * 5f) + 15f), Time.deltaTime * ((difficulty * 5f) + 15f * 1.5f)); }
            else { health_disc_b += Time.deltaTime * 20f; if (health_disc_b > 100f) { rising_disc = "none"; } }

            disc_a_trans.localPosition = new Vector3(-1.25f, CalculateHeight(health_disc_a), -1.9f);
            disc_b_trans.localPosition = new Vector3(1.25f, CalculateHeight(health_disc_b), -1.9f);

            if (0f > health_disc_a) { Failed(); }
            if (0f > health_disc_b) { Failed(); }

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
