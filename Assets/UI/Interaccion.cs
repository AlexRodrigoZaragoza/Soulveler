using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaccion : MonoBehaviour
{
    public DialogueScript Interact;
    public DialogoBallesta Ballesta;
    public DialogoMago Mago;

    bool Dentro;
    public GameObject Cuadro_Dialogos, Canvas_Armas;

    public GameObject FuegoMago, FuegoBuzo, FuegoMazo;

    public GameObject IconoMagia, IconoMartillo, IconoBallesta;

    public bool Hablando, Buzo, Caballero, Magia;

    PlayerGM PGM;
    PowerUps_Ballesta PWB;
    PowerUps_Martillo PWM;
    PowerUps_Magia PWMG;
    GameManager GM;

    private void Awake()
    {



    }


    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        PGM = FindObjectOfType<PlayerGM>();
        PWB = FindObjectOfType<PowerUps_Ballesta>();
        PWM = FindObjectOfType<PowerUps_Martillo>();
        PWMG = FindObjectOfType<PowerUps_Magia>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Interact == null || Ballesta == null || Mago == null || Cuadro_Dialogos == null || Canvas_Armas == null)
        {
            Interact = GameObject.Find("Canvas_Dialogos").transform.GetChild(0).GetComponent<DialogueScript>();
            Ballesta = GameObject.Find("Canvas_Dialogos").transform.GetChild(1).GetComponent<DialogoBallesta>();
            Mago = GameObject.Find("Canvas_Dialogos").transform.GetChild(2).GetComponent<DialogoMago>();

            Cuadro_Dialogos = GameObject.Find("Cuadro_Dialogos").transform.GetChild(0).gameObject;
            Canvas_Armas = GameObject.Find("Canvas_Armas01").transform.GetChild(0).gameObject;

            IconoMagia = GameObject.Find("Iconos").transform.GetChild(7).gameObject;
            IconoBallesta = GameObject.Find("Iconos").transform.GetChild(6).gameObject;
            IconoMartillo = GameObject.Find("Iconos").transform.GetChild(5).gameObject;

        }


        if (Input.GetKeyDown(KeyCode.E) && Dentro && Hablando == false && Buzo == true)
        {
            Hablando = true;
            Ballesta.StartDialogueBuzo();



            Interact.Martillo.gameObject.SetActive(false);
            Mago.mago.gameObject.SetActive(false);
            PWB.Start();

            FuegoMago.SetActive(false);
            IconoMagia.SetActive(false);
            FuegoBuzo.SetActive(true);
            IconoBallesta.SetActive(true);
            FuegoMazo.SetActive(false);
            IconoMartillo.SetActive(false);
            GM.armaEspectralImages[3].enabled = true;
            GM.armaEspectralImages[4].enabled = true;
            GM.armaEspectralImages[0].enabled = true;
            GM.armaEspectralImages[7].enabled = true;
            PlayerGM.armas = 2;
 


        }

        if (Input.GetKeyDown(KeyCode.E) && Dentro && Hablando == false && Caballero == true)
        {
            Hablando = true;
            Interact.StartDialogueCaballero();

            Ballesta.Ballesta.gameObject.SetActive(false);
            Mago.mago.gameObject.SetActive(false);
            PWM.Start();

            FuegoMago.SetActive(false);
            IconoMagia.SetActive(false);
            FuegoBuzo.SetActive(false);
            IconoBallesta.SetActive(false);
            FuegoMazo.SetActive(true);
            IconoMartillo.SetActive(true);
            GM.armaEspectralImages[1].enabled = true;
            GM.armaEspectralImages[2].enabled = true;
            GM.armaEspectralImages[0].enabled = true;
            GM.armaEspectralImages[7].enabled = true;
            PlayerGM.armas = 1;
        }

        if (Input.GetKeyDown(KeyCode.E) && Dentro && Hablando == false && Magia == true)
        {
            Hablando = true;
            Mago.StartDialogueMago();

            Ballesta.Ballesta.gameObject.SetActive(false);
            Interact.Martillo.gameObject.SetActive(false);

            PWMG.Start();

            FuegoMago.SetActive(true);
            IconoMagia.SetActive(true);
            FuegoBuzo.SetActive(false);
            IconoBallesta.SetActive(false);
            FuegoMazo.SetActive(false);
            IconoMartillo.SetActive(false);
            GM.armaEspectralImages[5].enabled = true;
            GM.armaEspectralImages[6].enabled = true;
            GM.armaEspectralImages[0].enabled = true;
            GM.armaEspectralImages[7].enabled = true;
            PlayerGM.armas = 3;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "Buzo")
        {
            Dentro = true;
            Buzo = true;
            Cuadro_Dialogos.gameObject.SetActive(true); //Cuadro Interacción


        }

        if (other.gameObject.tag == "Player" && gameObject.tag == "CaballeroPiruleta")
        {
            Dentro = true;
            Caballero = true;
            Cuadro_Dialogos.gameObject.SetActive(true); //Cuadro Interacción


        }

        if (other.gameObject.tag == "Player" && gameObject.tag == "MagoOscuro")
        {
            Dentro = true;
            Magia = true;
            Cuadro_Dialogos.gameObject.SetActive(true); //Cuadro Interacción


        }
    }

    void OnTriggerExit(Collider other)
    {

        Dentro = false;
        Hablando = false;
        Buzo = false;
        Caballero = false;
        Magia = false;
        Cuadro_Dialogos.gameObject.SetActive(false);


    }


}
