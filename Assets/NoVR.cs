using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class NoVR : MonoBehaviour
{

    RaycastHit hit;
    public GameObject marker;
    public XRSimpleInteractable interactionManager;
    public SelectEnterEvent picked_event;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(r, out hit) && hit.collider.gameObject.GetComponent<XRSimpleInteractable>())
        {
            Transform objectHit = hit.transform;
            marker.transform.position = objectHit.position;
            Debug.DrawRay(r.origin, r.direction * 100, Color.green, 1f, true);

            if (Input.GetMouseButtonDown(0)) 
            {
                picked_event = hit.collider.gameObject.GetComponent<XRSimpleInteractable>().selectEntered;
                interactionManager = hit.collider.gameObject.GetComponent<XRSimpleInteractable>();

                interactionManager.Invoke(picked_event as UnityEvent, 0f);

                // interactionManager.Invoke(picked_event.ToString(), 0f) ;
            }

        } else
        {
            Debug.DrawRay(r.origin, r.direction * 100, Color.red, 1f, true);
        }
    }
}
