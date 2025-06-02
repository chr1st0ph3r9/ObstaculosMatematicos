using UnityEngine;

public class GalaxyMovement : MonoBehaviour
{
    public float alturaMaxima = 5f;
    public float velocidadSubida = 2f;
    private bool subir = false;
    private float alturaInicial;
    public float checkup= 2;
    public SphereCollider lolplayer;
    private float sunrad;
    public LayerMask playerMask;

    void Start()
    {
        alturaInicial = transform.position.y;
        sunrad= lolplayer.radius;
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
        RevisarJugador();
    }

    void OnCollisionEnter(Collision collision)
    {
        print("ENTRÓ EN LA COLISIÓN v:");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("ENTRÓ EN LA CONDICIONAL :v");
            subir = true;
        }
    }
    private void RevisarJugador()
    {
        if (Physics.CheckSphere(transform.position+Vector3.up*checkup,sunrad,playerMask))
        {
            subir = true;
        }


    }
}
