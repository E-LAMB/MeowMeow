using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotator : MonoBehaviour
{

    public Transform to_spin;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        to_spin.eulerAngles = new Vector3(0f, to_spin.eulerAngles.y + (speed * Time.deltaTime), 0f);
    }
}
