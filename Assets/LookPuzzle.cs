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

    public void Completed()
    {
        prize.SetActive(true);
    }

    public void Failed()
    {
        lives -= 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpGame();
    }

    public void SetUpGame()
    {
        lives = 3;

        head_chosen = all_head_materials[Random.Range(0, all_head_materials.Length)];
        body_chosen = all_body_materials[Random.Range(0, all_body_materials.Length)];
        leg_chosen = all_leg_materials[Random.Range(0, all_leg_materials.Length)];

        int amount_to_set = all_mans.Length;
        int current_set = 0;

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

            all_mans[current_set].GetComponent<LookMani>().Setup(false, selected_head, selected_body, selected_leg);

            current_set += 1;
        }

        all_mans[Random.Range(0, all_mans.Length)].GetComponent<LookMani>().Setup(true, head_chosen, body_chosen, leg_chosen);

        template_head.material = head_chosen;
        template_body.material = body_chosen;
        template_leg.material = leg_chosen;

    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            Debug.Log("OUCH!");
            Debug.Break();
        }
    }
}
