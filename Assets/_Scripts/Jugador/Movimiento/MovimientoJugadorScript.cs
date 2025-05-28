using UnityEngine;

public class MovimientoJugadorScript : MonoBehaviour
{

    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private float velocidad =2f;
    private Vector3 direccionMovimiento;


    //transform.Translate(direccion* Time.deltaTime* velocidad);

    private void Awake()
    {
        inputReader.eventoMoverHorizontal += ActualizarDireccionMovimiento;
    }



    private void OnDisable()
    {
        inputReader.eventoMoverHorizontal -= ActualizarDireccionMovimiento;

    }

    private void FixedUpdate()
    {
        if (direccionMovimiento!=Vector3.zero)
        {
            MovimientoHorizontalJugador();
        }
    }

    private void MovimientoHorizontalJugador()
    {
        transform.Translate(direccionMovimiento * Time.fixedDeltaTime * velocidad);

    }

    private void ActualizarDireccionMovimiento (Vector2 direccion)
    {

        direccionMovimiento.x = direccion.x;
        direccionMovimiento.z = direccion.y;
    }


}
