using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LookMani : MonoBehaviour
{
    public bool is_right;
    public LookPuzzle my_manager;

    public Renderer head;
    public Renderer body;
    public Renderer legs;

    public bool should_be_rise;
    public GameObject my_body;

    public int setup_id;
    public XRSimpleInteractable interactable;

    public void Interacted()
    {
        if (my_manager.is_active)
        {
            if (is_right)
            {
                my_manager.Completed();

            }
            else
            {
                my_manager.Failed();
                gameObject.SetActive(false);
            }
        }
    }

    public void Setup(bool is_correct, Material my_head_material, Material my_body_material, Material my_legs_material, bool should_rise)
    {
        setup_id = my_manager.setup_id;
        should_be_rise = should_rise || is_correct;
        is_right = is_correct;
        head.material = my_head_material;
        body.material = my_body_material;
        legs.material = my_legs_material;
        if (is_right)
        {
            gameObject.name = "CORRECT";
        } else
        {
            gameObject.name = "FALSE";
        }
        //gameObject.transform.position = gameObject.transform.position + new Vector3(0f, 1f, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        interactable.enabled = should_be_rise;
        my_body.SetActive(should_be_rise);
    }
}
