using UnityEngine;

public class Rotacion : MonoBehaviour
{
    [Header("Movimiento orbital")]
    public float frecuencia = 1f;  // Vueltas por segundo
    public float amplitud = 1f;    // Radio de la órbita
    public Transform ejeTransform; // Transform del eje de rotación

    private Vector3 planoX;
    private Vector3 planoY;

    void Start()
    {
        if (ejeTransform != null)
        {
            // Calcula dos vectores ortogonales al eje de rotación
            planoX = Vector3.Cross(ejeTransform.up, Vector3.right);
            if (planoX == Vector3.zero)
                planoX = Vector3.Cross(ejeTransform.up, Vector3.forward);
            planoX.Normalize();
            planoY = Vector3.Cross(ejeTransform.up, planoX).normalized;
        }
    }

    void Update()
    {
        if (ejeTransform == null) return;

        float angulo = Time.time * frecuencia * 2f * Mathf.PI; // Radianes
        Vector3 orbita = Mathf.Cos(angulo) * planoX + Mathf.Sin(angulo) * planoY;
        transform.position = ejeTransform.position + orbita * amplitud;
    }
}
