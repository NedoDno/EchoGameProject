using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject playereyes;

    public GameObject GameOverPanel;
    public GameObject settingsPopup;
    
    public GameObject startGameButton;
    public bool isPaused = false;
    void Start()
    {
        InitiateUIVisibility();
    }
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            OnOpenSettings();
            playereyes.GetComponent<MouseLookX>().enabled = false;
            playereyes.GetComponentInChildren<MouseLookY>().enabled = false;
        }
    }
    public void OnOpenSettings()
    {
        settingsPopup.GetComponent<SettingsPopup>().Open();
        Time.timeScale = 0;
        isPaused = true;
    }
    public void OnContinue()
    {
        Time.timeScale = 1;
        isPaused = false;
        InitiateUIVisibility();
        settingsPopup.GetComponent<SettingsPopup>().Close();
        playereyes.GetComponent<MouseLookX>().enabled = true;
        playereyes.GetComponentInChildren<MouseLookY>().enabled = true;

    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        OnContinue();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
        isPaused = false;
        InitiateUIVisibility();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void InitiateUIVisibility()
    {
        if (GameOverPanel == null)
        {
            
        Debug.Log("Redo1");
            GameOverPanel = GameObject.Find("GameOver");
        }
        if (settingsPopup == null)
        {
            
        Debug.Log("Redo2");
            settingsPopup = GameObject.Find("Menu");
        }
        if (playereyes == null)
        {
            
        Debug.Log("Redo3");
            playereyes = GameObject.Find("Player");
        }
        if (settingsPopup != null)
        {

        Debug.Log(settingsPopup.transform.Find("continue").GetComponent<Button>().onClick);
            settingsPopup.transform.Find("continue").GetComponent<Button>().onClick.AddListener(OnContinue);
            settingsPopup.transform.Find("exittomenu").GetComponent<Button>().onClick.AddListener(ExitToMenu);
            settingsPopup.GetComponent<SettingsPopup>().Close();
        }
        if (GameOverPanel != null)
        {
            
        Debug.Log("Redo5");
            GameOverPanel.transform.Find("tomenu").GetComponent<Button>().onClick.AddListener(ExitToMenu);
            GameOverPanel.transform.Find("exitgame").GetComponent<Button>().onClick.AddListener(ExitGame);
            GameOverPanel.SetActive(false);
        }
        
        if (startGameButton == null)
        {
            
        Debug.Log("Redo6");
            startGameButton = GameObject.Find("startgame");
        }
        if (startGameButton != null)
        {
            
        Debug.Log("Redo7");
            GameObject.Find("startgame").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("startgame").GetComponent<Button>().onClick.AddListener(StartGame);
        }

    }

}
