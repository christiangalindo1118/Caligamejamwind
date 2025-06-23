using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventario")]
    public int maxCapacity = 10;

    private List<CollectibleItem> items = new List<CollectibleItem>();

    public bool AddItem(CollectibleItem item)
    {
        if (items.Count >= maxCapacity)
        {
            Debug.Log("Inventario lleno.");
            return false;
        }

        items.Add(item);
        Debug.Log($"Item agregado al inventario: {item.itemName}");
        return true;
    }
}