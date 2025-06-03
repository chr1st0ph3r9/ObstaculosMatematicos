using UnityEditor;
using UnityEngine;

public class Gravedad : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;

    [SerializeField] private float fuerzaGravedad = 9.81f;
    [SerializeField] private float distanciaSuelo = 0.1f;
    private Vector3 direccionGravedad=Vector3.down;
    private Vector3 movimientoGravitatorio;

    [SerializeField] private CapsuleCollider colliderJugador;
    [SerializeField] private LayerMask layerSuelo;
    [SerializeField] private CharacterController controller; // Arrástralo desde el inspector
    [SerializeField] private MovimientoJugadorScript movimientoJugador; // Arrástralo desde el inspector

    private float alturaJugador;
    private float radioJugador;



    private float velocidadInicialCaida = 0f;    
    private float tiempoDeCaida = 0f;             
    private Vector3 lugarDeInicioDeCaida;     
    private bool estaCayendo = false;
    private float currentVelocity = 0f; // Velocidad actual de la caida


    private bool estaSaltando = false;
    [SerializeField] private float fuerzaSaltoOriginal = 5f;
    [SerializeField] private float fuerzaSalto = 5f;


    [Header("Trampolines")]
    private bool isTrampoline;
    public Transform trampCheck;
    public float trampDistance = 0.3f;
    public LayerMask trampMask;
    private Vector3 velocity;

    private Vector3 lugarDeSalto;

    private void Awake()
    {
        inputReader.eventoSaltar += Saltar;
    }



    private void OnDisable()
    {
        inputReader.eventoSaltar -= Saltar;

    }

    private void Start()
    {
        alturaJugador = colliderJugador.height;
        radioJugador = colliderJugador.radius;
    }

    private Vector3 movimientoVertical = Vector3.zero;

    private void FixedUpdate()
    {
        RevisarFisicas();

        // Aplica el movimiento horizontal + vertical en cada FixedUpdate
        Vector3 movimientoTotal = movimientoJugador.MovimientoHorizontal + movimientoVertical;
        controller.Move(movimientoTotal);
    }

    private void RevisarFisicas()
    {
        if (!estaSaltando)
        {
            if (!RevisarSuelo() && !estaCayendo)
            {
                InicioCaida();
            }
            else if (!RevisarSuelo() && estaCayendo)
            {
                ContinuarCaida();
            }
            else if (RevisarSuelo() && estaCayendo)
            {
                FinalizaCaida();
            }
        }

        if (estaSaltando)
        {
            ContinuarSalto();
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Vector3 topeCollider = transform.position + Vector3.up * (alturaJugador/distanciaSuelo);
    //    Vector3 fondoCollider = transform.position + Vector3.down * (alturaJugador/distanciaSuelo);

    //    Gizmos.color = Color.yellow;

    //    Gizmos.DrawWireSphere(topeCollider, radioJugador);
    //    Gizmos.DrawWireSphere(fondoCollider, radioJugador);

    //}
    private bool RevisarSuelo()
    {
        Vector3 topeCollider = transform.position + Vector3.up * (alturaJugador / 2);
        Vector3 fondoCollider = transform.position + Vector3.up * (alturaJugador / 2 + distanciaSuelo);
        isTrampoline = Physics.CheckSphere(trampCheck.position, trampDistance, trampMask);

        // Si está sobre el suelo normal
        if (Physics.CheckCapsule(topeCollider, fondoCollider, radioJugador, layerSuelo))
        {
            return true;
        }
        // Si está sobre un trampolín y está cayendo o saltando
        else if (isTrampoline && (estaCayendo || estaSaltando))
        {
            ReboteTrampolin();
            return false;
        }
        else
        {
            return false;
        }
    }

    // Nuevo método para el rebote
    private void ReboteTrampolin()
    {
        estaCayendo = false;
        estaSaltando = true;
        fuerzaSalto += fuerzaSaltoOriginal * 1.5f; // Puedes ajustar el multiplicador para más/menos rebote
        lugarDeSalto = transform.position;
        tiempoDeCaida = 0f;
        velocidadInicialCaida = 0f;
    }

    private void InicioCaida()
    {
        lugarDeInicioDeCaida = transform.position;
        // Empieza con velocidad cero al caer
        currentVelocity = 0f;
        tiempoDeCaida = 0f;
        estaCayendo = true;
    }

    private void ContinuarCaida()
    {
        tiempoDeCaida += Time.fixedDeltaTime;
        currentVelocity -= fuerzaGravedad * Time.fixedDeltaTime;
        float frameDisplacement = currentVelocity * Time.fixedDeltaTime;
        movimientoVertical = new Vector3(0, frameDisplacement, 0);

        if (RevisarPorColisionSuelo(movimientoVertical))
        {
            FinalizaCaida();
            movimientoVertical = Vector3.zero;
        }
    }

    private bool RevisarPorColisionSuelo(Vector3 direccion)
    {
        isTrampoline = Physics.CheckSphere(trampCheck.position, trampDistance, trampMask);
        if (Physics.CheckCapsule(transform.position, transform.position + direccion, distanciaSuelo))
        {
            return true;
        }
        else if (isTrampoline)
        {
            velocidadInicialCaida = -currentVelocity * 1.05f; // Salto con caida
            tiempoDeCaida = 0f;
            return false;
        }
        else
        {
            return false;
        }

        
            
    }


    private void FinalizaCaida()
    {
        lugarDeInicioDeCaida = transform.position;
        velocidadInicialCaida = 0f;
        tiempoDeCaida = 0f;
        estaCayendo = false;
        movimientoVertical = Vector3.zero;
    }



    private void Saltar()
    {
        if (RevisarSuelo() && !estaSaltando && !estaCayendo)
        {
            estaSaltando = true;
            lugarDeSalto = transform.position;
            fuerzaSalto = fuerzaSaltoOriginal;
        }
    }
    private void ContinuarSalto()
    {
        float frameDisplacement = fuerzaSalto * Time.fixedDeltaTime;
        movimientoVertical = new Vector3(0, frameDisplacement, 0);

        fuerzaSalto -= fuerzaGravedad * Time.fixedDeltaTime;

        // Solo finalizar el salto si está cayendo (fuerzaSalto <= 0) y toca el suelo
        if (fuerzaSalto <= 0 && RevisarSuelo())
        {
            FinalizaSalto();
            movimientoVertical = Vector3.zero;
        }
    }
    private void FinalizaSalto()
    {
        estaSaltando = false;
        fuerzaSalto = fuerzaSaltoOriginal;
        movimientoVertical = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
            print("ENTR� EN LA COLISI�N v:");
            if (collision.gameObject.CompareTag("Sun"))
            {
                print("ENTR� EN LA CONDICIONAL :v");
            }
 
    }
}

