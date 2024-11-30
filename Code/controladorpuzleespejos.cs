using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class controladorpuzleespejos : MonoBehaviour
{
    // Lista que contiene la combinación de espejos seleccionados por el jugador
    public List<int> combinacion = new List<int>();
    // Lista que contiene la combinación correcta de espejos
    public List<int> combinacioncorrecta = new List<int>();

    // Referencia al objeto "puerta" que se abrirá cuando el jugador resuelva el puzzle
    public GameObject puerta;
    // Bandera que indica si el puzzle ha sido resuelto correctamente
    public bool final = false;

    // Método que se llama al iniciar el juego o el objeto
    void Start()
    {
        // Inicialización si es necesario (vacío en este caso)
    }

    // Método que se llama cada cuadro (frame) del juego
    void Update()
    {
        // Comprobar si el puzzle ha sido resuelto (final es true)
        if (final)
        {
            print("hit");

            // Verificar si la combinación ingresada por el jugador coincide con la correcta
            if (combinacion.SequenceEqual(combinacioncorrecta))
            {
                // Si es correcta, llamar a la función para proceder con el éxito del puzzle
                pass();
                final = true; // Asegurarse de que el puzzle ya está resuelto
            }
            else
            {
                // Si la combinación no es correcta, resetear el estado del puzzle
                final = false;
            }
        }
    }

    // Método que agrega un espejo a la combinación del jugador (máximo 5 y que no sea 9)
    public void listar(int espejo)
    {
        // Verificar que la lista de combinaciones no exceda 5 elementos y que el espejo no sea 9
        if (combinacion.Count < 5 && espejo != 9)
        {
            combinacion.Add(espejo); // Agregar el número del espejo a la lista de combinaciones
        }
    }

    // Método que elimina un espejo de la combinación del jugador
    public void borrar(int espejo)
    {
        // Si el espejo está en la lista de combinaciones, eliminarlo
        if (combinacion.Remove(espejo))
        {
            Debug.Log("Número " + espejo + " eliminado de la lista.");
        }
        else
        {
            Debug.Log("Número " + espejo + " no esta en la lista.");
        }
    }

    // Método que se ejecuta cuando el jugador ha resuelto el puzzle correctamente
    public void pass()
    {
        print("hola");
        puerta.gameObject.GetComponent<telentransporte>().puertahabierta = true; // Abrir la puerta
        gameObject.GetComponent<basepuzle>().acierto(); // Notificar al sistema que el puzzle fue resuelto
    }

    // Método que reinicia el estado de todos los espejos
    public void reinicio()
    {
        // Buscar todos los objetos con la etiqueta "Espejo"
        GameObject[] espejos = GameObject.FindGameObjectsWithTag("Espejo");

        // Iterar sobre todos los espejos encontrados
        foreach (GameObject espejo in espejos)
        {
            // Llamar al método de reinicio del componente de cada espejo
            espejo.GetComponent<puzleespejos>().reinicio();
        }
    }
}
