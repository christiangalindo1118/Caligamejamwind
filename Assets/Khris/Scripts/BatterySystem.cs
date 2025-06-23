using UnityEngine;

public class BatterySystem : MonoBehaviour
{
    [Header("Battery")]
    public float batteryLife = 100f;
    public float drainRate = 5f;
    public AnimationCurve decayCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    [Header("Low Battery Warning")]
    public float warningThreshold = 20f;
    public FlashlightAudio audioManager;

    public float CurrentBattery => batteryLife;
    public float BatteryPercent => batteryLife / 100f;
    public bool HasBattery => batteryLife > 0f;

    private bool lowBatteryTriggered = false;

    void Awake()
    {
        if (audioManager == null)
            audioManager = GetComponent<FlashlightAudio>();
    }

    public void Drain()
    {
        batteryLife -= drainRate * Time.deltaTime;
        batteryLife = Mathf.Clamp(batteryLife, 0f, 100f);

        if (batteryLife < warningThreshold && !lowBatteryTriggered)
        {
            lowBatteryTriggered = true;
            audioManager.PlayLowBattery();
        }
    }

    public void Recharge(float amount)
    {
        batteryLife += amount;
        batteryLife = Mathf.Clamp(batteryLife, 0f, 100f);
        lowBatteryTriggered = false;
    }

    public float EvaluateDecay() => decayCurve.Evaluate(BatteryPercent);
}

