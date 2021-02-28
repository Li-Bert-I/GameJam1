using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("main");
    }

    public void ShowCredits() {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ShowTutorial() {
        SceneManager.LoadScene("Tutorial");
    }
}
