using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovement : MonoBehaviour
{
    public Transform puntoPivot;        // Punto desde donde cuelga el hacha
    public float longitud = 2.0f;       // Longitud del "brazo" del pendulo
    public float anguloInicial = 30f;   // Angulo inicial (en grados)
    public float gravedad = 9.81f;      // Gravedad
    public float amortiguacion = 0.01f; // Friccion o perdida de energia
    public float masa = 1.0f;           // Masa del hacha para calcular energía

    private float angulo;               // Angulo actual (en radianes)
    private float velocidadAngular;     // Velocidad angular del pendulo

    void Start()
    {
        angulo = anguloInicial * Mathf.Deg2Rad;

        float periodo = 2 * Mathf.PI * Mathf.Sqrt(longitud / gravedad);
        float altura = longitud * (1 - Mathf.Cos(angulo)); // h = L(1 - cos(ang))
        //float energiaPotencial = masa * gravedad * altura;

        Debug.Log($"[Péndulo] Período aproximado: {periodo:F2} s");
        //Debug.Log($"[Péndulo] Energía potencial inicial: {energiaPotencial:F2} J");
    }

    void Update()
    {
        // Formula del pendulo: ang'' = -(g / L) * sin(ang)
        float aceleracionAngular = -(gravedad / longitud) * Mathf.Sin(angulo);
        velocidadAngular += aceleracionAngular * Time.deltaTime;
        angulo += velocidadAngular * Time.deltaTime;

        // Calcular posicion del hacha basado en el angulo
        Vector3 offset = new Vector3(Mathf.Sin(angulo), -Mathf.Cos(angulo), 0) * longitud;
        transform.position = puntoPivot.position + offset;
        transform.right = Vector3.Cross(Vector3.forward, offset.normalized);
    }
}