using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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

    public XRSimpleInteractable my_interactable;


    public void BecomeSet(Material newmat, int my_id)
    {
        my_material = newmat;
        my_symbol = my_id; 
        is_set = true;
        is_revealed = false;
    }

    public void FlipMe()
    {
        if (my_plate.my_progressor.match_state == 0)
        {
            if (!is_revealed && is_set && Mind.can_interact && my_plate.allow_interaction)
            {
                is_revealed = true;

                my_plate.ItFlipped(gameObject);
            }
        }
    }

    public void FlipDown()
    {
        is_revealed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        my_interactable = gameObject.GetComponent<XRSimpleInteractable>();
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

            my_interactable.enabled = !my_plate.currently_punishing;
            if (is_revealed) { my_renderer.material = my_material; my_interactable.enabled = false; } else { my_renderer.material = hidden_material; my_interactable.enabled = true;  }
            my_interactable.enabled = my_plate.my_progressor.match_state == 0;
        }

    }
}
