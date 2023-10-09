using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float yaw, pitch;
    private Rigidbody rb;
    public float speed, sensitivity;

    public bool should_be_in_control;

    public GameObject my_camera;

    public Transform self;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (should_be_in_control)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, -80, 50);
            yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
            my_camera.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
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
        }
        else
        {
            // nothing happens
        }
    }
}
