using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleTrigger : MonoBehaviour
{
    public float radius = 2f;
    //public Transform playerCamera;
    //[SerializeField] private SettingsPopup popup;
    public bool isActivated = false;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 direction = hitCollider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > .5f & !isActivated)
                {
                    isActivated = true;
                    SceneManager.LoadScene("test", LoadSceneMode.Additive);
                }
            }
        }
    }
}
