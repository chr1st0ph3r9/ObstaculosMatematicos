using UnityEngine;

public class GalaxyMovement : MonoBehaviour
{
    public float alturaMaxima = 5f;
    public float velocidadSubida = 2f;
    private bool subir = false;
    private float alturaInicial;

    void Start()
    {
        alturaInicial = transform.position.y;
    }

    void Update()
    {
        if (subir && transform.position.y < alturaInicial + alturaMaxima)
        {
            transform.position += Vector3.up * velocidadSubida * Time.deltaTime;
            if (transform.position.y > alturaInicial + alturaMaxima)
            {
                Vector3 pos = transform.position;
                pos.y = alturaInicial + alturaMaxima;
                transform.position = pos;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            subir = true;
        }
    }
}
