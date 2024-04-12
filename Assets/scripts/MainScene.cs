using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        HideMenu();
        SceneManager.LoadSceneAsync("Game");
    }
    public void HideMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
