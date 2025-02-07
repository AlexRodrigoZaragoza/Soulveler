using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TiendaDeLaBase : MonoBehaviour
{
    DialogueScript Interact;
    PlayerGM PGM;
    TestingInputSystem TIS;
    PlayerInput PI;

    public GameObject Cuadro_Dialogos, CanvasTienda, CanvasMazmorra, BotonComprar;

    public TMP_Text Precio;

    public GameObject _Banderolas, _Estandartes, _Banderas, _Globos, _Cajas, _Puestos; //etc...

    public GameObject _TextoBanderolas, _TextoEstandartes, _TextoBanderas, _TextoGlobos, _TextoCajas, _TextoPuestos;

    public bool BD = false, ES = false, BAND = false, GB = false, CJ = false, PT = false, NoObjeto = true; //etc...

    bool BDcomprado, EScomprado, BANDcomprado, GBcomprado, CJcomprado, PTcomprado, DentroDeTienda; //etc...

    int precioObjeto = 50;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        PGM = FindObjectOfType<PlayerGM>();
        Interact = FindObjectOfType<DialogueScript>();
        PI = FindObjectOfType<TestingInputSystem>().gameObject.GetComponent<PlayerInput>();
        TIS = FindObjectOfType<TestingInputSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Cuadro_Dialogos == null || CanvasMazmorra == null)
        {
            Cuadro_Dialogos = GameObject.Find("Cuadro_Dialogos").transform.GetChild(0).gameObject;
            CanvasMazmorra = GameObject.Find("Canvas_Mazmorra");
        }


        if (Input.GetKeyDown(KeyCode.E) && DentroDeTienda == true)
        {
            CanvasTienda.SetActive(true);
            CanvasMazmorra.SetActive(false);

            TIS.enabled = false;
            PI.enabled = false;
        }
    }

    #region BotonesObjetos
    public void Banderolas()
    {
        #region Booles
        BD = true;
        ES = false;
        BAND = false;
        GB = false;
        CJ = false;
        PT = false;
        NoObjeto = false;
        #endregion

        #region Textos
        _TextoBanderolas.gameObject.SetActive(true);
        _TextoEstandartes.SetActive(false);
        _TextoBanderas.SetActive(false);
        _TextoGlobos.SetActive(false);
        _TextoCajas.SetActive(false);
        _TextoPuestos.SetActive(false);
        #endregion
       

        Precio.text = precioObjeto + " TICK";
        BotonComprar.SetActive(true);

        if (BDcomprado == true)
        {
            BD = false;

            Debug.Log("El objeto ya esta comprado");


        }

        Debug.Log("Objeto: Banderolas, seleccionado");

    }

    public void Estandartes()
    {
        #region Booles
        BD = false;
        ES = true;
        BAND = false;
        GB = false;
        CJ = false;
        PT = false;
        NoObjeto = false;
        #endregion

        #region Textos
        _TextoBanderolas.SetActive(false);
        _TextoEstandartes.SetActive(true);
        _TextoBanderas.SetActive(false);
        _TextoGlobos.SetActive(false);
        _TextoCajas.SetActive(false);
        _TextoPuestos.SetActive(false);
        #endregion
       


        Precio.text = precioObjeto + " TICK";
        BotonComprar.SetActive(true);

        if (EScomprado == true)
        {
            ES = false;

            Debug.Log("El objeto ya esta comprado");
        }

        Debug.Log("Objeto: Estandartes, seleccionado");
    }

    public void Banderas()
    {
        #region Booles
        BD = false;
        ES = false;
        BAND = true;
        GB = false;
        CJ = false;
        PT = false;
        NoObjeto = false;
        #endregion

        #region Textos
        _TextoBanderolas.SetActive(false);
        _TextoEstandartes.SetActive(false);
        _TextoBanderas.SetActive(true);
        _TextoGlobos.SetActive(false);
        _TextoCajas.SetActive(false);
        _TextoPuestos.SetActive(false);
        #endregion
        


        Precio.text = precioObjeto + " TICK";
        BotonComprar.SetActive(true);

        if (BANDcomprado == true)
        {
            BAND = false;

            Debug.Log("El objeto ya esta comprado");
        }

        Debug.Log("Objeto: Noria, seleccionado");

    }

    public void Globos()
    {
        #region Booles
        BD = false;
        ES = false;
        BAND = false;
        GB = true;
        CJ = false;
        PT = false;
        NoObjeto = false;
        #endregion

        #region Textos
        _TextoBanderolas.SetActive(false);
        _TextoEstandartes.SetActive(false);
        _TextoBanderas.SetActive(false);
        _TextoGlobos.SetActive(true);
        _TextoPuestos.SetActive(false);
        _TextoCajas.SetActive(false);
        #endregion



        Precio.text = precioObjeto + " TICK";
        BotonComprar.SetActive(true);

        if (GBcomprado == true)
        {
            GB = false;

            Debug.Log("El objeto ya esta comprado");
        }

        Debug.Log("Objeto: Noria, seleccionado");
    }

    public void Cajas()
    {
        #region Booles
        BD = false;
        ES = false;
        BAND = false;
        GB = false;
        CJ = true;
        PT = false;
        NoObjeto = false;
        #endregion

        #region Textos
        _TextoBanderolas.gameObject.SetActive(false);
        _TextoEstandartes.SetActive(false);
        _TextoBanderas.SetActive(false);
        _TextoGlobos.SetActive(false);
        _TextoCajas.SetActive(true);
        _TextoPuestos.SetActive(false);
        #endregion



        Precio.text = precioObjeto + " TICK";
        BotonComprar.SetActive(true);

        if (CJcomprado == true)
        {
            CJ = false;

            Debug.Log("El objeto ya esta comprado");


        }

        Debug.Log("Objeto: Cajas, seleccionado");

    }

    public void Puestos()
    {
        #region Booles
        BD = false;
        ES = false;
        BAND = false;
        GB = false;
        CJ = false;
        PT = true;
        NoObjeto = false;
        #endregion

        #region Textos
        _TextoBanderolas.gameObject.SetActive(false);
        _TextoEstandartes.SetActive(false);
        _TextoBanderas.SetActive(false);
        _TextoGlobos.SetActive(false);
        _TextoCajas.SetActive(false);
        _TextoPuestos.SetActive(true);
        #endregion



        Precio.text = precioObjeto + " TICK";
        BotonComprar.SetActive(true);

        if (PTcomprado == true)
        {
            PT = false;

            Debug.Log("El objeto ya esta comprado");


        }

        Debug.Log("Objeto: Puestos, seleccionado");

    }


    #endregion
    public void ComprarObjetos()
    {

        if (NoObjeto == true) // COMPROBANTE DE OBJETO SELECCIONADO
        {
            Debug.Log("Falta escoger Objeto");
            return;
        }

        #region Objetos
        if (BD == true && PlayerGM.Tickets >= precioObjeto)
        {
            PlayerGM.Tickets = PlayerGM.Tickets - precioObjeto; //Restamos el dinero

            _Banderolas.SetActive(true); //Activamos los objetos comprados

            BDcomprado = true;
            NoObjeto = true;

            Debug.Log("Banderolas Compradas");

        } // BANDEROLAS

        if (ES == true && PlayerGM.Tickets >= precioObjeto)
        {
            PlayerGM.Tickets = PlayerGM.Tickets - precioObjeto;

            _Estandartes.SetActive(true);

            EScomprado = true;
            NoObjeto = true;

            Debug.Log("Estandartes Comprados");

        } // ESTANDARTES

        if (BAND == true && PlayerGM.Tickets >= precioObjeto)
        {
            PlayerGM.Tickets = PlayerGM.Tickets - precioObjeto;

            _Banderas.SetActive(true);

            BANDcomprado = true;
            NoObjeto = true;

            Debug.Log("Noria Comprados");
        } // NORIA

        if (GB == true && PlayerGM.Tickets >= precioObjeto)
        {
            PlayerGM.Tickets = PlayerGM.Tickets - precioObjeto;

            _Globos.SetActive(true);

            Debug.Log("Globos Comprados");

            GBcomprado = true;
            NoObjeto = true;
        } // Globos

        if (CJ == true && PlayerGM.Tickets >= precioObjeto)
        {
            PlayerGM.Tickets = PlayerGM.Tickets - precioObjeto;

            _Cajas.SetActive(true);

            Debug.Log("Cajas Compradas");

            CJcomprado = true;
            NoObjeto = true;
        } // Cajas

        if (PT == true && PlayerGM.Tickets >= precioObjeto)
        {
            PlayerGM.Tickets = PlayerGM.Tickets - precioObjeto;

            _Puestos.SetActive(true);

            Debug.Log("Puestos Comprados");

            PTcomprado = true;
            NoObjeto = true;
        } // Puestos de Comida
        #endregion

    }




    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Cuadro_Dialogos.gameObject.SetActive(true);
            DentroDeTienda = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Cuadro_Dialogos.gameObject.SetActive(false);
        DentroDeTienda = false;
    }

    public void CerrarTienda()
    {
        TIS.enabled = true;
        PI.enabled = true;
        CanvasTienda.SetActive(false);
        CanvasMazmorra.SetActive(true);

        #region Textos
        _TextoBanderolas.SetActive(false);
        _TextoEstandartes.SetActive(false);
        _TextoBanderas.SetActive(false);
        _TextoGlobos.SetActive(false);
        _TextoCajas.SetActive(false);
        _TextoPuestos.SetActive(false);
        #endregion

        BotonComprar.SetActive(false);

    }
}
