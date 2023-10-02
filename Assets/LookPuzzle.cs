using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class LookPuzzle : MonoBehaviour
{

    public GameObject[] all_mans;
    public GameObject prize;
    public int identical_material;
    public Material[] all_materials;

    public Renderer template;

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

        identical_material = Random.Range(0, all_materials.Length);

        int amount_to_set = all_mans.Length;
        int current_set = 0;
        int selected_mat = 0;

        while (current_set != amount_to_set)
        {
            selected_mat = identical_material;

            while (selected_mat == identical_material)
            {
                selected_mat = Random.Range(0, all_materials.Length);
            }

            all_mans[current_set].GetComponent<LookMani>().Setup(false, all_materials[selected_mat]);

            current_set += 1;
        }

        all_mans[Random.Range(0, all_mans.Length)].GetComponent<LookMani>().Setup(true, all_materials[identical_material]);
        template.material = all_materials[identical_material];

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
