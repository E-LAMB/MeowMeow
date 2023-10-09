using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToHome : MonoBehaviour
{
    // Start is called before the first frame update

    public void ToScene(int spec)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(spec);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
