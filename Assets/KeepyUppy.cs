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

    public ParticleSystem particles_a;
    public ParticleSystem particles_b;
    public ParticleSystem particles_c;

    public float time_until_win;
    public float whole_time;
    public Transform time_expander;

    public bool completed;
    public float setup_time;

    // Start is called before the first frame update
    void Start()
    {
        particles_a.Stop();
        particles_b.Stop();
        particles_c.Stop();
    }

    public void GameBegin()
    {
        if (!currently_playing && !completed && currently_open)
        {
            health_disc_a = 100f;
            health_disc_b = 100f;
            health_disc_c = 100f;
            a_speed = Random.Range(0f, 2.5f);
            b_speed = Random.Range(0f, 2.5f);
            c_speed = Random.Range(0f, 2.5f);
            cylinder_speed = 7f + (difficulty * 2f);
            currently_playing = true;
            rising_disc = "none";
            particles_a.Stop();
            particles_b.Stop();
            particles_c.Stop();
            time_until_win = (difficulty * 5.5f);
            whole_time = time_until_win;
            time_expander.localScale = new Vector3(1f, 1f ,((whole_time - time_until_win) / whole_time) * 28f);
        }
    }

    public void PushDisc(string disc_name)
    {
        if (currently_playing && button_cooldown < 0f && rising_disc != disc_name && ((disc_name == "A" && health_disc_a < 90f) || (disc_name == "B" && health_disc_b < 90f)) || (disc_name == "C" && health_disc_c < 90f))
        {

            particles_a.Stop();
            particles_b.Stop();
            particles_c.Stop();

            if (disc_name == "A") {  particles_a.Play(); }
            if (disc_name == "B") {  particles_b.Play(); }
            if (disc_name == "C") {  particles_c.Play(); }

            rising_disc = disc_name;
            button_cooldown = 1.2f + (difficulty / 15f);
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
        particles_a.Stop();
        particles_b.Stop();
        particles_c.Stop();
        currently_open = false;
        setup_time = 0f;
    }

    public void Success()
    {
        currently_playing = false;
        particles_a.Stop();
        particles_b.Stop();
        particles_c.Stop();
        difficulty += 1;
        if (difficulty == 7)
        {
            completed = true;
        }
        currently_open = false;
        setup_time = 0f;
        Mind.total_solves += 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (currently_playing)
        {
            time_expander.localScale = new Vector3(1f, 1f, ((whole_time - time_until_win) / whole_time) * 28f);
            time_until_win -= Time.deltaTime;

            if (rising_disc == "none") 
            {
                particles_a.Stop();
                particles_b.Stop();
                particles_c.Stop();
            }

            if (button_cooldown > 0f || rising_disc == "A" || health_disc_a > 90f) { renderer_a.material = inactive_button; } else { renderer_a.material = active_button; }
            if (button_cooldown > 0f || rising_disc == "B" || health_disc_b > 90f) { renderer_b.material = inactive_button; } else { renderer_b.material = active_button; }
            if (button_cooldown > 0f || rising_disc == "C" || health_disc_c > 90f) { renderer_c.material = inactive_button; } else { renderer_c.material = active_button; }

            if (button_cooldown > -1f) { button_cooldown -= Time.deltaTime; }

            if (cylinder_speed > a_speed) { a_speed += Time.deltaTime * (difficulty); }
            if (cylinder_speed > b_speed) { b_speed += Time.deltaTime * (difficulty); }
            if (cylinder_speed > c_speed) { c_speed += Time.deltaTime * (difficulty); }

            health_disc_a -= a_speed * Time.deltaTime;
            health_disc_b -= b_speed * Time.deltaTime;
            health_disc_c -= c_speed * Time.deltaTime;

            if (rising_disc == "A") { a_speed -= Time.deltaTime * 35f; if (health_disc_a > 90f) { rising_disc = "none"; } }

            if (rising_disc == "B") { b_speed -= Time.deltaTime * 35f; if (health_disc_b > 90f) { rising_disc = "none"; } }

            if (rising_disc == "C") { c_speed -= Time.deltaTime * 35f; if (health_disc_c > 90f) { rising_disc = "none"; } }

            disc_a_trans.localPosition = new Vector3(1.9f, CalculateHeight(health_disc_a), -1.9f);
            disc_b_trans.localPosition = new Vector3(0f, CalculateHeight(health_disc_b), -1.9f);
            disc_c_trans.localPosition = new Vector3(-1.9f, CalculateHeight(health_disc_c), -1.9f);

            if (0f > health_disc_a) { Failed(); }
            if (0f > health_disc_b) { Failed(); }
            if (0f > health_disc_c) { Failed(); }

            if (time_until_win < 0f) { Success(); }

            if (100f < health_disc_a) { health_disc_a = 100f; if (a_speed < 0f) { a_speed = 0f; } }
            if (100f < health_disc_b) { health_disc_b = 100f; if (b_speed < 0f) { b_speed = 0f; } }
            if (100f < health_disc_c) { health_disc_c = 100f; if (c_speed < 0f) { c_speed = 0f; } }

        } else
        {
            if (!completed && door_a_rotation == 0f && door_b_rotation == 0f && !currently_open)
            {
                setup_time += Time.deltaTime;
                if (setup_time > 2f && Mind.total_solves > 7)
                {
                    currently_open = true;
                    health_disc_a = 100f;
                    health_disc_b = 100f;
                    health_disc_c = 100f;
                    disc_a_trans.localPosition = new Vector3(1.9f, CalculateHeight(health_disc_a), -1.9f);
                    disc_b_trans.localPosition = new Vector3(0f, CalculateHeight(health_disc_b), -1.9f);
                    disc_c_trans.localPosition = new Vector3(-1.9f, CalculateHeight(health_disc_c), -1.9f);
                }
            }
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
