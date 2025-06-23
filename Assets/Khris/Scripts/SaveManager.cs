using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private HashSet<string> collectedItems = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void MarkItemAsCollected(string uniqueID)
    {
        if (!collectedItems.Contains(uniqueID))
            collectedItems.Add(uniqueID);
    }

    public bool IsItemCollected(string uniqueID)
    {
        return collectedItems.Contains(uniqueID);
    }
}