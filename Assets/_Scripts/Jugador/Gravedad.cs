using UnityEngine;

public class Gravedad : MonoBehaviour
{

    [SerializeField] private float fuerzaGravedad = -9.81f;
    [SerializeField] private float distanciaSuelo = 0.5f;
    private Vector3 direccionGravedad=Vector3.down;
    private Vector3 movimientoGravitatorio;


    private void FixedUpdate()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit suelo, distanciaSuelo);
        if (!suelo.collider)
        {
            AplicarGravedad();
        }
        
        
    }

    private void AplicarGravedad()
    {
        movimientoGravitatorio += direccionGravedad * fuerzaGravedad * Time.fixedDeltaTime;
        transform.Translate(movimientoGravitatorio);
    }
}
