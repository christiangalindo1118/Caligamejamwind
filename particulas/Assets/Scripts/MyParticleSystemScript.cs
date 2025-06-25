using UnityEngine;

public class MyParticleSystemScript : MonoBehaviour
{
    void PlayerAndDestroy(ParticleSystem part)
    {
        part = GetComponent<ParticleSystem>();
        part.Play();
        Destroy(gameObject, part.main.duration);
    }
}
