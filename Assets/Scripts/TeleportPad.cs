 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportPad : MonoBehaviour
{

    public Transform player_rig;
    public bool teleported;

    public Collider my_collider;

    public GameObject effects;
    public Renderer my_renderer;
    public XRSimpleInteractable interactor;

    public ParticleSystem particle_effect;
    public bool effectState;

    public float vertical_height;

    public bool is_count_controlled;
    public TMPro.TextMeshPro screen_text;

    public void TakeMe()
    {
        if (!is_count_controlled)
        {
            if (Vector3.Distance(player_rig.transform.position, gameObject.transform.position) > 6f && Mind.able_to_teleport)
            {
                particle_effect.Play();
                player_rig.transform.position = new Vector3(gameObject.transform.position.x, vertical_height, gameObject.transform.position.z);
                teleported = true;
                //my_renderer.enabled = false;
                interactor.enabled = false;
                effectState = false;
            }

        } else
        {
            if (Vector3.Distance(player_rig.transform.position, gameObject.transform.position) > 6f && Mind.able_to_teleport && Mind.total_solves > 3)
            {
                particle_effect.Play();
                player_rig.transform.position = new Vector3(gameObject.transform.position.x, vertical_height, gameObject.transform.position.z);
                teleported = true;
                //my_renderer.enabled = false;
                interactor.enabled = false;
                effectState = false;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        player_rig = GameObject.FindGameObjectWithTag("XR_RIG").GetComponent<Transform>();
        particle_effect = GameObject.FindGameObjectWithTag("AppearEffect").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_count_controlled)
        {
            if (Mind.total_solves > 4)
            {
                screen_text.text = "ENTER";
            } else
            {
                screen_text.text = "COME BACK LATER";
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !teleported)
        {
            // TakeMe();
        }

        effects.SetActive(effectState && Mind.able_to_teleport);
        my_collider.enabled = (effectState && Mind.able_to_teleport);

        if (teleported)
        {
            if (Vector3.Distance(player_rig.transform.position, gameObject.transform.position) > 6f)
            {
                teleported = false;
                //my_renderer.enabled = true;
                interactor.enabled = true;
                effectState = true;
            }
        }
    }
}
