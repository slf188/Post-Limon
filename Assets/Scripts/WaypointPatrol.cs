using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // para acceder a la clase NavMeshAgent

public class WaypointPatrol : MonoBehaviour
{
    /* Informacion:
    Este programa se utilizara para crear un bucle para que el fantasma patrulle en la ruta que se le indique.
    */
    // para poder referenciar el componente NavMeshAgent
    public NavMeshAgent navMeshAgent;
    // un arreglo con para almacenar los puntos de la ruta
    public Transform[] waypoints;
    // para trackear el punto actual
    int m_CurrentWaypointIndex;

    void Start()
    {
        // destino inicial de NavMeshAgent
        navMeshAgent.SetDestination(waypoints[0].position);
    }


    void Update()
    {
        // revisar si el agente esta cerca del destino
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance){
            // anadir uno a la posicion actual, pero si el incremento hace que el indice sea igual al numero de elementos, entonces regresar a 0
            try {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            } catch(System.Exception e) {
                Debug.Log(e.Message);
            }
            navMeshAgent.SetDestination (waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
