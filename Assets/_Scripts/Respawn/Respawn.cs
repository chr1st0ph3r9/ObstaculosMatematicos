using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint; // Asigna aquí la posición de respawn en el inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; // Reinicia la velocidad al reaparecer
            }
        }
    }
}
