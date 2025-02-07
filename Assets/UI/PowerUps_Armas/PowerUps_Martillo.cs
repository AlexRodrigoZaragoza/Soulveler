using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUps_Martillo : MonoBehaviour
{
    //ACTIVAR COSAS EN LOS RESPECTIVOS SCRIPTS : PLAYERGM, TEST ATTACK, ULTYS, INTERACCION, TESTING INPUTS SYSTEM y CLOWN

    public static int enemigosDerrotadosMartillo; //AQUI ALGO


    // Start is called before the first frame update
    TestAttack TE;                                              //DAÑOS
    TestingInputSystem TI;                                      //CDS
    PlayerGM PGM;                                               //COSAS DEL PERSONAJE
    Clown clw;


    float vidaMartillo = 100;
    float dañoMartillo = 15;

    public float dañoRecibido, ArmaduraObtenida;

    public GameObject lvl2, lvl3, lvl4, lvl5, SubirDeNivel;

    public TMP_Text Mision, DescripcionMision, NivelActual;

    int NivelArma = 0;

    bool Nivel1, Nivel2, Nivel3, Nivel4, Nivel5, DañoConseguido, godmode;

    bool Nivel1Conseguido, Nivel2Conseguido, Nivel3Conseguido, Nivel4Conseguido, Nivel5Conseguido;

    public bool GanoArmadura, UltiMartillo;

    bool Objetivo1; //MisionBuzo 4


    private void Awake()
    {
        TE = FindObjectOfType<TestAttack>();
        TI = FindObjectOfType<TestingInputSystem>();
        PGM = FindObjectOfType<PlayerGM>();
        clw = FindObjectOfType<Clown>();
    }
    public void Start()
    {
        PlayerGM.maxHealth = vidaMartillo;
        TE.hamerDamage = dañoMartillo;
        dañoRecibido = 0;

        if (PlayerGM.health < vidaMartillo || PlayerGM.health > vidaMartillo)
        {
            PlayerGM.health = vidaMartillo;
        }

        NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

        if (Nivel5Conseguido == true)
        {
            DescripcionMision.text = "Has completado todas las misiones que tenia, ves y acaba con tus enemigos con nuestro poder".ToUpper();
        }


    }

    #region Botones
    public void LVL1()
    {
        if (enemigosDerrotadosMartillo > 50) enemigosDerrotadosMartillo = 50;

        Mision.text = "Enemigos Derrotados: ".ToUpper() + enemigosDerrotadosMartillo + " / 50";
        DescripcionMision.text = "ACABAS DE INICIAR TU CAMINO EN EL ARTE DEL MANEJO DEL MARTILLO COLOSAL. SAL Y APLASTA A UNOS CUANTOS ENEMIGOS PARA DEMOSTRAR QUE PUEDES MANEJAR ESTA PODEROSA ARMA".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel1 = true;

    } //50 Enemigos

    public void LVL2()
    {
        if (enemigosDerrotadosMartillo > 100) enemigosDerrotadosMartillo = 100;

        Mision.text = "Enemigos derrotados: ".ToUpper() + enemigosDerrotadosMartillo + " / 100";
        DescripcionMision.text = "HAS DEMOSTRADO TU FUERZO CON EL MARTILLO, SIGUE MEJORANDO TU DOMINIO CON EL.".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel2 = true;


    } //100 Enemigos

    public void LVL3()
    {
        if (enemigosDerrotadosMartillo > 100) enemigosDerrotadosMartillo = 100;

        Mision.text = "Enemigos derrotados: ".ToUpper() + enemigosDerrotadosMartillo + " / 100 " + "                  Daño recibido: ".ToUpper() + dañoRecibido + " / 210";
        DescripcionMision.text = "No solo has de fortalecer tu fuerza, sino tu cuerpo, ves y aguanta unos cuantos golpes sin desfallecer".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel3 = true;

    } //100 Enemigos, 210 Daño recibido;

    public void LVL4()
    {

        Mision.text = "Enemigos derrotados: ".ToUpper() + enemigosDerrotadosMartillo + " / 150 " + " Armadura Obtenida: ".ToUpper() + ArmaduraObtenida + " / 40";
        DescripcionMision.text = "Ahora que tu cuerpo es tan duro como lo fué el mio, es hora de demostrar lo duro que te has hecho. Vuelve y usa su poder para fortalecer tu cuerpo".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel4 = true;


    } //150 Enemigos, 50 Armadura (En una run)

    public void LVL5()
    {

        Mision.text = "Enemigos derrotados: ".ToUpper() + enemigosDerrotadosMartillo + " / 200 " +                 "Derrota al Payaso Jefe".ToUpper();
        DescripcionMision.text = "Has seguido todas mis instrucciones hasta el final, es hora de que aprendas mi movimiento estrella. ¡Vuelve una ultima vez y alzate con la victoria!".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel5 = true;



    }

    #endregion

    private void Update()
    {
        if (dañoRecibido >= 210)
        {
            DañoAlcanzado();
        }

        if (ArmaduraObtenida >= 50)
        {
            ObjetivoArmadura();
            Debug.Log(Objetivo1);
        }

    }

    #region misiones

    void DañoAlcanzado()
    {
        DañoConseguido = true;
    }

    void ObjetivoArmadura()
    {
        Objetivo1 = true;
    }

    #endregion

    public void SubirNivel()
    {
        if ((Nivel1 == true && Nivel1Conseguido == false) || godmode == true && Nivel1Conseguido == false)
        {

            if (enemigosDerrotadosMartillo >= 50 || godmode == true) //HACER QUE SEA CON EL ARMA
            {
                enemigosDerrotadosMartillo = 0;

                PlayerGM.maxHealth = PlayerGM.maxHealth + 20;
                PlayerGM.health = PlayerGM.maxHealth;

                vidaMartillo = PlayerGM.maxHealth;

                Nivel1Conseguido = true;
                NivelArma = 1;

                lvl2.SetActive(true);

                Mision.text = "";
                DescripcionMision.text = "Buen trabajo, novato".ToUpper();
                NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

                SubirDeNivel.SetActive(false);
            }


        }

        else if ((Nivel2 == true && Nivel2Conseguido == false) || godmode == true && Nivel2Conseguido == false)
        {

            if (enemigosDerrotadosMartillo >= 100 || godmode == true) //HACER QUE SEA CON EL ARMA
            {
                enemigosDerrotadosMartillo = 0;
                Nivel2 = true;

                PlayerGM.maxHealth = PlayerGM.maxHealth + 20;
                TE.hamerDamage = TE.hamerDamage + 5;
                PlayerGM.health = PlayerGM.maxHealth;

                dañoMartillo = TE.hamerDamage;
                vidaMartillo = PlayerGM.maxHealth;

                Nivel2Conseguido = true;
                NivelArma = 2;

                lvl3.SetActive(true);

                Mision.text = "";
                DescripcionMision.text = "Tu poder aumenta, sigue asi".ToUpper();
                NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

                SubirDeNivel.SetActive(false);
            }
        }

        else if ((Nivel3 == true && Nivel3Conseguido == false) || godmode == true && Nivel3Conseguido == false)
        {
            if ((enemigosDerrotadosMartillo >= 100 && DañoConseguido == true) || godmode == true) //210 VIDA
            {
                enemigosDerrotadosMartillo = 0;
                DañoConseguido = false;
                dañoRecibido = 0;
                Nivel3 = true;

                PlayerGM.maxHealth = PlayerGM.maxHealth + 20;
                PlayerGM.health = PlayerGM.maxHealth;
                GanoArmadura = true;

                vidaMartillo = PlayerGM.maxHealth;

                Nivel3Conseguido = true;
                NivelArma = 3;

                lvl4.SetActive(true);

                Mision.text = "";
                DescripcionMision.text = "Duro como la roca".ToUpper();
                NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

                SubirDeNivel.SetActive(false);
            }
        }

        else if ((Nivel4 == true && Nivel4Conseguido == false) || godmode == true && Nivel4Conseguido == false)
        {
            if ((enemigosDerrotadosMartillo >= 150 && Nivel4Conseguido == false && Objetivo1 == true) || godmode == true) //HACER QUE SEA CON EL ARMA
            {
                enemigosDerrotadosMartillo = 0;
                Nivel4 = true;

                PlayerGM.maxHealth = PlayerGM.maxHealth + 20;
                TE.hamerDamage = TE.hamerDamage + 5;

                dañoMartillo = TE.hamerDamage;
                vidaMartillo = PlayerGM.maxHealth;

                Nivel4Conseguido = true;
                NivelArma = 4;

                lvl5.SetActive(true);

                Mision.text = "";
                DescripcionMision.text = "Su vitalidad es tu fuerza".ToUpper();
                NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

                SubirDeNivel.SetActive(false);


            }
        }

        else if ((Nivel5 == true && Nivel5Conseguido == false) || godmode == true && Nivel5Conseguido == false)
        {
            if ((enemigosDerrotadosMartillo >= 200 && clw.PayasoMuerto == true && lvl5 == false) || godmode == true)
            {
                enemigosDerrotadosMartillo = 0;
                Nivel5 = true;

                PlayerGM.maxHealth = PlayerGM.maxHealth + 20;

                vidaMartillo = PlayerGM.maxHealth;


                UltiMartillo = true;

                Nivel5Conseguido = true;
                NivelArma = 5;

                Mision.text = "";
                DescripcionMision.text = "Has completado todas las misiones que tenia, ves y acaba con tus enemigos con nuestro poder".ToUpper();
                NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

                SubirDeNivel.SetActive(false);

            }
        }

    }

    public void GODMODE()
    {
        godmode = true;
    }

}
