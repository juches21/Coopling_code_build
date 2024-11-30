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
    // Referencia a la puerta que se abrir� cuando el puzzle se resuelva correctamente
    public GameObject puerta;


    // Se ejecuta al inicio, cuando se carga el script
    void Start()
    {
   
    }

    // Se ejecuta en cada frame, para verificar si las llaves deben aparecer
    void Update()
    {
        // Si la variable "aparecer" es verdadera, se activa la funci�n para mostrar las llaves
        if (aparecer)
        {
            aparecer_llaves();
        }
    }

    // M�todo que se llama cuando el puzzle se resuelve correctamente
    public void correcto()
    {
        // Reproduce el sonido correspondiente al acertar el puzzle
        AudioManager.INSTANCE.PlaySFX(4);

        // Llama al m�todo "acierto()" de la clase basepuzle (presumiblemente registra el acierto)
        gameObject.GetComponent<basepuzle>().acierto();

        // Abre la puerta rot�ndola en el eje Y
        puerta.gameObject.transform.rotation = Quaternion.Euler(0, -100, 0);
    }

    // M�todo que se llama cuando el puzzle se resuelve incorrectamente
    public void incorrecto()
    {
        // Llama al m�todo "fallo()" de la clase basepuzle (presumiblemente registra el fallo)
        gameObject.GetComponent<basepuzle>().fallo();
    }

    // M�todo que aparece una llave aleatoria del array "llaves"
    void aparecer_llaves()
    {
        // Reproduce el sonido correspondiente a la aparici�n de una llave
        AudioManager.INSTANCE.PlaySFX(3);

        // Cambia la variable "aparecer" a false para evitar que se muestren m�s llaves
        aparecer = false;

        // Genera un �ndice aleatorio dentro del rango del array "llaves"
        int indiceAleatorio = Random.Range(0, llaves.Length);

        // Activa la llave correspondiente al �ndice aleatorio
        llaves[indiceAleatorio].SetActive(true);
    }
}
