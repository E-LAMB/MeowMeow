using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParallax : MonoBehaviour
{

    public Material my_mat;
    public Vector2 my_speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        my_mat.mainTextureOffset = new Vector2(my_mat.mainTextureOffset.x + (my_speed.x * Time.deltaTime), my_mat.mainTextureOffset.y + (my_speed.y * Time.deltaTime));
    }
}
