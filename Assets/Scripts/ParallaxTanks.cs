using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTanks : MonoBehaviour
{

    public float speed = 0.2f;
    Material the_material;

    // Start is called before the first frame update
    void Start()
    {
        the_material  = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        the_material.mainTextureOffset = new Vector2(the_material.mainTextureOffset.x + (speed * Time.deltaTime), the_material.mainTextureOffset.y);
    }
}
