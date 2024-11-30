using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class puzleespejos : MonoBehaviour
{
    // Referencia al objeto controlador
    public GameObject controlador;

    // Referencia al BoxCollider de la esfera (rayog)
    private BoxCollider esferaCol;

    // Estado de activación del espejo
    public bool activo = true;

    // Identificador único del espejo
    public int id;

    // Rotación del espejo (en grados)
    public int rotacion;

    // Posición actual del espejo
    public int posicion;

    // Posición inicial del espejo
    public int posicion_inicial;

    // Referencias a los objetos del rayo y su visualización
    public GameObject rayog;
    public GameObject rayovis;

    // Bandera booleana para controlar la activación del rayo
    bool x = true;
    bool y = true;

    // Activador de la interacción
    public int actibador = 0;

    // Objeto que fue impactado por el rayo
    public GameObject objetoImpactado;

    // Estado de impacto
    public bool impacto = false;

    // Referencia al jugador y su animador
    public GameObject player;
    Animator animator;

    // Longitud del rayo
    float longitudRayo = 7f;

    // Start is called before the first frame update
    void Start()
    {
        // Obtiene el componente Animator del jugador
        animator = player.GetComponent<Animator>();

        // Establece la posición inicial del espejo
        posicion = posicion_inicial;

        // Obtiene el BoxCollider del objeto del rayo
        esferaCol = rayog.GetComponent<BoxCollider>();

        // Desactiva y luego reactiva el collider
        esferaCol.enabled = false;
        esferaCol.enabled = true;

        // Rota el espejo
        RotarEspejo();
    }

    // Método para rotar el espejo
    void RotarEspejo()
    {
        // Incrementa la posición para cambiar el ángulo de rotación
        posicion++;

        // Dependiendo de la posición, establece la rotación en grados
        switch (posicion)
        {
            case 0: rotacion = 0; break;
            case 1: rotacion = 45; break;
            case 2: rotacion = 90; break;
            case 3: rotacion = 135; break;
            case 4: rotacion = 180; break;
            case 5: rotacion = 225; break;
            case 6: rotacion = 270; break;
            case 7: rotacion = 315; break;
            default:
                rotacion = 0;
                posicion = 0;
                break;
        }
        //juaneselmejor:)

        // Inicia la coroutine para rotar el espejo de manera suave
        StartCoroutine(rotarespejo());
    }

    // Método para manejar las entradas táctiles o clics del ratón
    void HandleInputs()
    {
        // Verifica si hay entradas táctiles en dispositivos móviles
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Obtiene el primer toque en la pantalla
            if (touch.phase == TouchPhase.Began) // Si el toque ha comenzado
            {
                ProcessTouch(touch.position); // Procesa la posición del toque
            }
        }
        // Verifica si hay clics con el ratón en dispositivos de escritorio
        else if (Input.GetMouseButtonDown(0)) // Si se ha hecho clic con el ratón
        {
            ProcessTouch(Input.mousePosition); // Procesa la posición del clic
        }
    }

    // Procesa la posición de un toque o clic
    void ProcessTouch(Vector3 touchPosition)
    {
        // Convierte la posición táctil/clic en un rayo desde la cámara
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        // Verifica si el rayo colisiona con algún objeto con un collider
        if (Physics.Raycast(ray, out hit))
        {
            // Si el objeto tocado/clicado es este GameObject
            if (hit.transform == transform)
            {
                RotarEspejo(); // Rota el espejo si se hace clic/toca
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Llama a HandleInputs cada vez que se actualiza el frame
        HandleInputs();

        // Si hay un impacto y el espejo está activo
        if (impacto && activo)
        {
            // Activa el rayo
            if (x)
            {
                rayog.SetActive(true);
                rayovis.SetActive(true);
                x = false;
                y = true;
            }
        }
        else
        {
            // Desactiva el rayo
            if (y)
            {
                y = false;
                x = true;
                rayog.SetActive(false);
                rayovis.SetActive(false);
            }
        }

        // Calcula la dirección del rayo
        Vector3 origenRayo = transform.position;
        Vector3 direccionRayo = -transform.forward;
        Ray rayo = new Ray(origenRayo, direccionRayo);
        RaycastHit hit;

        // Dibuja el rayo para depuración
        Debug.DrawRay(origenRayo, direccionRayo * longitudRayo, Color.red);

        // Si hay un impacto, verifica qué objeto fue golpeado
        if (impacto)
        {
            if (Physics.Raycast(rayo, out hit, longitudRayo))
            {
                if (hit.collider.CompareTag("Espejo"))
                {
                    print("golpeo");
                    rayog.transform.position = hit.collider.transform.position; // Mueve el rayo al objeto impactado
                }
                if (hit.collider.CompareTag("Final"))
                {
                    hit.collider.GetComponent<controladorpuzleespejos>().final = true; // Marca el final del puzzle
                }
                if (hit.collider.CompareTag("simbolos"))
                {
                    hit.collider.gameObject.GetComponent<puzlesimbolos>().id_espejo = id; // Asocia el espejo al símbolo
                }

                // Cambia el tamaño del rayo
                float distance = hit.distance * 3;
                rayovis.transform.localScale = new Vector3(rayovis.transform.localScale.x, rayovis.transform.localScale.y, distance);
            }
        }
    }

    // Coroutine para rotar suavemente el espejo
    IEnumerator rotarespejo()
    {
        animator.SetBool("rotar", true);
        AudioManager.INSTANCE.PlaySFX(6); // Reproduce un sonido
        while (Mathf.Abs(gameObject.transform.eulerAngles.y - rotacion) > 0.1f)
        {
            yield return new WaitForSeconds(0.001f);
            Quaternion rotacionDeseada = Quaternion.Euler(0, rotacion, 0);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotacionDeseada, 2 * Time.deltaTime);
        }
        animator.SetBool("rotar", false);
    }

    // Enciende el espejo y activa el impacto
    public void encender(int id_activador)
    {
        if (!impacto)
        {
            impacto = true;
            actibador = id_activador;
            print("activado por " + actibador);
            controlador.GetComponent<controladorpuzleespejos>().listar(id);
        }
    }

    // Apaga el espejo y desactiva el impacto
    public void apagar(int id_activador)
    {
        if (impacto)
        {
            if (id_activador == actibador)
            {
                impacto = false;
                actibador = 0;
                print("apagado por " + actibador);
                controlador.GetComponent<controladorpuzleespejos>().borrar(id);
            }
        }
    }

    // Reinicia el espejo a su posición inicial
    public void reinicio()
    {
        posicion = posicion_inicial;
        RotarEspejo();
    }

    // Maneja la colisión con el rayo
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rayo"))
        {
            if (other.transform.parent.GetComponent<puzleespejos>().impacto == true)
            {
                other.transform.parent.GetComponent<puzleespejos>().objetoImpactado = gameObject;
                encender(other.transform.parent.GetComponent<puzleespejos>().id);
            }
        }
    }

    // Maneja la salida de la colisión con el rayo
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("rayo"))
        {
            apagar(other.transform.parent.GetComponent<puzleespejos>().id);
        }
    }
}
