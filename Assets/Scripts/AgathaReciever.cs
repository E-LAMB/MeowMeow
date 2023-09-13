using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgathaReciever : MonoBehaviour
{

    public AgathaScript my_agatha;

    public void HasLeftGround_Activate()
    {
        my_agatha.HasLeftGround();
    }

}
