using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// source - https://www.youtube.com/watch?v=YMj2qPq9CP8

public class LevelLoader : MonoBehaviour
{
    public Slider slider;


    public void LoadLevel (int sceneIndex)
    {
        slider.gameObject.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }
}
