using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // El player a seguir
    
    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10);
    
    [Header("Zoom Settings")]
    public float normalZoom = 5f;
    public float zoomInSize = 3f;
    public float zoomSpeed = 2f;
    
    [Header("Boundaries (Optional)")]
    public bool useBoundaries = false;
    public float minX, maxX, minY, maxY;
    
    private Camera cam;
    private float targetZoom;
    private bool isZoomedIn = false;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = normalZoom;
        cam.orthographicSize = normalZoom;
        
        // Si no hay target asignado, buscar el player automáticamente
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // Seguir al target
        FollowTarget();
        
        // Manejar zoom
        HandleZoom();
    }
    
    void FollowTarget()
    {
        Vector3 desiredPosition = target.position + offset;
        
        // Aplicar límites si están habilitados
        if (useBoundaries)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
        }
        
        // Movimiento suave
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    
    void HandleZoom()
    {
        // Interpolar suavemente hacia el zoom objetivo
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }
    
    // Función pública para activar zoom
    public void SetZoom(bool zoomIn)
    {
        targetZoom = zoomIn ? zoomInSize : normalZoom;
        isZoomedIn = zoomIn;
    }
    
    // Getter para saber si está en zoom
    public bool IsZoomedIn()
    {
        return isZoomedIn;
    }
}