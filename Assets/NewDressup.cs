using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDressup : MonoBehaviour
{

    public bool[] head_unlocked;
    public bool[] body_unlocked;
    public bool[] leg_unlocked;

    public Material current_head;
    public Material current_body;
    public Material current_leg;

    public Renderer head_renderer;
    public Renderer body_renderer;
    public Renderer leg_renderer;

    public GameObject[] clothing_group;
    public int current_group;

    public void SwitchGroups(int change)
    {
        clothing_group[current_group].SetActive(false);
        current_group += change;
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

    }

    // Update is called once per frame
    void Update()
    {
        head_renderer.material = current_head;
        body_renderer.material = current_body;
        leg_renderer.material = current_leg;
    }
}
