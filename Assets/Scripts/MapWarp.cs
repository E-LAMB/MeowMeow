using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWarp : MonoBehaviour
{

    public Transform tablet;
    public Transform self;

    public Vector3 my_quat;

    // Update is called once per frame
    private void LateUpdate()
    {
        tablet.position = self.position;
        tablet.position = new Vector3(tablet.position.x,  -4f, tablet.position.z);

        my_quat = self.eulerAngles;
        my_quat.x = 90f;
        my_quat.z = 0f;
        tablet.eulerAngles = my_quat;
    }
}
