using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public void ChangeScene(int value)
    {
        StartCoroutine(SceneChangeCoroutine(value));
       
    }

    private IEnumerator SceneChangeCoroutine(int value)
    {
        yield return new WaitForSeconds(0.3f);

        OnSceneChanged(value);
    }

    private void OnSceneChanged(int value)
    {
        SceneManager.LoadScene(value);
    }   

    public void OnQuitScene()
    {
        #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;

        #endif

        Application.Quit();
    }
}
