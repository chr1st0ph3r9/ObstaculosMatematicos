using UnityEngine;

public class MovimientoJugadorScript : MonoBehaviour
{

     [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private float velocidad =2f;

    private void Awake()
    {
        inputReader.eventoMoverHorizontal += MovimientoHorizontalJugador;
    }



    private void OnDisable()
    {
        inputReader.eventoMoverHorizontal -= MovimientoHorizontalJugador;

    }

    private void MovimientoHorizontalJugador(Vector2 direccion)
    {
        transform.Translate(direccion * Time.deltaTime * velocidad);
    }


}
