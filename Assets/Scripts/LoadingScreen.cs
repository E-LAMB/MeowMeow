using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{

    public GameObject loader_stuff;
    public Image loading_bar;

    public float progress;

    public IEnumerator LoadNewScene(int index)
    {
        loader_stuff.SetActive(true);
        loading_bar.fillAmount = 0f;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;

        // float progress = 0f;

        while (!asyncOperation.isDone)
        {
            progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loading_bar.fillAmount = progress;

            if (progress >= 0.9f)
            {
                loading_bar.fillAmount = 1f;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadTheScene(int scene_no)
    {
        StartCoroutine(LoadNewScene(scene_no));
    }

}
