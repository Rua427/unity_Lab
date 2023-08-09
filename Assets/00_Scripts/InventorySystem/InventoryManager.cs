using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] inventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }


    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            TextMeshProUGUI itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            Button removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }

        }
        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (ItemContent == null) { return; }

        // 해당 transform의 자식 오브젝트를 가져온다
        foreach (Transform item in ItemContent)
        {
            item.Find("RemoveButton").gameObject.SetActive(EnableRemove.isOn);
        }
    }

    public void SetInventoryItems()
    {
        inventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].AddItem(items[i]);
        }
    }
}
