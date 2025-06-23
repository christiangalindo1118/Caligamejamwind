using UnityEngine;
using System.Collections;

public class CollectibleItem : MonoBehaviour
{
    [Header("Item Settings")]
    public string itemName = "Item";
    public Sprite itemIcon;
    public bool isUnique = false;
    public bool isCollected = false;

    [Header("Visual & Audio")]
    public GameObject glowEffect;
    public ParticleSystem collectEffect;
    public AudioClip collectSound;
    public float pickupDelay = 0.5f;

    [Header("Luz (opcional para Tablet)")]
    public LightManager lightManager;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (glowEffect != null)
            glowEffect.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {
            AttemptCollection();
        }
    }

    public void AttemptCollection()
    {
        var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogWarning("[DEBUG] No se encontró el PlayerInventory.");
            return;
        }

        if (!inventory.AddItem(this))
        {
            Debug.Log("[DEBUG] Inventario lleno.");
            return;
        }

        isCollected = true;
        Debug.Log($"[DEBUG] Item recolectado: {itemName}");

        // Activamos la luz solo si el item es la Tablet
        if (itemName == "Tablet" && lightManager != null)
        {
            lightManager.ActivateTabletLight();
        }

        StartCoroutine(CollectionSequence());
    }

    private IEnumerator CollectionSequence()
    {
        if (collectSound != null)
            audioSource.PlayOneShot(collectSound);

        if (collectEffect != null)
            collectEffect.Play();

        yield return new WaitForSeconds(pickupDelay);
        gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
#endif
}





