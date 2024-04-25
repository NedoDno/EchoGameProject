using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpItemManager : MonoBehaviour
{
    public static PickUpItemManager Instance { get; private set; }

    public int type1Count = 0;
    public int type2Count = 0;
    public int type3Count = 0;

    public TextMeshProUGUI type1Text;
    public TextMeshProUGUI type2Text;
    public TextMeshProUGUI type3Text;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ItemPickedUp(PickupItem.ItemType itemType)
    {
        switch (itemType)
        {
            case PickupItem.ItemType.Type1:
                type1Count++;
                type1Text.text = "x " + type1Count;
                break;
            case PickupItem.ItemType.Type2:
                type2Count++;
                type2Text.text = "x " + type2Count;
                break;
            case PickupItem.ItemType.Type3:
                type3Count++;
                type3Text.text = "x " + type3Count;
                break;
        }
    }
}
