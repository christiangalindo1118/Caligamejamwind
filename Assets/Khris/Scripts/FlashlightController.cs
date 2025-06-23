using UnityEngine;

[RequireComponent(typeof(BatterySystem))]
[RequireComponent(typeof(ScreenFeedback))]
[RequireComponent(typeof(FlashlightAudio))]
public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight")]
    public Light flashlight;
    public float maxRange = 10f;

    private bool isOn = false;
    private float originalIntensity;
    private float originalRange;

    private BatterySystem battery;
    private ScreenFeedback screen;
    private FlashlightAudio audioManager;

    void Start()
    {
        battery = GetComponent<BatterySystem>();
        screen = GetComponent<ScreenFeedback>();
        audioManager = GetComponent<FlashlightAudio>();

        if (flashlight != null)
        {
            originalIntensity = flashlight.intensity;
            originalRange = flashlight.range;
            flashlight.enabled = false;
        }
        screen.UpdateScreen(isOn, battery.CurrentBattery);
    }

    void Update()
    {
        HandleInput();

        if (isOn && battery.HasBattery)
        {
            battery.Drain();
            UpdateFlashlightIntensity();
        }

        if (!battery.HasBattery && isOn)
            TurnOff();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOn) TurnOff();
            else TurnOn();
        }
    }

    public void TurnOn()
    {
        if (!battery.HasBattery) return;
        isOn = true;
        if (flashlight != null) flashlight.enabled = true;
        audioManager.PlayOn();
        screen.UpdateScreen(isOn, battery.CurrentBattery);
    }

    public void TurnOff()
    {
        isOn = false;
        if (flashlight != null) flashlight.enabled = false;
        audioManager.PlayOff();
        screen.UpdateScreen(isOn, battery.CurrentBattery);
    }

    void UpdateFlashlightIntensity()
    {
        if (flashlight == null) return;

        float batteryPercent = battery.BatteryPercent;
        float intensityMultiplier = battery.EvaluateDecay();

        flashlight.intensity = originalIntensity * intensityMultiplier;
        flashlight.range = originalRange * Mathf.Lerp(0.3f, 1f, intensityMultiplier);

        if (batteryPercent < 0.1f)
        {
            float flicker = Mathf.Sin(Time.time * 10f) * 0.3f + 0.7f;
            flashlight.intensity *= flicker;
        }
    }

    public void Recharge(float amount)
    {
        battery.Recharge(amount);
        screen.UpdateScreen(isOn, battery.CurrentBattery);
    }
}
