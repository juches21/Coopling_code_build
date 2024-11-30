using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulsables : MonoBehaviour
{
    // Referencia al objeto de tipo GameObject que representa el puzzle
    public GameObject puzle;

    // Variable booleana que indica si la interacci�n es correcta o no
    public bool correcto;

  
    
    void Update()
    {
        // Llama a HandleInputs cada vez que se actualiza el frame
        HandleInputs();
    }

    // M�todo para gestionar las entradas t�ctiles o de clic
    void HandleInputs()
    {
        // Verificar si hay entradas t�ctiles en dispositivos m�viles
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Obtiene el primer toque en la pantalla
            if (touch.phase == TouchPhase.Began) // Verifica si el toque acaba de empezar
            {
                ProcessTouch(touch.position); // Procesa la posici�n del toque
            }
        }
        // Verificar si hay clics con el rat�n en dispositivos de escritorio
        else if (Input.GetMouseButtonDown(0)) // Verifica si se hace clic con el bot�n izquierdo del rat�n
        {
            ProcessTouch(Input.mousePosition); // Procesa la posici�n del clic
        }
    }

    // M�todo que procesa la posici�n del toque o clic
    void ProcessTouch(Vector3 touchPosition)
    {
        // Convierte la posici�n del toque/clic en un rayo desde la c�mara
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        // Verifica si el rayo colisiona con alg�n objeto con un collider
        if (Physics.Raycast(ray, out hit))
        {
            // Si el objeto tocado/clicado es este GameObject
            if (hit.transform == transform)
            {
                OnTouch(); // Ejecuta la funci�n OnTouch si el objeto es tocado/clicado
            }
        }
    }

    // M�todo que maneja lo que ocurre cuando el objeto es tocado/clicado
    public void OnTouch()
    {
        // Si la interacci�n no es correcta
        if (!correcto)
        {
            // Llama al m�todo "incorrecto" del script "puzzlecandado" asociado al puzzle
            puzle.GetComponent<puzzlecandado>().incorrecto();
        }
        // Si la interacci�n es correcta
        if (correcto)
        {
            // Llama al m�todo "correcto" del script "puzzlecandado" asociado al puzzle
            puzle.GetComponent<puzzlecandado>().correcto();
        }
    }
}
