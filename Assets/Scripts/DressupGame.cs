using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressupGame : MonoBehaviour
{

    public int cosmetic_head_id;
    public int cosmetic_body_id;
    public int cosmetic_leg_id;

    public Material[] cosmetic_head_material;
    public Material[] cosmetic_body_material;
    public Material[] cosmetic_leg_material;

    public bool[] head_unlocked;
    public bool[] body_unlocked;
    public bool[] leg_unlocked;

    public Material current_head;
    public Material current_body;
    public Material current_leg;

    public Renderer head_renderer;
    public Renderer body_renderer;
    public Renderer leg_renderer;

    public void ButtonPressed(string which_one)
    {
        bool was_successful = false;
        int limit_break = 0;

        if (which_one == "HEAD/NEXT")
        {
            while (!was_successful && limit_break < 150)
            {
                limit_break += 1;
                cosmetic_head_id += 1;
                if (cosmetic_head_id == cosmetic_head_material.Length)
                {
                    cosmetic_head_id = 0;
                }
                if (head_unlocked[cosmetic_head_id])
                {
                    was_successful = true;
                }
            }
            current_head = cosmetic_head_material[cosmetic_head_id];
        }
        if (which_one == "HEAD/BACK")
        {
            while (!was_successful && limit_break < 150)
            {
                limit_break += 1;
                //Debug.Log("Start");
                //Debug.Log(cosmetic_head_id);
                cosmetic_head_id -= 1;
                //Debug.Log(cosmetic_head_id);
                if (cosmetic_head_id < 0)
                {
                    cosmetic_head_id = cosmetic_head_material.Length - 1;
                    //Debug.Log("Loop");
                    //Debug.Log(cosmetic_head_id);
                }
                if (head_unlocked[cosmetic_head_id])
                {
                    was_successful = true;
                }
            }
            current_head = cosmetic_head_material[cosmetic_head_id];
        }



        if (which_one == "BODY/NEXT")
        {
            while (!was_successful && limit_break < 150)
            {
                limit_break += 1;
                cosmetic_body_id += 1;
                if (cosmetic_body_id == cosmetic_body_material.Length)
                {
                    cosmetic_body_id = 0;
                }
                if (body_unlocked[cosmetic_body_id])
                {
                    was_successful = true;
                }
            }
            current_body = cosmetic_body_material[cosmetic_body_id];
        }
        if (which_one == "BODY/BACK")
        {
            while (!was_successful && limit_break < 150)
            {
                limit_break += 1;
                cosmetic_body_id -= 1;
                if (cosmetic_body_id < 0)
                {
                    cosmetic_body_id = cosmetic_body_material.Length - 1;
                }
                if (body_unlocked[cosmetic_body_id])
                {
                    was_successful = true;
                }
            }
            current_body = cosmetic_body_material[cosmetic_body_id];
        }



        if (which_one == "LEG/NEXT")
        {
            while (!was_successful && limit_break < 150)
            {
                limit_break += 1;
                cosmetic_leg_id += 1;
                if (cosmetic_leg_id == cosmetic_leg_material.Length)
                {
                    cosmetic_leg_id = 0;
                }
                if (leg_unlocked[cosmetic_leg_id])
                {
                    was_successful = true;
                }
            }
            current_leg = cosmetic_leg_material[cosmetic_leg_id];
        }
        if (which_one == "LEG/BACK")
        {
            while (!was_successful && limit_break < 150)
            {
                limit_break += 1;
                cosmetic_leg_id -= 1;
                if (cosmetic_leg_id < 0)
                {
                    cosmetic_leg_id = cosmetic_leg_material.Length - 1;
                }
                if (leg_unlocked[cosmetic_leg_id])
                {
                    was_successful = true;
                }
            }
            current_leg = cosmetic_leg_material[cosmetic_leg_id];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        head_renderer.material = current_head;
        body_renderer.material = current_body;
        leg_renderer.material = current_leg;
    }
}
