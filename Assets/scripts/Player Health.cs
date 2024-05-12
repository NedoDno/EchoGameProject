using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 100;
    public Slider hpSlider;
    public GameObject GameOverPanel;
    public AudioSource audioSource;

    void Start()
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpSlider.value = hp;
        audioSource.Play();
        if (hp <= 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            foreach (GameObject ui_el in GameObject.FindGameObjectsWithTag("UI"))
            {
                ui_el.SetActive(false);
            }
        }
    }
}
