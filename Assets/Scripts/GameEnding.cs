using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // para manejar la escena y poder reiniciarla

public class GameEnding : MonoBehaviour{
    /* Informacion
    Este programa se utilizara en dos casos:
        - cuando el personaje es atrapado
        - cuando escapa de la casa
    */
    // periodo de tiempo para que se desvanesca la pantalla
    // la variable debe ser publica para que se pueda modificar en el inspector
    public float fadeDuration = 1f; // el desvanecimiento ocurrira en 1 segundo
    // el desvanecimiento debe ocurrir solo cuando el personaje llege al trigger o a la meta
    public float displayImageDuration = 1f; // el desvanecimiento ocurrira en 1 segundo
    // referencia al personaje
    public GameObject player;
    // crear una variable publica para el grupo canvas
    public CanvasGroup exitBackgroundImageCanvasGroup;
    // referencia del audio cuando el personaje sale de la casa
    public AudioSource exitAudio;
    // referencia del audio cuando el personaje es atrapado
    public AudioSource caughtAudio;
    // otro grupo de canvas de imagenes que se proyectaran cuando el personaje es atrapado
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    // necesitamos saber cuando desvanecer el canvas
    bool m_IsPlayerAtExit; // para saber si el personaje esta en la meta
    bool m_IsPlayerCaught; // para saber si el personaje esta atrapado
    // un temporizador para asegurar que el juego no se acabe antes de que el desvanecimiento termine
    float m_Timer;
    // una variable de tipo bool para asegurar que el audio solo se reproduzca una vez
    bool m_HasAudioPlayed;

    // esta funcion se utiliza para detectar si el personaje llego al trigger
    void OnTriggerEnter(Collider other){
        // revisar si el objeto que entro en el trigger es el personaje
        if(other.gameObject == player){
            m_IsPlayerAtExit = true;
        }
    }
    // funcion para dar a conocer que el personaje esta atrapado
    public void CaughtPlayer(){
        m_IsPlayerCaught = true;
    }

    // por que onTriggerEnter es llamado solo una ocasion, necesitamos a update que es llamada constantemente
    void Update(){
        // revisar si el personaje llego al trigger
        if(m_IsPlayerAtExit){
            // llamar la funcion para concluir el juego
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio); // pasar el parametro exitBackgroundImageCanvasGroup y el audio cuando escapa
        } else if(m_IsPlayerCaught){ // revisar si el personaje es atrapado
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio); // pasar el parametro caughtBackgroundImageCanvasGroup y el audio cuando es atrapado
        }
    }
    // ahora la funcion EndLevel() modificara la propiedad alpha del parametro que se pase y podremos indicar si reiniciamos el juego o no con el segundo parametro
    // ahora que queremos reproducir un audio, tenemos que anadir un tercer parametro en la funcion EndLevel() 
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource){
        // chequar si el audio no se ha reproducido
        if(!m_HasAudioPlayed){
            // reproducir el audio
            audioSource.Play();
            // marcar que el audio ya se reprodujo
            m_HasAudioPlayed = true;
        }
        
        // el temporizador se incrementa en el tiempo
        m_Timer += Time.deltaTime;
        // modificar el alpha del canvas, 0 cuando el temporizador es 0 y 1 cuando el temporizador es mayor a 0
        imageCanvasGroup.alpha = m_Timer / fadeDuration; // la imagen se desvanene cuando el personaje llega al trigger
        // salir del juego cuando el desvanecimiento es completado
        if(m_Timer > fadeDuration + displayImageDuration){ // + 1f para anadir un segundo mas en desvanecer la imagen
            // reiniciar el juego o volver al menu
            if(doRestart){
                // reiniciar el juego
                SceneManager.LoadScene(0);
            } else {
                // salir del juego
                Application.Quit();
            }
        }
    }
}