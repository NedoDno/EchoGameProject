using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupItem : MonoBehaviour
{
    public enum ItemType { Shard, Shadow, Essence }
    public ItemType itemType;

    void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < 1.5f && Input.GetKeyDown(KeyCode.F))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        PickUpItemManager.Instance.ItemPickedUp(itemType);
        Destroy(gameObject); 
    }
}
