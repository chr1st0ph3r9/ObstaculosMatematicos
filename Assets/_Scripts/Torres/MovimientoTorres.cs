using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MovimientoTorres : MonoBehaviour
{
    public float tiempo;
    int i;
    public GameObject Torre;
    Vector3 A;
    Vector3 B;
    float t;
    float tiempoActual = 0f;
    

    void Start()
    {
        int i = (int)gameObject.transform.position.y;
        InvokeRepeating("temporizador", tiempo, tiempo);
        
        i = Random.Range(0, 15);
            
    }

    IEnumerator corrutina()
    {
        Vector3 start = A;
        Vector3 end = B;
        float elapsed = 0f;

        while (elapsed < tiempo)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / tiempo);
            Vector3 nextPos = Vector3.Lerp(start, end, t);
            Vector3 delta = nextPos - transform.position;
            transform.Translate(delta, Space.World); // Mueve relativo al mundo
            yield return null;
        }
        transform.position = end; // Asegura que termine exacto
    }


    public void temporizador()
    {
        tiempoActual = 0;
        A = transform.position;
        B = new Vector3(transform.position.x, Random.Range(0, 15), transform.position.z);

       
        StartCoroutine(corrutina());
        
    }

     


}
