using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletWarp : MonoBehaviour
{

    public Transform tablet;
    public Transform self;

    // Update is called once per frame
    private void LateUpdate()
    {
        tablet.position = self.position;
        tablet.rotation = self.rotation;
    }
}
