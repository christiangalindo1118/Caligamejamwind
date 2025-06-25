// Scripts/BinocularPhotoManager.cs
using UnityEngine;

public class BinocularPhotoManager : MonoBehaviour
{
    public float detectionRadius = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Al hacer clic
        {
            TryTakePhoto();
        }
    }

    void TryTakePhoto()
    {
        Collider2D[] birds = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var bird in birds)
        {
            if (bird.CompareTag("Bird"))
            {
                TakePhoto();
                return; // Tomamos solo una foto por click
            }
        }

        Debug.Log("No hay pájaros en el área.");
    }

    public void TakePhoto()
    {
        Debug.Log("¡Foto tomada!");

        // Aquí después puedes guardar las capturas o incrementar el score.
        // Por ejemplo:
        // FindObjectOfType<ColorRevealManager>().RevealColor(10f);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
#endif
}