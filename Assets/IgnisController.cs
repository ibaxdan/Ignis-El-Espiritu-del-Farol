using UnityEngine;
using UnityEngine.UI;

public class IgnisController : MonoBehaviour
{
    [Header("Variables de Estado (0 a 100)")]
    [Range(0, 100)] public float energia = 70f;
    [Range(0, 100)] public float animo = 60f;
    [Range(0, 100)] public float limpieza = 80f;

    [Header("Referencias UI")]
    public Slider sliderEnergia;
    public Slider sliderAnimo;
    public Slider sliderLimpieza;

    [Header("Decaimiento por segundo")]
    public float tasaDecaimiento = 2.0f;

    [Header("Movimiento Fluido (Flotación)")]
    public float velocidadFlotacion = 2.5f;
    public float amplitudFlotacion = 0.25f;

    [Header("Sprites y Transformación")]
    public Sprite spriteNormal;
    public Sprite spriteMagico;
    [Range(0, 100)] public float umbralMagico = 75f;

    private Vector3 posicionBase;
    private Vector3 escalaInicial;
    private SpriteRenderer spriteRenderer;
    private float multiplicadorClic = 1.0f;

    void Start()
    {
        posicionBase = transform.position;
        escalaInicial = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && spriteNormal != null)
        {
            spriteRenderer.sprite = spriteNormal;
            spriteRenderer.color = Color.white;
        }
    }

    void Update()
    {
        // 1. Timers: Decaimiento natural continuo
        energia = Mathf.Max(0, energia - (tasaDecaimiento * Time.deltaTime));
        animo = Mathf.Max(0, animo - (tasaDecaimiento * Time.deltaTime));
        limpieza = Mathf.Max(0, limpieza - (tasaDecaimiento * 0.8f * Time.deltaTime));

        // 2. Actualizar las barras en pantalla
        if (sliderEnergia != null) sliderEnergia.value = energia;
        if (sliderAnimo != null) sliderAnimo.value = animo;
        if (sliderLimpieza != null) sliderLimpieza.value = limpieza;

        // 3. Juice: Escala física reacciona a la energía y al clic
        multiplicadorClic = Mathf.Lerp(multiplicadorClic, 1.0f, Time.deltaTime * 8f);
        float factorEscala = Mathf.Lerp(0.5f, 1.2f, energia / 100f) * multiplicadorClic;
        transform.localScale = escalaInicial * factorEscala;

        // 4. Movimiento vertical fluido continuo
        float nuevaY = posicionBase.y + Mathf.Sin(Time.time * velocidadFlotacion) * amplitudFlotacion;
        transform.position = new Vector3(posicionBase.x, nuevaY, transform.position.z);

        // 5. Feedback visual: Cambio de sprite condicionado al umbral
    // Dentro de Update(), en la sección de cambio de sprite:
    if (spriteRenderer != null && spriteNormal != null && spriteMagico != null)
    {
        spriteRenderer.sprite = (energia >= umbralMagico) ? spriteMagico : spriteNormal;
        // Forzar siempre opacidad al 100% y sin tintes raros:
        spriteRenderer.color = Color.white;
    }
    }

    private void OnMouseDown()
    {
        animo = Mathf.Min(100, animo + 15f);
        multiplicadorClic = 1.35f; // Provoca el estiramiento y rebote elástico
    }

    public void Alimentar(Color nuevoColor)
    {
        energia = Mathf.Min(100, energia + 30f);
        // No alterar spriteRenderer.color aquí
    }
    public void Limpiar()
    {
        limpieza = Mathf.Min(100, limpieza + 35f);
    }
}