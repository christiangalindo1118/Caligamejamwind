using UnityEngine;

public class FlashlightAudio : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip onClip;
    public AudioClip offClip;
    public AudioClip lowBatteryClip;

    public void PlayOn()
    {
        PlaySound(onClip);
    }

    public void PlayOff()
    {
        PlaySound(offClip);
    }

    public void PlayLowBattery()
    {
        PlaySound(lowBatteryClip);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}