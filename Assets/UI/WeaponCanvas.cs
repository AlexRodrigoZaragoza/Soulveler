using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCanvas : MonoBehaviour
{

    PlayerGM PGM;
    DialogueScript Martillo;
    DialogoMago Mago;
    DialogoBallesta Ballesta;

    PowerUps_Martillo PWM;
    PowerUps_Ballesta PWB;
    PowerUps_Magia PWMG;

    public GameObject Canvas_Armas, Mazmorra;

    // Start is called before the first frame update
    void Start()
    {
        PGM = FindObjectOfType<PlayerGM>();
        Martillo = FindObjectOfType<DialogueScript>();
        Mago = FindObjectOfType<DialogoMago>();
        Ballesta = FindObjectOfType<DialogoBallesta>();

        PWM = FindObjectOfType<PowerUps_Martillo>();
        PWB = FindObjectOfType<PowerUps_Ballesta>();
        PWMG = FindObjectOfType<PowerUps_Magia>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SeleccionarMartillo()
    {
        PlayerGM.armas = 1;
        Debug.Log("Martillo");
    }

    public void SeleccionarBallesta()
    {
        PlayerGM.armas = 2;
        Debug.Log("Ballesta");
    }

    public void SeleccionarMagia()
    {
        PlayerGM.armas = 3;
        Debug.Log("Magia");
    }

    public void SalirArmas()
    {


        PWM.Mision.text = "";
        PWM.DescripcionMision.text = "";
        PWM.SubirDeNivel.SetActive(false);
        PWB.MisionBuzo.text = "";
        PWB.DescripcionMisionBuzo.text = "";
        PWB.SubirDeNivel.SetActive(false);
        PWMG.MisionMago.text = "";
        PWMG.DescripcionMisionMago.text = "";
        PWMG.SubirDeNivel.SetActive(false);

        Canvas_Armas.SetActive(false);
        Mazmorra.SetActive(true);




        TestingInputSystem.stop = false;
    }
}
