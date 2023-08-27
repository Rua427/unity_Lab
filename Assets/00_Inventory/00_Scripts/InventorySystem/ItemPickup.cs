using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
    }

    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }
    private void OnMouseDown()
    {
        Pickup();
    }
}
