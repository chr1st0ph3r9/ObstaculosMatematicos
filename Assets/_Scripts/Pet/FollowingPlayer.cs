using UnityEngine;
using UnityEngine.AI;

public class FollowingPlayer : MonoBehaviour
{
    public Transform jugador; // Asigna el jugador en el Inspector
    private NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (jugador == null || agente == null) return;

        // Asigna el destino del agente al jugador
        agente.SetDestination(jugador.position);
    }
}
