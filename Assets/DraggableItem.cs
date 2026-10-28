using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    public enum TipoItem { Combustible, Limpiador }
    public TipoItem tipo;
    public Color colorLlamaAlAlimentar = new Color(1f, 0.4f, 0f); // Naranja por defecto

    private Vector3 posicionInicial;
    private Camera camaraPrincipal;
    private bool arrastrando = false;

    void Start()
    {
        posicionInicial = transform.position;
        camaraPrincipal = Camera.main;
    }

    private void OnMouseDown()
    {
        arrastrando = true;
    }

    private void OnMouseDrag()
    {
        if (arrastrando)
        {
            Vector3 posicionMouse = camaraPrincipal.ScreenToWorldPoint(Input.mousePosition);
            posicionMouse.z = 0; // Mantener en el plano 2D
            transform.position = posicionMouse;
        }
    }

    private void OnMouseUp()
    {
        arrastrando = false;

        // Comprobar si soltamos el objeto sobre el collider de Ignis
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit != null && hit.CompareTag("Player"))
        {
            IgnisController ignis = hit.GetComponent<IgnisController>();
            if (ignis != null)
            {
                if (tipo == TipoItem.Combustible)
                {
                    ignis.Alimentar(colorLlamaAlAlimentar);
                }
                else if (tipo == TipoItem.Limpiador)
                {
                    ignis.Limpiar();
                }
            }
        }

        // Regresar el objeto a su lugar en la mesa
        transform.position = posicionInicial;
    }
}