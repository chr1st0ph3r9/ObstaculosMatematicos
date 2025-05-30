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
        while (tiempoActual <= tiempo)
        {
            tiempoActual += Time.deltaTime;
            t = tiempoActual / tiempo;
            Vector3 U = new Vector3(transform.position.x, Mathf.Lerp(A.y, B.y, t), transform.position.z);
            transform.position = U;
           
            yield return null;
            
        }
    }


    public void temporizador()
    {
        tiempoActual = 0;
        A = transform.position;
        B = new Vector3(transform.position.x, Random.Range(0, 15), transform.position.z);

       
        StartCoroutine(corrutina());
        
    }

     


}
