using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScene : MonoBehaviour
{
    public void new_scene(int new_scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(new_scene);
    }

    public void Quit_thing()
    {
        Application.Quit();
    }
}
