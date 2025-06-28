using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class WindAudioManager : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;

    [Header("Dirección del viento")]
    public Vector3 windDirection = Vector3.forward;

    [Header("Intensidad del viento")]
    public float baseVolume = 0.2f;
    public float directionalMultiplier = 0.8f;
    public float windFluctuationSpeed = 0.5f;
    public float windFluctuationAmount = 0.2f;

    [System.Serializable]
    public class SceneAudioClip
    {
        public string sceneName;
        public AudioClip windClip;
    }

    [Header("Audio por escena")]
    public List<SceneAudioClip> sceneAudioClips;

    private AudioSource windAudio;
    private float fluctuationOffset;

    void Awake()
    {
        windAudio = GetComponent<AudioSource>();
        windAudio.loop = true;
        fluctuationOffset = Random.Range(0f, 100f);

        SetClipForCurrentScene();
    }

    void Update()
    {
        if (!IsSceneConfigured())
        {
            if (windAudio.isPlaying) windAudio.Stop();
            return;
        }

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
            else return;
        }

        if (!windAudio.isPlaying && windAudio.clip != null)
            windAudio.Play();

        UpdateWindVolume();
    }

    void UpdateWindVolume()
    {
        Vector3 dirToWind = windDirection.normalized;
        float dot = Vector3.Dot(player.forward, dirToWind);
        float directionalVolume = baseVolume + directionalMultiplier * Mathf.Clamp01(dot);

        float timeFluctuation = Mathf.Sin(Time.time * windFluctuationSpeed + fluctuationOffset) * windFluctuationAmount;
        float finalVolume = Mathf.Clamp01(directionalVolume + timeFluctuation);

        windAudio.volume = finalVolume;
    }

    void SetClipForCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        foreach (var entry in sceneAudioClips)
        {
            if (entry.sceneName == currentScene)
            {
                windAudio.clip = entry.windClip;
                Debug.Log($"[WindAudioManager] Clip asignado: {entry.windClip?.name ?? "Ninguno"} para la escena {currentScene}");
                return;
            }
        }

        windAudio.clip = null;
        Debug.LogWarning($"[WindAudioManager] No hay clip asignado para la escena {currentScene}");
    }

    bool IsSceneConfigured()
    {
        return windAudio.clip != null;
    }
}
