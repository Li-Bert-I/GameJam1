using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject PausedScreen;
    public GameObject PauseButton;
    bool isPaused;

    void Start(){
        isPaused = false;
    }

    void Update(){
        if(isPaused){
            Time.timeScale = 0;
        } else{
            Time.timeScale = 1;
        }
    }

    public void PauseGame(){
        isPaused = true;
        PausedScreen.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void ResumeGame(){
        isPaused = false;
        PausedScreen.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void GoMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void ShowTutorial() {
        SceneManager.LoadScene("Tutorial");
    }
}
