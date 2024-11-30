using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzlepesas : MonoBehaviour
{
    // Referencia al panel que puede cambiar de color
    public GameObject panel;

    // Referencia a las pesas, que son el objeto principal del puzzle
    public GameObject pesas;

    // Referencias a las pesas y sus botones correspondientes
    public GameObject pesa1;
    public GameObject pesaboton1;

    public GameObject pesa2;
    public GameObject pesaboton2;

    public GameObject pesa3;
    public GameObject pesaboton3;

    public GameObject pesa4;
    public GameObject pesaboton4;

    public GameObject pesa5;
    public GameObject pesaboton5;

    // Variable para controlar si las pesas deben aparecer o no
    public bool aparecer = false;

    // Referencia a la puerta que se abrirá si el puzzle es resuelto correctamente
    public GameObject puerta;

    // Variables para el control de color del panel (rojo y verde)
    public Vector4 colorpanel;
    public Vector4 colorpanelrojo = new Vector4(250, 0, 0, 100);
    public Vector4 colorpanelverde = new Vector4(0, 250, 0, 100);

    // Variables para almacenar el valor correcto y el valor actual de las pesas
    public int valorcorrecto;
    public int valoractual;

    // Start se llama al inicio del juego
    void Start()
    {
        // Obtiene el color inicial del panel
        colorpanel = panel.gameObject.GetComponent<Image>().color;
    }

    // Update se llama una vez por frame, controla si las pesas deben aparecer o no
    void Update()
    {
        // Si aparecer es true, las pesas se hacen visibles, si es false, se ocultan
        if (aparecer)
        {
            pesas.SetActive(true);
        }
        else
        {
            pesas.SetActive(false);
        }
    }

    // Método que comprueba si el valor actual es igual al valor correcto
    public void comprovar()
    {
        // Si el valor actual es correcto, se resuelve el puzzle
        if (valorcorrecto == valoractual)
        {
            // Llama al método "acierto()" de la clase basepuzle para indicar que se ha resuelto correctamente
            gameObject.GetComponent<basepuzle>().acierto();

            // Rota la puerta para abrirla
            puerta.gameObject.transform.rotation = Quaternion.Euler(0, -173.88f, 0);

            // Reinicia el valor correcto
            valorcorrecto = 0;

            // Reproduce el sonido de éxito
            AudioManager.INSTANCE.PlaySFX(0);
        }
        else
        {
            // Si el valor es incorrecto, llama al método "fallo()" de la clase basepuzle
            gameObject.GetComponent<basepuzle>().fallo();
        }
    }

    // Métodos para poner las pesas, incrementando el valor actual y desactivando los botones
    public void ponerpesa1()
    {
        // Reproduce el sonido correspondiente
        AudioManager.INSTANCE.PlaySFX(5);

        // Incrementa el valor actual en 1
        valoractual += 1;

        // Activa la pesa 1 y desactiva su botón
        pesa1.gameObject.SetActive(true);
        pesaboton1.gameObject.SetActive(false);
    }

    public void ponerpesa2()
    {
        // Reproduce el sonido correspondiente
        AudioManager.INSTANCE.PlaySFX(5);

        // Incrementa el valor actual en 2
        valoractual += 2;

        // Activa la pesa 2 y desactiva su botón
        pesa2.gameObject.SetActive(true);
        pesaboton2.gameObject.SetActive(false);
    }

    public void ponerpesa3()
    {
        // Reproduce el sonido correspondiente
        AudioManager.INSTANCE.PlaySFX(5);

        // Incrementa el valor actual en 3
        valoractual += 3;

        // Activa la pesa 3 y desactiva su botón
        pesa3.gameObject.SetActive(true);
        pesaboton3.gameObject.SetActive(false);
    }

    public void ponerpesa4()
    {
        // Reproduce el sonido correspondiente
        AudioManager.INSTANCE.PlaySFX(5);

        // Incrementa el valor actual en 4
        valoractual += 4;

        // Activa la pesa 4 y desactiva su botón
        pesa4.gameObject.SetActive(true);
        pesaboton4.gameObject.SetActive(false);
    }

    public void ponerpesa5()
    {
        // Reproduce el sonido correspondiente
        AudioManager.INSTANCE.PlaySFX(5);

        // Incrementa el valor actual en 5
        valoractual += 5;

        // Activa la pesa 5 y desactiva su botón
        pesa5.gameObject.SetActive(true);
        pesaboton5.gameObject.SetActive(false);
    }

    // Método para reiniciar el puzzle, restaurando el estado de las pesas y botones
    public void restart()
    {
        // Reproduce el sonido correspondiente
        AudioManager.INSTANCE.PlaySFX(5);

        // Desactiva todas las pesas y reactiva todos los botones
        pesa1.gameObject.SetActive(false);
        pesaboton1.gameObject.SetActive(true);

        pesa2.gameObject.SetActive(false);
        pesaboton2.gameObject.SetActive(true);

        pesa3.gameObject.SetActive(false);
        pesaboton3.gameObject.SetActive(true);

        pesa4.gameObject.SetActive(false);
        pesaboton4.gameObject.SetActive(true);

        pesa5.gameObject.SetActive(false);
        pesaboton5.gameObject.SetActive(true);

        // Reinicia el valor actual
        valoractual = 0;
    }
}
