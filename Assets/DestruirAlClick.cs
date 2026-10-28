using UnityEngine;

public class DestruirAlClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Al hacer clic sobre la escarcha, se limpia y suma limpieza
        IgnisController ignis = FindFirstObjectByType<IgnisController>();
        if (ignis != null) ignis.Limpiar();
        Destroy(gameObject);
    }
}