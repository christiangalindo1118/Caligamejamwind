using UnityEngine;

public class DebugBattery : MonoBehaviour
{
    public FlashlightController flashlight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            flashlight.Recharge(20f);
            Debug.Log("Recharged Battery");
        }
    }
}
