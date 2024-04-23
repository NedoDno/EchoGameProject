using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 100;
    public Slider hpSlider;

    void Start()
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpSlider.value = hp;
        if (hp <= 0)
        {
            Debug.Log("Player is dead!");
        }
    }
}
