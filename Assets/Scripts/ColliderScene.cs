using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScene : MonoBehaviour
{

    public int new_scene;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(new_scene);
        }
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
