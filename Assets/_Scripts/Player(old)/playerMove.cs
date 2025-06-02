using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlayerController : MonoBehaviour
{
    public CharacterController controller; // Identifico CharacterController
    public float speed = 10f;              // Velocidad del Jugador
    public int playerWeight = 15;         // Peso del jugador
    public float jumpheight = 3;           // Fuerza de salto
    public Transform groundCheck;          // Punto de contacto con el suelo
    public float groundDistance = 0.3f;    // Distancia minima para salto
    public LayerMask groundMask;           // Layer designada como suelo
    private float gravity = 9.8f;          // Gravedad a los saltos
    private Vector3 velocity;              // Creo vector respecto a la velocidad
    private bool isGrounded;               // Comprovacion del suelo
    public Transform trampCheck;
    public float trampDistance = 0.3f;
    public LayerMask trampMask;
    public float jumpTramp = 6;
    private bool isTrampoline;


    void Update()
    {
        float x = Input.GetAxis("Horizontal");                       // Lee eje Horizontal
        float z = Input.GetAxis("Vertical");                         // Lee eje Vertical
        Vector3 move = transform.right * x + transform.forward * z; // Crea direccion para movimiento
        controller.Move(move * speed * Time.deltaTime);                  // Mueve el personaje en X,Z

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Revisa si hay suelo bajo los pies

        if (isGrounded && velocity.y <= 0) // Si el personaje esta en el suelo y la velocidad en Y es menor a 0
        {
            velocity.y = 0; // Velocidad en Y negativa para que no se quede pegado al suelo
        }

        if (Input.GetButtonDown("Jump") && isGrounded) velocity.y = Mathf.Sqrt(jumpheight * playerWeight * -2 * -gravity); // Salto con caida
        velocity.y -= gravity * Time.deltaTime * playerWeight;   // Gravedad                                                                 // Gravedad
        controller.Move(velocity * Time.deltaTime); // Mueve el personaje en Y

        isTrampoline = Physics.CheckSphere(trampCheck.position, trampDistance, trampMask);

        if (isTrampoline) velocity.y = -velocity.y * 1.05f; // Salto con caida

    }
}