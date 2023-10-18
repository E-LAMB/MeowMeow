using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

// This script works with the NoVR component to run different Functions() when interacted with, Similar to XR's Simple Interactable.
public class NoVrInt : MonoBehaviour
{

    public UnityEvent myevent;
    // The event the interactable will run

    XRSimpleInteractable interactable;
    // OPTIONAL: The XR interactable that is on the same component.
    // This is used for disabling the NoVR whenever the XR interactable is also disabled

    private void Start()
    {
        // Here we check if there is an XR Simple Interactable on the script to begin with
        if (gameObject.GetComponent<XRSimpleInteractable>())
        { interactable = gameObject.GetComponent<XRSimpleInteractable>(); } // And if we find one - we set it as our "interactable" variable.
    }

    // This is the actual function that runs the function 
    public void RunMyCode()
    {
        if (interactable == null) // If we have no XR Simple Interactable, Then we simply run the function
        {
            myevent.Invoke();

        } else
        {
            if (interactable.enabled) // If we do have an XR Simple Interactable, We only run it when it is enabled.
            {
                myevent.Invoke();

            } else
            {
                // And in case we wanted something else to happen like an error message - That'd fit here!
            }
        }
    }

}
