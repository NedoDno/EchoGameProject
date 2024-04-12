using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    [SerializeField] private Text shard_num;
    [SerializeField] private Text shad_num;
    [SerializeField] private SettingsPopup settingsPopup;
    public bool isPaused = false;
    void Start()
    {
        settingsPopup.Close();
    }
    void Update()
    {
        shard_num.text = Time.realtimeSinceStartup.ToString();
        shad_num.text = (Time.realtimeSinceStartup/10).ToString();
        if (Input.GetKey("escape"))
        {
            OnOpenSettings();
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
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        OnContinue();
    }

}
