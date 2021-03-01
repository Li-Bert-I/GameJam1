using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject CreditsScreen;
    public GameObject MenuScreen;
    public GameObject TutorialScreen;

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public void ShowCredits() {
        MenuScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void ShowMenu() {
        TutorialScreen.SetActive(false);
        CreditsScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }

    public void GoMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ShowTutorial() {
        MenuScreen.SetActive(false);
        TutorialScreen.SetActive(true);
    }
}
