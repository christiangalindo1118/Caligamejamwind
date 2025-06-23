using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float GameTime { get; private set; }

    public bool TabletCollected { get; private set; } = false;
    public bool BinocularsCollected { get; private set; } = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        GameTime += Time.deltaTime;
    }

    public void SetTabletCollected()
    {
        TabletCollected = true;
        Debug.Log("Tablet ha sido recolectada.");
    }

    public void SetBinocularsCollected()
    {
        BinocularsCollected = true;
        Debug.Log("Binoculares han sido recolectados.");
    }
}