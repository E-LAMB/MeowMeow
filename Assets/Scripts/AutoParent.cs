using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoParent : MonoBehaviour
{

    public GameObject parenter;

    public void DoParent()
    {
        parenter = GameObject.FindGameObjectWithTag("Gatherer");
        gameObject.transform.SetParent(parenter.transform, true);
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        Destroy(gameObject.GetComponent<AutoParent>());
    }

}
