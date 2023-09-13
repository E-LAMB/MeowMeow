using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSpark : MonoBehaviour
{

    public LayerMask sparky;
    public Renderer my_renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        my_renderer.enabled = Physics.CheckSphere(gameObject.transform.position, 25f, sparky);
    }
}
