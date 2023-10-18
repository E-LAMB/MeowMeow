using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

// This script works with the NoVRInt (Interactable) component to run different Functions() when interacted with, Similar to XR's Simple Interactable.
public class NoVR : MonoBehaviour
{

    RaycastHit hit; // The data from our raycast (More on that later)

    GameObject my_camera; // The main camera - The eyes the player will see out of

    // Different variables handling the camera movement - Such as the Yaw / Pitch as well as the sensitivity
    private float yaw, pitch;
    public float sensitivity = 3; // I find "3" to be a good number

    public bool run_without_headset; // THIS IS VERY IMPORTANT. 

    bool is_moving_cam; // This bool shows if the player is moving the camera or not. It's used to prevent accidental interaction while looking around

    // Start is called before the first frame update
    void Start()
    {
        my_camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (run_without_headset)
        {
            GameObject[] to_disable = GameObject.FindGameObjectsWithTag("HeadsetKit");
            for (int i = 0; i < to_disable.Length; i++)
            {
                to_disable[i].SetActive(false);
            }
            GameObject.FindGameObjectWithTag("CameraOffset").transform.localPosition = Vector3.up;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            is_moving_cam = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.Confined;
            is_moving_cam = false;
        }

        if (Input.GetMouseButton(1))
        {
            pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, -90, 90);
            yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
            // my_camera.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
        }
        my_camera.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(r, out hit) && hit.collider.gameObject.GetComponent<NoVrInt>() && !is_moving_cam)
        {
            Transform objectHit = hit.transform;
            // marker.transform.position = objectHit.position;
            Debug.DrawRay(r.origin, r.direction * 100, Color.green, 1f, true);

            if (Input.GetMouseButtonDown(0)) 
            {
                hit.collider.gameObject.GetComponent<NoVrInt>().RunMyCode();

                // interactionManager.Invoke(picked_event, 0f);

                // picked_event.Invoke();

                // ue.Invoke();
            }

        } else
        {
            Debug.DrawRay(r.origin, r.direction * 100, Color.red, 1f, true);
        }
    }
}
