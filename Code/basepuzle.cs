using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePuzle : MonoBehaviour
{
    // Referencias a objetos de la escena
    public GameObject camara; // C�mara principal que se mueve para acercarse al puzzle
    public GameObject puntopuzzle; // Punto de destino donde la c�mara debe moverse
    public GameObject puntooriginal; // Punto original de la c�mara antes de acercarse al puzzle
    public GameObject controlador; // Controlador que maneja el conteo de fallos
    public GameObject panel; // Panel de UI que indica el estado del puzzle

    Vector3 posicioncamara; // Almacena la posici�n de la c�mara

    public GameObject jugador_completo; // Referencia al jugador completo (con todas sus animaciones y comportamientos)
    public GameObject jugador; // Referencia al jugador sin animaciones
    public Animator animaciones; // Animator que controla las animaciones del jugador

    public int puzzle; // Identificador del tipo de puzzle actual

    public GameObject panelcontroles; // Panel que muestra los controles del juego
    public GameObject panelpuzzle; // Panel que muestra el puzzle

    public bool encendido = true; // Flag para controlar si el puzzle est� activo

    // Start es llamado antes de la primera actualizaci�n de frame
    void Start()
    {
        // Obtener el componente Animator del jugador
        animaciones = jugador.GetComponent<Animator>();
    }

    // M�todo llamado cuando el jugador toca o hace clic en la pantalla
    public void OnTouch()
    {
        if (encendido)
        {
            // Desactivar el movimiento del jugador mientras interact�a con el puzzle
            jugador_completo.GetComponent<Character>().movible = false;
            // Iniciar la animaci�n de cambio de c�mara
            StartCoroutine(CambioCamara());
        }
    }

    // Manejo de las entradas (toques o clics) del jugador
    void HandleInputs()
    {
        Vector3? inputPosition = null;

        // Detectar si hay un toque o clic en la pantalla
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            inputPosition = Input.GetTouch(0).position;
        else if (Input.GetMouseButtonDown(0))
            inputPosition = Input.mousePosition;

        // Si se detecta un toque o clic, procesar la entrada
        if (inputPosition.HasValue)
            ProcessTouch(inputPosition.Value);
    }

    // Procesar el toque o clic recibido
    void ProcessTouch(Vector3 touchPosition)
    {
        // Hacer un raycast desde la posici�n de entrada y verificar si el objeto fue tocado
        if (Physics.Raycast(Camera.main.ScreenPointToRay(touchPosition), out RaycastHit hit) && hit.transform == transform)
        {
            // Llamar al m�todo OnTouch si el objeto fue tocado
            OnTouch();
        }
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Detectar entradas de toque o clic
        HandleInputs();
    }

    // M�todo para salir del puzzle, con animaci�n de regreso
    public void Salir()
    {
        StartCoroutine(SalirPuzle());
    }

    // Coroutine para acercar la c�mara al puzzle
    IEnumerator CambioCamara()
    {
        // Activar animaciones espec�ficas para el tipo de puzzle
        if (puzzle == 5)
        {
            jugador_completo.transform.position = new Vector3(1.08000004f, 0.537352562f, -1f);
            animaciones.SetBool("espejos", true);
        }
        else
        {
            animaciones.SetBool("puzle", true);
        }

        // Mover la c�mara hacia el punto del puzzle
        while (Vector3.Distance(camara.transform.position, puntopuzzle.transform.position) > 0.01f)
        {
            yield return new WaitForSeconds(0.001f);
            camara.transform.position = Vector3.Lerp(camara.transform.position, puntopuzzle.transform.position, 2 * Time.deltaTime);
            camara.transform.rotation = Quaternion.Slerp(camara.transform.rotation, puntopuzzle.transform.rotation, 2 * Time.deltaTime);
        }

        // Cambiar la UI para mostrar los controles del puzzle
        CambiarUIPuzle();

        // Llamar a funciones espec�ficas para ciertos puzzles
        if (puzzle == 1)
        {
            gameObject.GetComponent<PuzzleCandado>().aparecer = true;
        }

        if (puzzle == 4)
        {
            gameObject.GetComponent<PuzzlePesas>().aparecer = true;
        }
    }

    // Coroutine para devolver la c�mara a su posici�n original
    IEnumerator VolverCambioCamara()
    {
        // Desactivar animaciones del puzzle
        animaciones.SetBool("puzle", false);
        animaciones.SetBool("espejos", false);
        animaciones.SetBool("puzle_completado", true);

        // Mover la c�mara de vuelta a su posici�n original
        while (Vector3.Distance(camara.transform.position, puntooriginal.transform.position) > 0.01f)
        {
            yield return new WaitForSeconds(0.001f);
            camara.transform.position = Vector3.Lerp(camara.transform.position, puntooriginal.transform.position, 2 * Time.deltaTime);
            camara.transform.rotation = Quaternion.Slerp(camara.transform.rotation, puntooriginal.transform.rotation, 2 * Time.deltaTime);
        }

        // Restaurar la UI de los controles
        CambiarUIControles();
        gameObject.SetActive(false);
        animaciones.SetBool("puzle_completado", false);
    }

    // Coroutine para salir del puzzle
    IEnumerator SalirPuzle()
    {
        // Mover la c�mara de vuelta a su posici�n original
        while (Vector3.Distance(camara.transform.position, puntooriginal.transform.position) > 0.01f)
        {
            yield return new WaitForSeconds(0.001f);
            camara.transform.position = Vector3.Lerp(camara.transform.position, puntooriginal.transform.position, 2 * Time.deltaTime);
            camara.transform.rotation = Quaternion.Slerp(camara.transform.rotation, puntooriginal.transform.rotation, 2 * Time.deltaTime);
        }

        // Desactivar las animaciones del puzzle
        animaciones.SetBool("puzle", false);
        animaciones.SetBool("espejos", false);

        // Restaurar la UI de los controles
        CambiarUIControles();
    }

    // M�todo espec�fico para salir del puzzle de espejos
    public void SalirPuzleEspejos()
    {
        // Restaurar la posici�n de la c�mara al punto original
        camara.transform.position = puntooriginal.transform.position;
        camara.transform.rotation = puntooriginal.transform.rotation;

        // Desactivar animaciones de espejos y puzzles
        animaciones.SetBool("puzle", false);
        animaciones.SetBool("espejos", false);

        // Restaurar la UI de los controles
        CambiarUIControles();
    }

    // M�todo para cambiar la UI de controles
    public void CambiarUIControles()
    {
        // Mostrar el panel de controles y ocultar el panel del puzzle
        panelcontroles.SetActive(true);
        panelpuzzle.SetActive(false);
        jugador_completo.GetComponent<Character>().movible = true; // Permitir el movimiento del jugador
    }

    // M�todo para cambiar la UI del puzzle
    public void CambiarUIPuzle()
    {
        // Mostrar el panel del puzzle y ocultar el panel de controles
        panelcontroles.SetActive(false);
        panelpuzzle.SetActive(true);
    }

    // Funci�n para marcar el fallo del jugador en el puzzle
    public void Fallo()
    {
        StartCoroutine(FalloVisual());
    }

    // Coroutine que cambia visualmente el panel de fallo
    IEnumerator FalloVisual()
    {
        panel.gameObject.GetComponent<Image>().color = new Vector4(250, 0, 0, 50); // Cambiar color del panel a rojo
        yield return new WaitForSeconds(0.5f); // Esperar un momento
        panel.gameObject.GetComponent<Image>().color = new Vector4(0, 100, 100, 0); // Restaurar el color original
        controlador.GetComponent<Controlador>().ContarFallos(); // Contar los fallos del jugador
    }

    // Funci�n para marcar el acierto del jugador
    public void Acierto()
    {
        StartCoroutine(VolverCambioCamara());
    }
}
