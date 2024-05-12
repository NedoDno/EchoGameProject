using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDoorSceneChanger : MonoBehaviour
{
    public int nextSceneNumber;
    public GameObject gameCompleted;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (nextSceneNumber == 4)
            {
                gameCompleted.SetActive(true);
                foreach (GameObject ui_el in GameObject.FindGameObjectsWithTag("UI"))
                {
                    ui_el.SetActive(false);
                }
            }
            else
            {
                SceneTransition.SwitchToScene("level" + nextSceneNumber.ToString());
            }
        }
    }

}
