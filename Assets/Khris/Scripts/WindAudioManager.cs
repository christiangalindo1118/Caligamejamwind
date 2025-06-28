using UnityEngine;

public class WindAudioManager : MonoBehaviour
{
    public Transform player;
    public AudioSource windAudio;
    
    void Update()
    {
        Vector3 dirToWind = (transform.position - player.position).normalized;
        float dot = Vector3.Dot(player.forward, dirToWind);
        
        // Simula que si miras hacia el viento, lo oyes más fuerte
        windAudio.volume = Mathf.Clamp01(0.5f + dot * 0.5f);
    }
}