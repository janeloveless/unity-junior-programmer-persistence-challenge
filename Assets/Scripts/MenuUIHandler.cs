using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputField;

    public void StartNewGame()
    {
        Debug.Log("Starting new game!");
        HighScore.instance.currentPlayerName = inputField.text;
        SceneManager.LoadScene(1);
    }

    public void Exit() 
    {
        HighScore.instance.SaveHighScore();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
