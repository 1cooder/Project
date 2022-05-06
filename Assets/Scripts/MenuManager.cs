using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject MenuCanvas;
    public GameObject SettingsCanvas;
    public void StartGame(){
        SceneManager.LoadScene("Main");
    }
    public void SetSettings(){
        SettingsCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
    }
    public void ExitSettings(){
        SettingsCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }
    public void ExitGame(){
        Application.Quit();
    }
}
