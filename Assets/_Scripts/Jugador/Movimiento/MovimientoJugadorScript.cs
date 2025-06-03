using UnityEngine;

public class MovimientoJugadorScript : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private CharacterController controller; // Arrástralo desde el inspector

    private Vector3 direccionMovimiento;

    public Vector3 MovimientoHorizontal { get; private set; }

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
        // Usa el transform del jugador como referencia
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * direccionMovimiento.z + right * direccionMovimiento.x;

        MovimientoHorizontal = moveDir * velocidad * Time.fixedDeltaTime;
    }

    private void ActualizarDireccionMovimiento(Vector2 direccion)
    {
        direccionMovimiento.x = direccion.x;
        direccionMovimiento.z = direccion.y;
    }
}
