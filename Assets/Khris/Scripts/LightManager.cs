using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [Header("Referencias de Luz")]
    public Light2D initialLight;  // Luz ambiente inicial
    public Light2D tabletLight;   // Luz de la Tablet (hija del Player)

    void Start()
    {
        if (initialLight != null)
            initialLight.enabled = true;

        if (tabletLight != null)
            tabletLight.enabled = false;
    }

    public void ActivateTabletLight()
    {
        Debug.Log("[LightManager] Activando la luz de la Tablet");

        if (initialLight != null)
            initialLight.enabled = false;

        if (tabletLight != null)
            tabletLight.enabled = true;
    }
}


