using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Leer input de movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized; // mantiene la misma velocidad en diagonal
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}