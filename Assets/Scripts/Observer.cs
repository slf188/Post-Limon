using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    /* Indicacion:
    1. El programa Observer se utiliza para observar el personaje del jugador
    2. Cuando el personaje es observado causara que el juego reinicie
    3. Reutilizaremos este programa para otros enemigos ademas de la Gargola
    */ 
    public Transform player; // para saber donde esta la propiedad Transform del personaje
    public GameEnding gameEnding; // utilizar la propiedad cuando se quiere reiniciar el juego
    bool m_IsPlayerInRange; // para saber si el personaje esta en el rango de vision

    void OnTriggerEnter(Collider other){
        // revisar si el personaje esta en el rango cuando la esta funcion es llamada
        if(other.transform == player){
            m_IsPlayerInRange = true;
        }
    }

    // revisar el caso contrario
    void OnTriggerExit(Collider other){
        if(other.transform == player){
            m_IsPlayerInRange = false;
        }
    }

    void Update(){
        if(m_IsPlayerInRange){
            // Esto crea un vector3 direction que almacena la direccion del personaje desde el punto de vista del enemigo
            Vector3 direction = player.position - transform.position + Vector3.up;
            // esto creara un instance de tipo Raycast que se utiliza para revisar si existen colliders a la vista
            Ray ray = new Ray(transform.position, direction);
            // RaycastHit contiene informacion acerca de que exactamente toco el ray
            RaycastHit raycastHit;
            // el siguiente condicional activara el metodo de Raycast, retornara un dato de tipo bool
            // sera true cuando el raycast encuentre un collider
            // sera false cuando el raycast no encuentre un collider
            if(Physics.Raycast(ray, out raycastHit)){
                // este condicional verifica si el collider que toco el ray es el personaje
                if(raycastHit.collider.transform == player){
                    // aqui es donde reiniciaremos el juego
                    gameEnding.CaughtPlayer(); // el metodo caughtplayer() es publico y se puede llamar desde cualquier script
                }
            }
        }
    }
}
