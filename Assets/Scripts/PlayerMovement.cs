using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public float turnSpeed = 20f; // angulo en radianes en que el jugador va a rotar por segundo
    
    Animator m_Animator; // Animator nos permite acceder al componente Animator del objeto
    // m_Movement es global para que se pueda utilizar ya sea en Start o en Update
    Rigidbody m_Rigidbody; // RigidBody nos permite acceder al componente RigidBody del objeto
    // para acceder al audio de los pasos del jugador
    AudioSource m_AudioSource;
    Vector3 m_Movement; // // Vector3 es un vector de 3 dimensiones para manejar el movimiento del jugador
    Quaternion m_Rotation = Quaternion.identity; // almacenar las rotaciones del jugador


    // Start is called before the first frame update
    void Start(){
        // Obtenemos el componente Animator del objeto
        m_Animator = GetComponent<Animator>();
        // Obtenemos el componente RigidBody del objeto
        m_Rigidbody = GetComponent<Rigidbody>();
        // Obtenemos el componente AudioSource del objeto
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate(){ // FixedUpdate es llamado solo 50 veces por segundo
        // para mover el personaje de izquierda a derecha, teclas 'a', 'd' '<-' '->'
        float horizontal = Input.GetAxis("Horizontal");
        // para mover el personaje de arriba a abajo, teclas 'w', 's' 'up', 'down'
        float vertical = Input.GetAxis("Vertical");
        // precisar la posicion del personaje en 3 dimensiones 
        m_Movement.Set(horizontal, 0f, vertical); // 0 0float 0
        // mantener la misma magnitud en el vector
        m_Movement.Normalize();
        // determinar si hay entrada horizontal
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        // determinar si hay entrada vertical
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        // determinar si el personaje esta caminando o no
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        // modificar el parametro de si el jugador esta caminando
        m_Animator.SetBool("IsWalking", isWalking);
        // revisar si el personaje esta caminando
        if(isWalking){
            // para que el audio no suene en cada frame:
            if(!m_AudioSource.isPlaying){
                // reproducir el audio
                m_AudioSource.Play();
            }
        } else {
            m_AudioSource.Stop();
        }
        // calcular el vector hacia adelante
        Vector3 desiredForward = Vector3.RotateTowards(
            transform.forward, // parametro vector3
            m_Movement, // parametro vector3
            turnSpeed * Time.deltaTime, // parametro de rotaciones del vector
            0f // parametro de rotaciones del vector
        );
        // rotar el personaje hacia adelante
        m_Rotation = Quaternion.LookRotation(desiredForward);

    }
    // modificar el movimiento del personaje aplicado al Animador
    void OnAnimatorMove(){
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude); // esta es la nueva posicion del personaje
        m_Rigidbody.MoveRotation(m_Rotation); // ajustando la rotacion
    }
}
