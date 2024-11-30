using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulsables : MonoBehaviour
{
    // Referencia al objeto de tipo GameObject que representa el puzzle
    public GameObject puzle;

    // Variable booleana que indica si la interacción es correcta o no
    public bool correcto;

  
    
    void Update()
    {
        // Llama a HandleInputs cada vez que se actualiza el frame
        HandleInputs();
    }

    // Método para gestionar las entradas táctiles o de clic
    void HandleInputs()
    {
        // Verificar si hay entradas táctiles en dispositivos móviles
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Obtiene el primer toque en la pantalla
            if (touch.phase == TouchPhase.Began) // Verifica si el toque acaba de empezar
            {
                ProcessTouch(touch.position); // Procesa la posición del toque
            }
        }
        // Verificar si hay clics con el ratón en dispositivos de escritorio
        else if (Input.GetMouseButtonDown(0)) // Verifica si se hace clic con el botón izquierdo del ratón
        {
            ProcessTouch(Input.mousePosition); // Procesa la posición del clic
        }
    }

    // Método que procesa la posición del toque o clic
    void ProcessTouch(Vector3 touchPosition)
    {
        // Convierte la posición del toque/clic en un rayo desde la cámara
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        // Verifica si el rayo colisiona con algún objeto con un collider
        if (Physics.Raycast(ray, out hit))
        {
            // Si el objeto tocado/clicado es este GameObject
            if (hit.transform == transform)
            {
                OnTouch(); // Ejecuta la función OnTouch si el objeto es tocado/clicado
            }
        }
    }

    // Método que maneja lo que ocurre cuando el objeto es tocado/clicado
    public void OnTouch()
    {
        // Si la interacción no es correcta
        if (!correcto)
        {
            // Llama al método "incorrecto" del script "puzzlecandado" asociado al puzzle
            puzle.GetComponent<puzzlecandado>().incorrecto();
        }
        // Si la interacción es correcta
        if (correcto)
        {
            // Llama al método "correcto" del script "puzzlecandado" asociado al puzzle
            puzle.GetComponent<puzzlecandado>().correcto();
        }
    }
}
