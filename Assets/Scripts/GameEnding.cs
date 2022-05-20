using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour{

    // periodo de tiempo para que se desvanesca la pantalla
    // la variable debe ser publica para que se pueda modificar en el inspector
    public float fadeDuration = 1f; // el desvanecimiento ocurrira en 1 segundo
    // el desvanecimiento debe ocurrir solo cuando el personaje llege al trigger o a la meta
    // referencia al personaje
    public GameObject player;
    // crear una variable publica para el grupo canvas
    public CanvasGroup exitBackgroundImageCanvasGroup;
    // necesitamos saber cuando desvanecer el canvas
    bool m_isPlayerAtExit;
    // un temporizador para asegurar que el juego no se acabe antes de que el desvanecimiento termine
    float m_Timer;

    // esta funcion se utiliza para detectar si el personaje llego al trigger
    void onTriggerEnter(Collider other){
        // revisar si el objeto que entro en el trigger es el personaje
        if(other.gameObject == player){
            m_isPlayerAtExit = true;
        }
    }

    // por que onTriggerEnter es llamado solo una ocasion, necesitamos a update que es llamada constantemente
    void Update(){
        // revisar si el personaje llego al trigger
        if(m_isPlayerAtExit){
            // llamar la funcion para concluir el juego
            EndLevel();
        }
    }

    void EndLevel(){
        // el temporizador se incrementa en el tiempo
        m_Timer += Time.deltaTime;
        // modificar el alpha del canvas, 0 cuando el temporizador es 0 y 1 cuando el temporizador es mayor a 0
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration; // la imagen se desvanene cuando el personaje llega al trigger
        // salir del juego cuando el desvanecimiento es completado
        if(m_Timer > fadeDuration + 1f){ // + 1f para anadir un segundo mas en desvanecer la imagen
            // salir del juego
            Application.Quit();
        }
    }

}
