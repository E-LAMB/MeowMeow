using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardSpecific : MonoBehaviour
{

    public string what_are;
    public CosmeticsManager my_manager;
    public NewDressup dressy;


    // Start is called before the first frame update
    public void Used()
    {
        my_manager.current_cosmetics += what_are;
        dressy.ReadCosmetics();
        gameObject.transform.position = new Vector3(0f, -200f, 0f);
    }

}
