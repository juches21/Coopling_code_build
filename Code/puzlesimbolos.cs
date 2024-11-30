using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class puzlesimbolos : MonoBehaviour
{
    // Referencias a los objetos y botones utilizados en el puzzle.
    public GameObject imageToRotate_externo; // Imagen externa que se va a rotar.
    public GameObject imageToRotate_interno; // Imagen interna que se va a rotar.

    public GameObject crsital; // Objeto que se activa cuando el puzzle es resuelto.
    public GameObject espejoreal; // Objeto que controla el espejo real en el puzzle.

    public int id_espejo; // Identificador del espejo.

    // Variables que gestionan la rotación de las imágenes.
    public int rotacion_externo; // Valor de la rotación para la imagen externa.
    public int posicion_externo; // Posición actual de la imagen externa.

    public int rotacion_interno; // Valor de la rotación para la imagen interna.
    public int posicion_interno; // Posición actual de la imagen interna.

    // Posiciones correctas para resolver el puzzle.
    public int posicion_externo_correcta;
    public int posicion_interno_correcta;

    // Variable que indica si el impacto ha ocurrido.
    public bool impacto = false;

    // Método Start, se ejecuta al inicio del juego.
    private void Start()
    {
        gameObject.GetComponent<basepuzle>().encendido = false; // Desactiva el puzzle inicialmente.

        crsital.SetActive(false); // Desactiva el cristal al inicio.
        espejoreal.GetComponent<puzleespejos>().activo = false; // Desactiva el espejo real al inicio.
    }

    // Método Update, se ejecuta en cada frame.
    private void Update()
    {
        // Habilita el collider y enciende el puzzle si el id del espejo es 2.
        if (id_espejo == 2)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<basepuzle>().encendido = true;
        }
    }

    // Método que rota la imagen externa hacia la derecha.
    public void derecha_externo()
    {
        AudioManager.INSTANCE.PlaySFX(2); // Reproduce el sonido de rotación.

        posicion_externo++; // Incrementa la posición de la imagen externa.
        print(posicion_externo); // Imprime la posición para depuración.
        Rotarcirculo_externo(); // Llama a la función para rotar la imagen externa.
    }

    // Método que rota la imagen interna hacia la derecha.
    public void derecha_interno()
    {
        AudioManager.INSTANCE.PlaySFX(2); // Reproduce el sonido de rotación.

        posicion_interno++; // Incrementa la posición de la imagen interna.
        Rotarcirculo_interno(); // Llama a la función para rotar la imagen interna.
    }

    // Método que calcula la rotación de la imagen externa.
    public void Rotarcirculo_externo()
    {
        // Establece el valor de la rotación basado en la posición de la imagen externa.
        switch (posicion_externo)
        {
            case 0:
                rotacion_externo = 0;
                break;
            case 1:
                rotacion_externo = 45;
                break;
            case 2:
                rotacion_externo = 90;
                break;
            case 3:
                rotacion_externo = 135;
                break;
            case 4:
                rotacion_externo = 180;
                break;
            case 5:
                rotacion_externo = 225;
                break;
            case 6:
                rotacion_externo = 270;
                break;
            case 7:
                rotacion_externo = 315;
                break;
            default:
                rotacion_externo = 0;
                posicion_externo = 0;
                break;
        }

        StartCoroutine(rotar_externo()); // Inicia la corrutina para rotar la imagen externa.
    }

    // Método que calcula la rotación de la imagen interna.
    public void Rotarcirculo_interno()
    {
        // Establece el valor de la rotación basado en la posición de la imagen interna.
        switch (posicion_interno)
        {
            case 0:
                rotacion_interno = 0;
                break;
            case 1:
                rotacion_interno = 45;
                break;
            case 2:
                rotacion_interno = 90;
                break;
            case 3:
                rotacion_interno = 135;
                break;
            case 4:
                rotacion_interno = 180;
                break;
            case 5:
                rotacion_interno = 225;
                break;
            case 6:
                rotacion_interno = 270;
                break;
            case 7:
                rotacion_interno = 315;
                break;
            default:
                rotacion_interno = 0;
                posicion_interno = 0;
                break;
        }

        StartCoroutine(rotar_interno()); // Inicia la corrutina para rotar la imagen interna.
    }

    // Corrutina que rota la imagen externa de manera suave.
    IEnumerator rotar_externo()
    {
        // Mientras la diferencia de rotación entre la actual y la deseada sea mayor a 0.5 grados.
        while (Quaternion.Angle(imageToRotate_externo.transform.rotation, Quaternion.Euler(rotacion_externo, 0, 0)) > 0.5f)
        {
            yield return new WaitForSeconds(0.001f); // Espera entre cada movimiento.

            Quaternion rotacionDeseada_ex = Quaternion.Euler(rotacion_externo, 0, 0); // Crea una rotación deseada.
            imageToRotate_externo.transform.rotation = Quaternion.Slerp(imageToRotate_externo.transform.rotation, rotacionDeseada_ex, 2 * Time.deltaTime); // Interpola la rotación hacia la deseada.
        }
    }

    // Corrutina que rota la imagen interna de manera suave.
    IEnumerator rotar_interno()
    {
        // Mientras la diferencia de rotación entre la actual y la deseada sea mayor a 0.1 grados.
        while (Quaternion.Angle(imageToRotate_interno.transform.rotation, Quaternion.Euler(rotacion_interno, 0, 0)) > 0.1f)
        {
            yield return new WaitForSeconds(0.001f); // Espera entre cada movimiento.

            Quaternion rotacionDeseada = Quaternion.Euler(rotacion_interno, 0, 0); // Crea una rotación deseada.
            imageToRotate_interno.transform.rotation = Quaternion.Slerp(imageToRotate_interno.transform.rotation, rotacionDeseada, 2 * Time.deltaTime); // Interpola la rotación hacia la deseada.
        }
    }

    // Método que comprueba si las posiciones de las imágenes coinciden con las correctas.
    public void comprovar()
    {
        if (posicion_externo == posicion_externo_correcta && posicion_interno == posicion_interno_correcta)
        {
            crsital.SetActive(true); // Activa el cristal si se resuelve el puzzle.
            espejoreal.GetComponent<puzleespejos>().activo = true; // Activa el espejo real.
            gameObject.GetComponent<basepuzle>().acierto(); // Llama al método de éxito del puzzle.
        }
        else
        {
            gameObject.GetComponent<basepuzle>().fallo(); // Llama al método de fallo del puzzle.
        }
    }

    // Método que reinicia las posiciones de las imágenes y las rota de nuevo.
    public void reset()
    {
        posicion_externo = 0; // Resetea la posición externa.
        posicion_interno = 0; // Resetea la posición interna.
        Rotarcirculo_interno(); // Rota la imagen interna al inicio.
        Rotarcirculo_externo(); // Rota la imagen externa al inicio.
    }
}
