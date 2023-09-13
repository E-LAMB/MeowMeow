using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public Transform outer_ring;
    public Transform outer_ring_2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // outer_ring.eulerAngles = new Vector3 (outer_ring.eulerAngles.z, 0f, 0f);
        outer_ring.Rotate(0f, 30f * Time.deltaTime, 0f);
        outer_ring_2.Rotate(0f, -45f * Time.deltaTime, 0f);
    }
}
