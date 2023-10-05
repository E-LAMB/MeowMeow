using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportPad : MonoBehaviour
{

    public Transform player_rig;
    public bool teleported;

    public GameObject effects;
    public Renderer my_renderer;
    public XRSimpleInteractable interactor;

    public void TakeMe()
    {
        if (Vector3.Distance(player_rig.transform.position, gameObject.transform.position) > 6f)
        {
            player_rig.transform.position = new Vector3(gameObject.transform.position.x, player_rig.transform.position.y, gameObject.transform.position.z); 
            teleported = true;
            my_renderer.enabled = false;
            interactor.enabled = false;
            effects.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (teleported)
        {
            if (Vector3.Distance(player_rig.transform.position, gameObject.transform.position) > 6f)
            {
                teleported = false;
                my_renderer.enabled = true;
                interactor.enabled = true;
                effects.SetActive(true);
            }
        }
    }
}
