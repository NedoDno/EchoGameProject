using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpItemManager : MonoBehaviour
{
    public static PickUpItemManager Instance { get; private set; }

    public int shardsCount = 0;
    public int shadowsCount = 0;
    public int essenceCount = 0;

    public TextMeshProUGUI shards;
    public TextMeshProUGUI shadows;
    public TextMeshProUGUI essence;
    public BoxCollider door;

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
            case PickupItem.ItemType.Shard:
                shardsCount++;
                shards.text = "x " + shardsCount;
                break;
            case PickupItem.ItemType.Shadow:
                shadowsCount++;
                shadows.text = "x " + shadowsCount;
                break;
            case PickupItem.ItemType.Essence:
                essenceCount++;
                essence.text = "x " + essenceCount;
                break;
        }
        if (shardsCount >= 3)
        {
            door.isTrigger = true;
        }
    }
    public bool ConsumeShadow()
    {
        if (shadowsCount > 0)
        {
            shadowsCount--;
            shadows.text = "x " + shadowsCount;
            return true;
        }
        return false;
    }

    public bool ConsumeEssence()
    {
        if (essenceCount > 0)
        {
            essenceCount--;
            essence.text = "x " + essenceCount;
            return true;
        }
        return false;
    }
}
