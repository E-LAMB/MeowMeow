using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPuzzleTile : MonoBehaviour
{

    public MatchPuzzlePlate my_plate;

    public bool is_revealed;

    public Transform self;

    public bool is_set;

    public Renderer my_renderer;

    public int my_symbol;

    public Material my_material;
    public Material hidden_material;

    public bool is_anomaly;


    public void BecomeSet(Material newmat, int my_id)
    {
        my_material = newmat;
        my_symbol = my_id; 
        is_set = true;
        is_revealed = false;
    }

    public void FlipMe()
    {
        if (!is_revealed && is_set && Mind.can_interact && my_plate.allow_interaction)
        {
            is_revealed = true;

            my_plate.ItFlipped(gameObject);
        }
    }

    public void FlipDown()
    {
        is_revealed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_set)
        {
            /*
            if (is_anomaly && my_plate.needed_to_complete == my_plate.amount_solved)
            {
                is_revealed = true;
            }
            */

            if (is_revealed) { my_renderer.material = my_material; } else { my_renderer.material = hidden_material; }
        }
    }
}
