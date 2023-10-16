using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class NoVrInt : MonoBehaviour
{

    public UnityEvent myevent;

    XRSimpleInteractable interactable;

    private void Start()
    {
        if (gameObject.GetComponent<XRSimpleInteractable>())
        { interactable = gameObject.GetComponent<XRSimpleInteractable>(); }
    }

    public void RunMyCode()
    {
        if (interactable == null)
        {
            myevent.Invoke();

        } else
        {
            if (interactable.enabled)
            {
                myevent.Invoke();

            } else
            {
                // disabled
            }
        }
    }

}
