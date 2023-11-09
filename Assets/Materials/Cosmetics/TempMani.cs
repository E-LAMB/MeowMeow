using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TempMani : MonoBehaviour
{

    public Transform self;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        Mind.current_tp_frames = 2;
        target = GameObject.FindGameObjectWithTag("XR_RIG").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mind.current_tp_frames > 0)
        {
            self.LookAt(target.position);
            self.eulerAngles = new Vector3(0f, self.eulerAngles.y + 180f, 0f);
        }
    }
}
