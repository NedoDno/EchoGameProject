using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SanityManager : MonoBehaviour
{
    public Slider sanitySlider;
    public int fullSanity = 100;
    public float lightRecoveryRate = 5f;
    public float darkDecayRate = 10f;
    public float enemyNearbyAdditionalDecay = 5f;
    public float enemyDetectionRadius = 10f;
    public PostProcessProfile postProcessProfile;
    public GameObject player;
    private int lightSourceCount = 0;

    private Vignette vignette;
    private bool isPlayerInLight = false;
    private bool isGamePaused = false;

    void Start()
    {
        postProcessProfile.TryGetSettings(out vignette);
        sanitySlider.maxValue = fullSanity;
        sanitySlider.value = fullSanity;
        vignette.intensity.value = 0;

        StartCoroutine(LoseSanity());
    }

    IEnumerator LoseSanity()
    {
        while (sanitySlider.value > 0)
        {
            if (isPlayerInLight)
            {
                sanitySlider.value = Mathf.Min(fullSanity, sanitySlider.value + lightRecoveryRate * Time.deltaTime);
            }
            else
            {
                float decayRate = darkDecayRate + (IsEnemyNearby() ? enemyNearbyAdditionalDecay : 0);
                sanitySlider.value = Mathf.Max(0, sanitySlider.value - decayRate * Time.deltaTime);
             }

             UpdateVignetteEffect();
             yield return null;
        }

        Debug.Log("Player has gone insane.");
    }

    void UpdateVignetteEffect()
    {
        float newValue = (sanitySlider.value - sanitySlider.maxValue) * (-1);
        float percent = newValue / sanitySlider.maxValue;
        vignette.intensity.value = percent;
    }

    bool IsEnemyNearby()
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, enemyDetectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightSource"))
        {
            lightSourceCount++;
            UpdateLightState();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightSource"))
        {
            lightSourceCount--;
            UpdateLightState();
        }
    }

    void UpdateLightState()
    {
        isPlayerInLight = lightSourceCount > 0;
    }


    public void ToggleGamePause(bool paused)
    {
        isGamePaused = paused;
    }
}
