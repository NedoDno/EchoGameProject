using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject playereyes; 
    
    public GameObject GameOverPanel;
    [SerializeField] private SettingsPopup settingsPopup;
    public bool isPaused = false;
    void Start()
    {
        GameOverPanel.SetActive(false);
        settingsPopup.Close();
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
        settingsPopup.Open();
        Time.timeScale = 0;
        isPaused = true;
    }
    public void OnContinue()
    {
        settingsPopup.Close();
        Time.timeScale = 1;
        isPaused = false;
        playereyes.GetComponent<MouseLookX>().enabled = true;
        playereyes. GetComponentInChildren<MouseLookY>().enabled = true;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        OnContinue();
    }
    
    void ExitGame()
    {
        Application.Quit();
    }

}
