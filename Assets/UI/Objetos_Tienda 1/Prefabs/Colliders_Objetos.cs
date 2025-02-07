using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Colliders_Objetos : MonoBehaviour
{

   
    Tienda tienda;

    // Start is called before the first frame update
    void Start()
    {
        tienda = FindObjectOfType<Tienda>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Vida") //CURACION
        {
            tienda.ObjetoVida = true;
            tienda.PanelDeTexto.SetActive(true);
            tienda.CuadroInteracci�n.SetActive(true);
            tienda.DescripcionObjetos.text = "Restaura 50 puntos de curaci�n";
            Debug.Log("Si");
        }

        else if (other.tag == "Player" && gameObject.tag == "VidaMax") //VIDA MAXIMA
        {
            tienda.ObjetoVidaMax = true;
            tienda.PanelDeTexto.SetActive(true);
            tienda.CuadroInteracci�n.SetActive(true);
            tienda.DescripcionObjetos.text = "Aumenta tu vida m�xima en 25 puntos";
            Debug.Log("Si");

        }

        else if (other.tag == "Player" && gameObject.tag == "Da�o") //AUMENTO DE DA�O
        {
            tienda.ObjetoDa�o = true;
            tienda.PanelDeTexto.SetActive(true);
            tienda.CuadroInteracci�n.SetActive(true);
            tienda.DescripcionObjetos.text = "Aumenta el da�o de la pala en 5 puntos";
            Debug.Log("Si");
        }

        else if (other.tag == "Player" && gameObject.tag == "ESnoLimit") //ESPECTRAL ILIMITADO
        {
            tienda.ObjetoESnoLimit = true;
            tienda.PanelDeTexto.SetActive(true);
            tienda.CuadroInteracci�n.SetActive(true);
            tienda.DescripcionObjetos.text = "Desata todo el poder espectral de forma ilimitada";
            Debug.Log("Si");
        }

        else if (other.tag == "Player" && gameObject.tag == "EspectralMax")
        {
            tienda.EspectralMax = true;
            tienda.PanelDeTexto.SetActive(true);
            tienda.CuadroInteracci�n.SetActive(true);
            tienda.DescripcionObjetos.text = "Aumenta tu capacidad espectral en 25 puntos";
            Debug.Log("Si");
        }


    }

    public void OnTriggerExit(Collider other)
    {
        #region Booleanas
        tienda.ObjetoVida = false;
        tienda.ObjetoVidaMax = false;
        tienda.ObjetoDa�o = false;
        tienda.ObjetoESnoLimit = false;
        tienda.EspectralMax = false;

        tienda.PanelDeTexto.SetActive(false);
        tienda.DescripcionObjetos.text = "";
        tienda.CuadroInteracci�n.SetActive(false);
        #endregion
    }
}
