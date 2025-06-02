using UnityEngine;

public class CamaraScript : MonoBehaviour
{

    [Header("Referencias")]
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private Transform pivoteJugador;

    [Header("parametros de camara")]
    [SerializeField] private float sensibilidad = 2f;
    [SerializeField] private float limiteInferior = -40f;
    [SerializeField] private float limiteSuperior = 80f;

    private float rotacionX;
    private float rotacionY;

    private void Awake()
    {
        inputReader.eventoMirar += MovimientoCamara;

    }
    private void Start()
    {
        rotacionX = transform.rotation.x;
        rotacionY = transform.rotation.y;
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al centro de la pantalla
        Cursor.visible = false; // Oculta el cursor
    }

    private void OnDisable()
    {
        inputReader.eventoMirar -= MovimientoCamara;
    }


    private void MovimientoCamara(Vector2 direccion)
    {

        rotacionX+= direccion.x * sensibilidad * Time.deltaTime;
        rotacionY += direccion.y * sensibilidad * Time.deltaTime;
        rotacionY = Mathf.Clamp(rotacionY, limiteInferior, limiteSuperior);
        transform.rotation = Quaternion.Euler(rotacionY, rotacionX,0);
        pivoteJugador.rotation = Quaternion.Euler(pivoteJugador.rotation.x, rotacionX, pivoteJugador.rotation.z);
    }

}
