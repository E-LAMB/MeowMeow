using System.Collections;
using System.Collections.Generic;
// using TreeEditor;
using UnityEngine;

public class OffsetTheTexture : MonoBehaviour
{

    public Material my_mat;
    public float my_speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        my_mat.mainTextureOffset = new Vector2(my_mat.mainTextureOffset.x, my_mat.mainTextureOffset.y + (my_speed * Time.deltaTime));
    }
}
