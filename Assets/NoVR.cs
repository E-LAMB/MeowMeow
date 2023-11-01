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
    public float sensitivity = 3; // I find "3" to be a good number for sensitivity. You can choose though!

    public bool run_without_headset; // THIS IS VERY IMPORTANT. 

    bool is_moving_cam; // This bool shows if the player is moving the camera or not. It's used to prevent accidental interaction while looking around

    void Start()
    {

        // We only want to set everything up if we're running the game without a VR headset
        // That's why we'll use this variable to check if we are running the game without one or not.
        // If we run the game with a headset and this is wrong - WEIRD STUFF HAPPENS!
        if (run_without_headset) 
        {

            // So we first find the player's camera
            my_camera = GameObject.FindGameObjectWithTag("MainCamera");

            // And then we disable everything we don't want to use, For instance the VR controllers and such
            GameObject[] to_disable = GameObject.FindGameObjectsWithTag("HeadsetKit");
            for (int i = 0; i < to_disable.Length; i++)
            {
                to_disable[i].SetActive(false);
            }

            // Finally we'll take the camera's offset and set it to (0, 1, 0)
            // It can be anything you want, But this is the height our game is set to.
            GameObject.FindGameObjectWithTag("CameraOffset").transform.localPosition = Vector3.up;

        } else
        {
            Destroy(gameObject); 
            // Remember this script should only run if we aren't using the headset - So if we are we simply delete the gameobject housing the script.
            // We could disable it too, It's your choice!
        }
    }

    // Update is called once per frame, This is where the magic happens
    void Update()
    {

        if (Input.GetMouseButtonDown(1)) 
        {
            // This checks when the player presses down the movement button, 
            // So begins camera movement by locking the cursor and setting the moving variable to TRUE.
            Cursor.lockState = CursorLockMode.Locked;
            is_moving_cam = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            // This checks when the player releases the movement button, 
            // So ends the camera's movement allowing the cursor to move around the screen and setting the moving variable to FALSE.
            Cursor.lockState = CursorLockMode.Confined;
            is_moving_cam = false;
        }

        // So while we have the movement button held down, We modify the camera's rotation like this!
        // It's from a pretty standard player controller - But we don't move the camera's transform because we don't have WASD movement.
        if (Input.GetMouseButton(1))
        {
            pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, -90, 90);
            yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
        }
        my_camera.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);

        // This fires a Raycast from the Main Camera to wherever we click on the screen, And saves that data!
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        // We check for three conditions here when deciding to interact.
        // 1. Did we actually hit anything??
        // 2. Did the thing we hit have the interactable component on it??
        // 3. Are we not rotating the camera right now?
        if (Physics.Raycast(r, out hit) && hit.collider.gameObject.GetComponent<NoVrInt>() && !is_moving_cam)
        {
            // Transform objectHit = hit.transform; (We don't actually need to save this, But I did anyway!)

            Debug.DrawRay(r.origin, r.direction * 100, Color.green, 1f, true); // We make a debug ray for editor purposes

            if (Input.GetMouseButtonDown(0)) // If we press the interact buttons and the conditions are satisfied - This code runs
            {
                // We find the object we hit, Get the interactable component and run the code in the RunMyCode() function.
                hit.collider.gameObject.GetComponent<NoVrInt>().RunMyCode();
            }

        } else
        {
            Debug.DrawRay(r.origin, r.direction * 100, Color.red, 1f, true); // We make a debug ray for editor purposes
        }
    }
}
