using UnityEngine;

public class BinocularsDetector : MonoBehaviour
{
    public float detectionRadius = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Al hacer click en modo binoculares
        {
            Collider2D[] birds = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
            foreach (var bird in birds)
            {
                if (bird.CompareTag("Bird"))
                {
                    Debug.Log("¡Ave detectada! Lista para la foto.");
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}