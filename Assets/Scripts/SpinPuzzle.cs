using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPuzzle : MonoBehaviour
{

    public Transform self;

    public void Spin(float degrees)
    {
        self.eulerAngles = new Vector3(self.eulerAngles.x, self.eulerAngles.y, self.eulerAngles.z + degrees);
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
