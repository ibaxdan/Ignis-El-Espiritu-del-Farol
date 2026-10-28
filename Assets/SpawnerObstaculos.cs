using UnityEngine;

public class SpawnerObstaculos : MonoBehaviour
{
    public GameObject prefabEscarcha; // Arrastra aquí tu prefab de Escarcha animado
    public float intervaloSpawn = 4f;
    private float temporizador;

    void Update()
    {
        temporizador += Time.deltaTime;
        if (temporizador >= intervaloSpawn)
        {
            temporizador = 0f;
            GenerarEscarcha();
        }
    }

    void GenerarEscarcha()
    {
        float x = Random.Range(-0.8f, 0.8f);
        float y = Random.Range(-1.2f, 1.2f);

        // Z en -1 para que quede por delante del collider de Ignis
        Vector3 posicionSpawn = new Vector3(x, y, -1f);

        Instantiate(prefabEscarcha, posicionSpawn, Quaternion.identity);
    }
}