using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    public float max;
    public float min;
    public Light my_light;

    // Update is called once per frame
    void Update()
    {
        my_light.intensity = Random.Range(min, max);
    }
}
