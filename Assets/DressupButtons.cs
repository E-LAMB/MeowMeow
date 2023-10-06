using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressupButtons : MonoBehaviour
{

    [Header("Assign manually")]
    public Material my_cosmetic;
    public string my_type;
    public int my_index;

    [Header("Assign in PREFAB / Automatically")]
    public NewDressup my_dress;
    public Renderer my_renderer;

    // Start is called before the first frame update
    void Start()
    {
        my_dress = GameObject.FindGameObjectWithTag("DressyWessy").GetComponent<NewDressup>();
        my_renderer.material = my_cosmetic;
    }

    // Update is called once per frame
    public void Used()
    {
        bool valid_use = true;

        if (my_type == "HEAD")
        {
            if (!my_dress.head_unlocked[my_index])
            {
                valid_use = false;
            }
        }
        if (my_type == "BODY")
        {
            if (!my_dress.body_unlocked[my_index])
            {
                valid_use = false;
            }
        }
        if (my_type == "LEGS")
        {
            if (!my_dress.leg_unlocked[my_index])
            {
                valid_use = false;
            }
        }

        if (valid_use)
        {
            my_dress.SwitchClothing(my_type, my_cosmetic);
        }
    }
}
