using UnityEditor;
using UnityEngine;

public class Gravedad : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;

    [SerializeField] private float fuerzaGravedad = -9.81f;
    [SerializeField] private float distanciaSuelo = 0.1f;
    private Vector3 direccionGravedad=Vector3.down;
    private Vector3 movimientoGravitatorio;

    [SerializeField] private CapsuleCollider colliderJugador;
    [SerializeField] private LayerMask layerSuelo;

    private float alturaJugador;
    private float radioJugador;



    private float velocidadInicialCaida = 0f;    
    private float tiempoDeCaida = 0f;             
    private Vector3 lugarDeInicioDeCaida;     
    private bool estaCayendo = false;


    private bool estaSaltando = false;
    [SerializeField] private float fuerzaSalto = 5f;

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

    private void FixedUpdate()
    {
        RevisarFisicas();
    }

    private void RevisarFisicas()
    {

        if (!RevisarSuelo() && !estaCayendo)
        {
            print("Inicio de caida");
            InicioCaida();
        }

        else if (!RevisarSuelo() && estaCayendo)
        {
            print("Continuar caida");
            ContinuarCaida();
        }

        else if(RevisarSuelo() && estaCayendo)
        {
            print("Finaliza caida");
            FinalizaCaida();
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
        Vector3 topeCollider=transform.position + Vector3.up * (alturaJugador/2) ;
        Vector3 fondoCollider=transform.position+Vector3.up * (alturaJugador / 2+distanciaSuelo);




        if ( Physics.CheckCapsule(topeCollider, fondoCollider, radioJugador,layerSuelo))
        {
            return true;
        }
        else
        {
            return false;

        }


    }

    private void InicioCaida()
    {
        lugarDeInicioDeCaida = transform.position;
        velocidadInicialCaida = 0f;
        tiempoDeCaida = 0f;
        estaCayendo = true;
    }

    private void ContinuarCaida()
    {
        // Incrementar tiempo de caída
        tiempoDeCaida += Time.fixedDeltaTime;


        float currentVelocity = velocidadInicialCaida - (fuerzaGravedad * tiempoDeCaida);

        float totalDisplacement = (velocidadInicialCaida * tiempoDeCaida) - (0.5f * fuerzaGravedad * Mathf.Pow(tiempoDeCaida,2));

        Vector3 targetPosition = lugarDeInicioDeCaida + new Vector3(0, totalDisplacement, 0);

        Vector3 frameMovement = targetPosition - transform.position;

        if (RevisarPorColisionSuelo(frameMovement))
        {
            FinalizaCaida();
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    private bool RevisarPorColisionSuelo(Vector3 direccion)
    {
        if (Physics.CheckCapsule(transform.position, transform.position + direccion, distanciaSuelo))
        {
            return true;
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
    }



    private void Saltar()
    {

    }

    private void AplicarGravedad()
    {
        movimientoGravitatorio += direccionGravedad * fuerzaGravedad * Time.fixedDeltaTime;
        transform.Translate(movimientoGravitatorio);
    }
}

