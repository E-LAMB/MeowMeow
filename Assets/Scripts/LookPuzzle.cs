using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class LookPuzzle : MonoBehaviour
{

    public GameObject[] all_mans;
    public GameObject prize;

    public Material head_chosen;
    public Material[] all_head_materials;

    public Material body_chosen;
    public Material[] all_body_materials;

    public Material leg_chosen;
    public Material[] all_leg_materials;

    public Renderer template_head;
    public Renderer template_body;
    public Renderer template_leg;

    public int lives;
    public bool is_active;
    public int amount_to_set;

    public int setup_id;

    public PuzzleProgressionShower progressor;

    public GameObject good_light;
    public GameObject bad_light;
    public GameObject normal_light;

    public void Completed()
    {
        // prize.SetActive(true);
        progressor.look_progress += 1;
        progressor.look_state = 1; 
        progressor.CompletedLook();
        is_active = false;

        good_light.SetActive(true);
        bad_light.SetActive(false);
        normal_light.SetActive(false);
    }

    public void Failed()
    {
        lives -= 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // SetUpGame();
    }

    public void SetUpGame(int progression)
    {

        for (int i = 0; i < all_mans.Length; i++)
        {
            all_mans[i].GetComponent<LookMani>().Reset();
        }

        lives = 1;
        setup_id += 1;

        if (progression == 1) { amount_to_set = 0; }
        if (progression == 2) { amount_to_set = 2; }
        if (progression == 3) { amount_to_set = 3; }
        if (progression == 4) { amount_to_set = 5; }
        if (progression == 5) { amount_to_set = 7; }
        if (progression == 6) { amount_to_set = 8; }
        if (progression == 7) { amount_to_set = 10; }
        if (progression == 8) { amount_to_set = 10; }

        head_chosen = all_head_materials[Random.Range(0, all_head_materials.Length)];
        body_chosen = all_body_materials[Random.Range(0, all_body_materials.Length)];
        leg_chosen = all_leg_materials[Random.Range(0, all_leg_materials.Length)];

        int current_set = 0;
        GameObject chosen_one;

        Material selected_head;
        Material selected_body;
        Material selected_leg;

        while (current_set != amount_to_set)
        {
            selected_head = head_chosen;
            selected_body = body_chosen;
            selected_leg = leg_chosen;

            while (head_chosen == selected_head || body_chosen == selected_body || leg_chosen == selected_leg)
            {
                selected_head = all_head_materials[Random.Range(0, all_head_materials.Length)];
                selected_body = all_body_materials[Random.Range(0, all_body_materials.Length)];
                selected_leg = all_leg_materials[Random.Range(0, all_leg_materials.Length)];
            }

            chosen_one = null;
            bool chosen_bool = false;
            int limit_break = 0;

            while ((chosen_one == null || chosen_bool) && limit_break < 500)
            {
                limit_break += 1;
                chosen_one = all_mans[Random.Range(0, all_mans.Length)];
                if (chosen_one.GetComponent<LookMani>()) { chosen_bool = (chosen_one.GetComponent<LookMani>().setup_id == setup_id); }
                Debug.Log(chosen_bool);
            }
            if (limit_break == 500) { Debug.Log("LIMIT BROKE"); Debug.Log(chosen_one); }

            chosen_one.GetComponent<LookMani>().setup_id = setup_id;
            chosen_one.GetComponent<LookMani>().Setup(false, selected_head, selected_body, selected_leg, true);

            current_set += 1;
        }

        all_mans[Random.Range(0, all_mans.Length)].GetComponent<LookMani>().Setup(true, head_chosen, body_chosen, leg_chosen, true);

        template_head.material = head_chosen;
        template_body.material = body_chosen;
        template_leg.material = leg_chosen;

        is_active = true;

        good_light.SetActive(false);
        bad_light.SetActive(false);
        normal_light.SetActive(true);

    }

    public void ResetPuzzle()
    {

        for (int i = 0; i < all_mans.Length; i++)
        {
            all_mans[i].GetComponent<LookMani>().Reset();
        }

        lives = 1;
        setup_id += 1;

        head_chosen = all_head_materials[Random.Range(0, all_head_materials.Length)];
        body_chosen = all_body_materials[Random.Range(0, all_body_materials.Length)];
        leg_chosen = all_leg_materials[Random.Range(0, all_leg_materials.Length)];

        int current_set = 0;
        GameObject chosen_one;

        Material selected_head;
        Material selected_body;
        Material selected_leg;

        while (current_set != amount_to_set)
        {
            selected_head = head_chosen;
            selected_body = body_chosen;
            selected_leg = leg_chosen;

            while (head_chosen == selected_head || body_chosen == selected_body || leg_chosen == selected_leg)
            {
                selected_head = all_head_materials[Random.Range(0, all_head_materials.Length)];
                selected_body = all_body_materials[Random.Range(0, all_body_materials.Length)];
                selected_leg = all_leg_materials[Random.Range(0, all_leg_materials.Length)];
            }

            chosen_one = null;
            bool chosen_bool = false;
            int limit_break = 0;

            while ((chosen_one == null || chosen_bool) && limit_break < 500)
            {
                limit_break += 1;
                chosen_one = all_mans[Random.Range(0, all_mans.Length)];
                if (chosen_one.GetComponent<LookMani>()) { chosen_bool = (chosen_one.GetComponent<LookMani>().setup_id == setup_id); }
                Debug.Log(chosen_bool);
            }
            if (limit_break == 500) { Debug.Log("LIMIT BROKE"); Debug.Log(chosen_one); }

            chosen_one.GetComponent<LookMani>().Setup(false, selected_head, selected_body, selected_leg, true);

            current_set += 1;
        }

        all_mans[Random.Range(0, all_mans.Length)].GetComponent<LookMani>().Setup(true, head_chosen, body_chosen, leg_chosen, true);

        template_head.material = head_chosen;
        template_body.material = body_chosen;
        template_leg.material = leg_chosen;

        is_active = true;

        good_light.SetActive(false);
        bad_light.SetActive(false);
        normal_light.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            lives = 1;
            Debug.Log("OUCH!");
            is_active = false;
            progressor.look_state = 1;
            progressor.CompletedLook();

            good_light.SetActive(false);
            bad_light.SetActive(true);
            normal_light.SetActive(false);

            // Debug.Break();
        }
    }
}
