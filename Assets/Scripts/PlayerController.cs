 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public TextMeshProUGUI my_text;

    public float yaw, pitch;
    private Rigidbody rb;
    public float walkspeed, runspeed, dashspeed, sensitivity;

    public bool should_be_in_control;

    public GameObject my_camera;

    public Transform self;

    public bool is_running;

    float speed;

    public GameObject my_canvas;

    public Image dash;

    public bool charging_power;
    public bool using_power;
    public float power_time;

    public GameObject roof;

    public GameObject red_overlay;

    private void Start()
    {
        roof.SetActive(true);
        my_canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {

        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Mind.special_stun = 20f;
            Mind.special_reveal = 20f;
            Debug.Log("debugstun");
        }
        */

        my_text.text = Mind.remaining_shards.ToString();

        if (Mind.remaining_shards == 0)
        {
            my_text.color = new Vector4(0.6f, 0f, 0f, 1f);
            if (Mind.has_ring)
            {
                my_text.text = "GET TO THE PORTAL";

            } else
            {
                my_text.text = "PLEASE WAIT";
            }
        }

        is_running = true;

        if (is_running)
        {
            speed = runspeed;
        }
        else
        {
            speed = walkspeed;
        }

        if (charging_power)
        {
            power_time += Time.deltaTime / 2.5f;
            if (power_time > 6f)
            {
                power_time = 6f;
                charging_power = false;
            }
        }

        red_overlay.SetActive(using_power);

        dash.fillAmount = power_time / 6f;

        if (using_power)
        {
            power_time -= Time.deltaTime;
            speed = dashspeed;
            if (0f > power_time)
            {
                power_time = 0f;
                using_power = false;
                charging_power = true;
            }
        }

        if (!charging_power && !using_power && Input.GetKeyDown(KeyCode.LeftShift))
        {
            using_power = true;
        }

        if (should_be_in_control)
        {

            //Camera Control
            if (should_be_in_control)
            {
                Cursor.lockState = CursorLockMode.Locked;
                pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
                pitch = Mathf.Clamp(pitch, -80, 50);
                yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
                my_camera.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
            }  
        }
    }

    private void FixedUpdate()
    {
        if (should_be_in_control) // The condition that would stop you
        {
            Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * speed;
            Vector3 forward = new Vector3(-my_camera.transform.right.z, 0, my_camera.transform.right.x);
            Vector3 wishDirection = (forward * axis.x + my_camera.transform.right * axis.y + Vector3.up * rb.velocity.y);
            rb.velocity = wishDirection;
        } else
        {
            // nothing happens
        }
    }
}
