using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzlecandado : MonoBehaviour
{

    // Array de llaves que se pueden mostrar aleatoriamente
    public GameObject[] llaves;

    // Controla si las llaves deben aparecer o no
    public bool aparecer = false;
    // Referencia a la puerta que se abrirá cuando el puzzle se resuelva correctamente
    public GameObject puerta;


    // Se ejecuta al inicio, cuando se carga el script
    void Start()
    {
   
    }

    // Se ejecuta en cada frame, para verificar si las llaves deben aparecer
    void Update()
    {
        // Si la variable "aparecer" es verdadera, se activa la función para mostrar las llaves
        if (aparecer)
        {
            aparecer_llaves();
        }
    }

    // Método que se llama cuando el puzzle se resuelve correctamente
    public void correcto()
    {
        // Reproduce el sonido correspondiente al acertar el puzzle
        AudioManager.INSTANCE.PlaySFX(4);

        // Llama al método "acierto()" de la clase basepuzle (presumiblemente registra el acierto)
        gameObject.GetComponent<basepuzle>().acierto();

        // Abre la puerta rotándola en el eje Y
        puerta.gameObject.transform.rotation = Quaternion.Euler(0, -100, 0);
    }

    // Método que se llama cuando el puzzle se resuelve incorrectamente
    public void incorrecto()
    {
        // Llama al método "fallo()" de la clase basepuzle (presumiblemente registra el fallo)
        gameObject.GetComponent<basepuzle>().fallo();
    }

    // Método que aparece una llave aleatoria del array "llaves"
    void aparecer_llaves()
    {
        // Reproduce el sonido correspondiente a la aparición de una llave
        AudioManager.INSTANCE.PlaySFX(3);

        // Cambia la variable "aparecer" a false para evitar que se muestren más llaves
        aparecer = false;

        // Genera un índice aleatorio dentro del rango del array "llaves"
        int indiceAleatorio = Random.Range(0, llaves.Length);

        // Activa la llave correspondiente al índice aleatorio
        llaves[indiceAleatorio].SetActive(true);
    }
}
