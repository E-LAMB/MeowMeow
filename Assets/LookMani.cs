using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMani : MonoBehaviour
{
    public bool is_right;
    public LookPuzzle my_manager;
    public Renderer myself;

    public void Interacted()
    {
        if (is_right)
        {
            my_manager.Completed();

        } else
        {
            my_manager.Failed();
            gameObject.SetActive(false);
        }
    }

    public void Setup(bool is_correct, Material my_material)
    {
        is_right = is_correct;
        myself.material = my_material;
        //gameObject.transform.position = gameObject.transform.position + new Vector3(0f, 1f, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
