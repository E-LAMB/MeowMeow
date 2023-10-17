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
    public float health_disc_c;

    public float button_cooldown;

    public float difficulty = 1f;

    public string rising_disc;

    public Transform disc_a_trans;
    public Transform disc_b_trans;
    public Transform disc_c_trans;

    public Transform disc_marker_up;
    public Transform disc_marker_down;

    public float a_speed;
    public float b_speed;
    public float c_speed;

    public float cylinder_speed;

    public Material active_button;
    public Material inactive_button;

    public Renderer renderer_a;
    public Renderer renderer_b;
    public Renderer renderer_c;

    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        particles.SetActive(false);
    }

    public void GameBegin()
    {
        if (!currently_playing)
        {
            health_disc_a = 100f;
            health_disc_b = 100f;
            health_disc_c = 100f;
            a_speed = 0f;
            b_speed = 0f;
            c_speed = 0f;
            cylinder_speed = 6f + difficulty;
            currently_playing = true;
            rising_disc = "none";
        }
    }

    public void PushDisc(string disc_name)
    {
        if (currently_playing && button_cooldown < 0f && rising_disc != disc_name && ((disc_name == "A" && health_disc_a < 90f) || (disc_name == "B" && health_disc_b < 90f)) || (disc_name == "C" && health_disc_c < 90f))
        {
            rising_disc = disc_name;
            button_cooldown = 0.4f + (difficulty / 10f);
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

            if (rising_disc == "none") { particles.SetActive(false); }
            if (rising_disc == "A") { particles.SetActive(true); particles.transform.localPosition = new Vector3(1.9f, 2f , -2f); }
            if (rising_disc == "B") { particles.SetActive(true); particles.transform.localPosition = new Vector3(0f, 2f, -2f); }
            if (rising_disc == "C") { particles.SetActive(true); particles.transform.localPosition = new Vector3(-1.9f, 2f, -2f); }

            if (button_cooldown > 0f || rising_disc == "A" || health_disc_a > 90f) { renderer_a.material = inactive_button; } else { renderer_a.material = active_button; }
            if (button_cooldown > 0f || rising_disc == "B" || health_disc_b > 90f) { renderer_b.material = inactive_button; } else { renderer_b.material = active_button; }
            if (button_cooldown > 0f || rising_disc == "C" || health_disc_c > 90f) { renderer_c.material = inactive_button; } else { renderer_c.material = active_button; }

            if (button_cooldown > -1f) { button_cooldown -= Time.deltaTime; }

            if (cylinder_speed > a_speed) { a_speed += Time.deltaTime * difficulty * 2f; }
            if (cylinder_speed > b_speed) { b_speed += Time.deltaTime * difficulty * 2f; }
            if (cylinder_speed > c_speed) { c_speed += Time.deltaTime * difficulty * 2f; }

            health_disc_a -= a_speed * Time.deltaTime;
            health_disc_b -= b_speed * Time.deltaTime;
            health_disc_c -= c_speed * Time.deltaTime;

            if (rising_disc == "A") { a_speed -= Time.deltaTime * 18f; if (health_disc_a > 90f) { rising_disc = "none"; } }

            if (rising_disc == "B") { b_speed -= Time.deltaTime * 18f; if (health_disc_b > 90f) { rising_disc = "none"; } }

            if (rising_disc == "C") { c_speed -= Time.deltaTime * 18f; if (health_disc_c > 90f) { rising_disc = "none"; } }

            disc_a_trans.localPosition = new Vector3(1.9f, CalculateHeight(health_disc_a), -1.9f);
            disc_b_trans.localPosition = new Vector3(0f, CalculateHeight(health_disc_b), -1.9f);
            disc_c_trans.localPosition = new Vector3(-1.9f, CalculateHeight(health_disc_c), -1.9f);

            if (0f > health_disc_a) { Failed(); }
            if (0f > health_disc_b) { Failed(); }
            if (0f > health_disc_c) { Failed(); }

            if (100f < health_disc_a) { health_disc_a = 100f; if (a_speed < 0f) { a_speed = 0f; } }
            if (100f < health_disc_b) { health_disc_b = 100f; if (b_speed < 0f) { b_speed = 0f; } }
            if (100f < health_disc_c) { health_disc_c = 100f; if (c_speed < 0f) { c_speed = 0f; } }

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
