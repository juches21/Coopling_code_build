using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzlecuerdas : MonoBehaviour
{
    // Referencias a los puentes que serán animados cuando el puzzle se resuelva
    public GameObject puente1;
    public GameObject puente2;

    // Arreglo de cuerdas y paneles asociados
    public GameObject[] cuerdas;
    public GameObject[] panel;

    // Variable para almacenar el índice aleatorio de la cuerda y el panel seleccionados
    int indiceAleatorio;

    // Referencias a los animadores de los puentes
    Animator puente1_animator;
    Animator puente2_animator;

    // Método Start() se ejecuta al principio cuando el juego comienza
    private void Start()
    {
        // Obtiene los animadores de los puentes para controlar sus animaciones
        puente1_animator = puente1.GetComponent<Animator>();
        puente2_animator = puente2.GetComponent<Animator>();

        // Genera un índice aleatorio entre 0 y el tamaño del array de cuerdas
        indiceAleatorio = Random.Range(0, cuerdas.Length);

        // Activa la cuerda y el panel correspondientes al índice aleatorio generado
        cuerdas[indiceAleatorio].SetActive(true);
        panel[indiceAleatorio].SetActive(true);
    }

    // Método que se llama cuando el puzzle se resuelve correctamente
    public void correcto()
    {
        // Reproduce el sonido correspondiente a un acierto
        AudioManager.INSTANCE.PlaySFX(7);

        // Llama al método "acierto()" de la clase basepuzle (probablemente para registrar el acierto)
        gameObject.GetComponent<basepuzle>().acierto();

        // Activa las animaciones de los puentes, poniéndolos en estado "activo"
        puente1_animator.SetBool("activo", true);
        puente2_animator.SetBool("activo", true);
    }

    // Método que se llama cuando el puzzle se resuelve incorrectamente
    public void incorrecto()
    {
        // Llama al método "fallo()" de la clase basepuzle (probablemente para registrar el fallo)
        gameObject.GetComponent<basepuzle>().fallo();
    }
}
