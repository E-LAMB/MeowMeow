using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDressup : MonoBehaviour
{
    public GameObject cosmeticsManagerObject;

    public bool[] head_unlocked;
    public bool[] body_unlocked;
    public bool[] legs_unlocked;

    public Material current_head;
    public Material current_body;
    public Material current_leg;

    public Renderer head_renderer;
    public Renderer body_renderer;
    public Renderer leg_renderer;

    public GameObject[] clothing_group;
    public Material[] clothing_group_icons;
    public int current_group;
    public Renderer button_UP;
    public Renderer button_DOWN;

    public bool has_not_read_cosmetics = true;

    public void SwitchGroups(int change)
    {
        clothing_group[current_group].SetActive(false);
        current_group += change;

        if (current_group > clothing_group.Length - 1)
        {
            current_group = 0;
        }
        if (current_group < 0)
        {
            current_group = clothing_group.Length - 1;
        }

        clothing_group[current_group].SetActive(true);

        int up_group = current_group + 1;
        int below_group = current_group - 1;

        if (up_group > clothing_group.Length - 1)
        {
            up_group = 0;
        }
        if (below_group < 0)
        {
            below_group = clothing_group.Length - 1;
        }

        button_UP.material = clothing_group_icons[below_group];
        button_DOWN.material = clothing_group_icons[up_group];

    }

    public void SwitchClothing(string type, Material new_material)
    {
        if (type == "HEAD")
        {
            current_head = new_material;
        }

        if (type == "BODY")
        {
            current_body = new_material;
        }

        if (type == "LEGS")
        {
            current_leg = new_material;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchGroups(0);
        {
            ReadCosmetics();
        }
    }

    public void ReadCosmetics()
    {
        string current_list = GameObject.FindGameObjectWithTag("CosMan").GetComponent<CosmeticsManager>().current_cosmetics;

        head_unlocked[0] = true;

        if (current_list.Contains("#CH_TEMP_DEF")) { head_unlocked[0] = true; }
        if (current_list.Contains("#CH_TEMP_PINK1")) { head_unlocked[1] = true; }
        if (current_list.Contains("#CH_TEMP_BLUE1")) { head_unlocked[2] = true; }

        if (current_list.Contains("#CB_TEMP_DEF")) { body_unlocked[0] = true; }
        if (current_list.Contains("#CB_TEMP_RED1")) { body_unlocked[1] = true; }
        if (current_list.Contains("#CB_TEMP_PURPLE1")) { body_unlocked[2] = true; }
        if (current_list.Contains("#CB_TEMP_BLUE1")) { body_unlocked[3] = true; }
        if (current_list.Contains("#CB_TEMP_LIME1")) { body_unlocked[4] = true; }
        if (current_list.Contains("#CB_TEMP_YELLOW1")) { body_unlocked[5] = true; }

        if (current_list.Contains("#CL_TEMP_DEF")) { legs_unlocked[0] = true; }
        if (current_list.Contains("#CL_TEMP_RED1")) { legs_unlocked[1] = true; }
        if (current_list.Contains("#CL_TEMP_LIME1")) { legs_unlocked[2] = true; }
        if (current_list.Contains("#CL_TEMP_BLUE1")) { legs_unlocked[3] = true; }

        has_not_read_cosmetics = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (has_not_read_cosmetics)
        {
            ReadCosmetics();
        }

        head_renderer.material = current_head;
        body_renderer.material = current_body;
        leg_renderer.material = current_leg;
    }
}