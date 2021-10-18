using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject start;
    public GameObject tutorial;

 
    public void ShowTutorial(){
        tutorial.SetActive(true);
        start.SetActive(false);
    }

    public void ShowStart(){
        start.SetActive(true);
        tutorial.SetActive(false);
    }

    public void StartGame(){
        SceneManager.LoadScene("SceneFish",LoadSceneMode.Single);

    }
}
