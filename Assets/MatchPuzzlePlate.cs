
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPuzzlePlate : MonoBehaviour
{

    public int amount_to_set;
    public int amount_been_set;
    public int anomalies_to_make;
    public int amount_solved;
    public int needed_to_complete;

    public GameObject first_revealed;
    public GameObject second_revealed;

    public bool on_first;

    public GameObject[] my_list;

    public bool[] preassigned_symbols;
    public Material[] my_symbol_materials;

    public bool allow_interaction;

    public bool disable_future;
    public float disable_future_timer;

    public GameObject reward;

    public int to_give = 0;

    public void ItFlipped(GameObject the_shape)
    {
        if (on_first) { first_revealed = the_shape; } else { second_revealed = the_shape; }

        if (!on_first)
        {
            if( first_revealed.GetComponent<MatchPuzzleTile>().my_symbol == second_revealed.GetComponent<MatchPuzzleTile>().my_symbol )
            {
                // good
                amount_solved += 1;
            } else
            {
                Mind.can_interact = false;
                disable_future = true;
                disable_future_timer = 1.5f;
            }
        }

        on_first = !on_first;

    }

    public void SetupGame()
    {
        on_first = true;

        amount_been_set = 0;
        int limit_break = 0;
        needed_to_complete = 0;

        GameObject button_1 = null;
        GameObject button_2 = null;

        while (limit_break < 200 && amount_been_set != amount_to_set)
        {
            limit_break += 1;

            button_1 = my_list[Random.Range(0, my_list.Length)];
            if (button_1.GetComponent<MatchPuzzleTile>().is_set)
            {
                button_1 = null;
            }

            button_2 = my_list[Random.Range(0, my_list.Length)];
            if (button_2.GetComponent<MatchPuzzleTile>().is_set || button_1 == button_2)
            {
                button_2 = null;
            }

            if (button_1 != null && button_2 != null)
            {
                /*
                int to_give = 0;
                while (to_give == 0 || preassigned_symbols[to_give] == true)
                {
                    to_give = Random.Range(1, preassigned_symbols.Length);
                }
                */

                to_give += 1;

                preassigned_symbols[to_give] = true;
                amount_been_set += 1;
                needed_to_complete += 1;
                button_1.GetComponent<MatchPuzzleTile>().BecomeSet(my_symbol_materials[to_give], to_give);
                button_2.GetComponent<MatchPuzzleTile>().BecomeSet(my_symbol_materials[to_give], to_give);

                button_1 = null;
                button_2 = null;
            }

        }

        amount_been_set = 0;
        limit_break = 0;

        button_1 = null;

        while (limit_break < 200 && amount_been_set != anomalies_to_make)
        {
            limit_break += 1;

            button_1 = my_list[Random.Range(0, my_list.Length)];
            if (button_1.GetComponent<MatchPuzzleTile>().is_set)
            {
                button_1 = null;
            }

            if (button_1 != null)
            {

                to_give += 1;
                preassigned_symbols[to_give] = true;
                amount_been_set += 1;
                button_1.GetComponent<MatchPuzzleTile>().BecomeSet(my_symbol_materials[to_give], to_give); 
                button_1.GetComponent<MatchPuzzleTile>().is_anomaly = true;
                button_1 = null;
            }

        }

        on_first = true;
        Mind.can_interact = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupGame();
    }

    // Update is called once per frame
    void Update()
    {

        if (amount_solved == needed_to_complete)
        {
            reward.SetActive(true);
        }

        if (disable_future)
        {
            disable_future_timer -= Time.deltaTime;
            if (disable_future_timer < 0f)
            {
                disable_future = false;
                Mind.can_interact = true;
                first_revealed.GetComponent<MatchPuzzleTile>().FlipDown();
                second_revealed.GetComponent<MatchPuzzleTile>().FlipDown();
            }
        }
    }
}
