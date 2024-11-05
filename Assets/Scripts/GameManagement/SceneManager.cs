using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    [Header("Settings")]
    public bool lockCursor; 

    // Components
    UITransitionController transitionController;

    protected override void Awake()
    {
        base.Awake();

        // Load first scene in build index
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        currentSceneIndex = 1;

        // Lock and hide cursor
        if (lockCursor) { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }

        transitionController = FindObjectOfType<UITransitionController>();
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    int currentSceneIndex;

    public void LoadScene(int sceneIndex)
    {
        // Load Next Level
        StartCoroutine(TransitionToScene(sceneIndex));
    }

    public void LoadNextScene()
    {
        LoadScene(currentSceneIndex + 1);
    }

    IEnumerator TransitionToScene(int sceneIndex)
    {
        transitionController.Close("Wipe Right");

        yield return new WaitForSeconds(0.5f);

        scenesLoading.Add(UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentSceneIndex));
        currentSceneIndex = sceneIndex;
        scenesLoading.Add(UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadingProgress());
    }

    public IEnumerator GetSceneLoadingProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        transitionController.Open("Wipe Left");
    }
}