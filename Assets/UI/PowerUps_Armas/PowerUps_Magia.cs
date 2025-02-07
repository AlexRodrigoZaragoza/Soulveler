using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUps_Magia : MonoBehaviour
{

    public static int enemigosDerrotadosMagia;

    TestAttack TE;                                              //DAÑOS
    TestingInputSystem TI;                                      //CDS
    PlayerGM PGM;                                               //COSAS DEL PERSONAJE


    float VidaMago = 100;
    float dañoMago = 3;
    float dañoDemonio = 25;

    public GameObject lvl2, lvl3, lvl4, lvl5, SubirDeNivel;

    bool Nivel1, Nivel2, Nivel3, Nivel4, Nivel5, godmode;

    bool Nivel1Conseguido, Nivel2Conseguido, Nivel3Conseguido, Nivel4Conseguido, Nivel5Conseguido;

    public TMP_Text MisionMago, DescripcionMisionMago, NivelActual;

    int NivelArma = 0;


    private void Awake()
    {
        TE = FindObjectOfType<TestAttack>();
        TI = FindObjectOfType<TestingInputSystem>();
        PGM = FindObjectOfType<PlayerGM>();
    }

    public void Start()
    {
        PlayerGM.maxHealth = VidaMago;
        TE.magiaDamage = dañoMago;
        TE.demonDamage = dañoDemonio;

        if (PlayerGM.health < VidaMago || PlayerGM.health > VidaMago)
        {
            PlayerGM.health = VidaMago;
        }

        NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

        if (Nivel5Conseguido == true)
        {
            DescripcionMisionMago.text = "Has completado todas las misiones que tenia, ves y acaba con tus enemigos con nuestro poder".ToUpper();
        }

    }

    public void LVL1()
    {
        if (enemigosDerrotadosMagia > 50) enemigosDerrotadosMagia = 50;

        MisionMago.text = "Enemigos Derrotados: ".ToUpper() + enemigosDerrotadosMagia + " / 50";
        DescripcionMisionMago.text = "Mago: No deberias hablar conmigo, no sabes en que te estas metiendo".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel1 = true;

    } //50 Enemigos

    public void LVL2()
    {
        if (enemigosDerrotadosMagia > 100) enemigosDerrotadosMagia = 100;

        MisionMago.text = "Enemigos Derrotados: ".ToUpper() + enemigosDerrotadosMagia + " / 100";
        DescripcionMisionMago.text = "Mago: No deberias escuchar a mi otro yo, solo te traera problemas".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel2 = true;

    } //100 Enemigos

    public void LVL3()
    {
        if (enemigosDerrotadosMagia > 150) enemigosDerrotadosMagia = 150;

        MisionMago.text = "Enemigos Derrotados: ".ToUpper() + enemigosDerrotadosMagia + " / 150";
        DescripcionMisionMago.text = "Mago: Cuanto mas nos fortalezcas, mas nos adentraremos en tu alma".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel3 = true;

    } //100 Enemigos, 210 Daño recibido;

    public void LVL4()
    {
        if (enemigosDerrotadosMagia > 200) enemigosDerrotadosMagia = 200;

        MisionMago.text = "Enemigos Derrotados: ".ToUpper() + enemigosDerrotadosMagia + " / 200";
        DescripcionMisionMago.text = "Mago: A cada paso que das, tu humanidad se desvanece. ¡No te lo advertire mas!".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel4 = true;
    }

    public void LVL5()
    {
        if (enemigosDerrotadosMagia > 250) enemigosDerrotadosMagia = 250;

        MisionMago.text = "Enemigos Derrotados: ".ToUpper() + enemigosDerrotadosMagia + " / 250";
        DescripcionMisionMago.text = "Seras ingenuo! eres una incosciente, te ha manipulado completamente y has caido en sus garras. ¡Te has condenado!".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel5 = true;


    }

    public void SubirNivel()
    {
        if ((Nivel1 == true && Nivel1Conseguido == false) || godmode == true && Nivel1Conseguido == false)
        {
            Debug.Log("NEPE");

            if (enemigosDerrotadosMagia >= 50) //HACER QUE SEA CON EL ARMA
            {
                enemigosDerrotadosMagia = 0;
                Nivel1 = true;

                PlayerGM.maxHealth = PlayerGM.maxHealth + 20;
                PlayerGM.health = PlayerGM.maxHealth;
                PGM.HealthBar.maxValue = PlayerGM.maxHealth;

                VidaMago = PlayerGM.maxHealth;

                Nivel1Conseguido = true;
                NivelArma = 1;

                lvl2.SetActive(true);

                MisionMago.text = "";
                DescripcionMisionMago.text = "Demonio: Asi que te crees digno de jugar con poderes que se escapan a tu comprension eh?".ToUpper();
                NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

                SubirDeNivel.SetActive(false);

            }
        }

        if ((Nivel2 == true && Nivel2Conseguido == false) || godmode == true && Nivel2Conseguido == false)
        {
            if (enemigosDerrotadosMagia >= 100) //HACER QUE SEA CON EL ARMA
            {
                enemigosDerrotadosMagia = 0;
                Nivel2 = true;

                PlayerGM.maxHealth = PlayerGM.maxHealth + 20;
                PlayerGM.health = PlayerGM.maxHealth;
                PGM.HealthBar.maxValue = PlayerGM.maxHealth;

                TE.magiaDamage = TE.magiaDamage + 1;

                VidaMago = PlayerGM.maxHealth;
                dañoMago = TE.magiaDamage;

                Nivel2Conseguido = true;
                NivelArma = 2;

                lvl3.SetActive(true);

                MisionMago.text = "";
                DescripcionMisionMago.text = "Demonio: Que sigas aqui demuestran tus ganas de poder, no escuches a mi otro yo y sigue fortaleciendome".ToUpper();
                NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

                SubirDeNivel.SetActive(false);

            }
        }
        if ((Nivel3 == true && Nivel3Conseguido == false) || godmode == true && Nivel3Conseguido == false)
        {
            if (enemigosDerrotadosMagia >= 150)
            {
                enemigosDerrotadosMagia = 0;
                Nivel3 = true;

                PlayerGM.maxHealth = PlayerGM.maxHealth + 20;
                PlayerGM.health = PlayerGM.maxHealth;
                PGM.HealthBar.maxValue = PlayerGM.maxHealth;

                VidaMago = PlayerGM.maxHealth;

                lvl4.SetActive(true);

                MisionMago.text = "";
                DescripcionMisionMago.text = "Demonio: Somos cada vez mas fuertes, no es una sensacion... ¿increible?, ¡obten mas poder!".ToUpper();
                NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

                SubirDeNivel.SetActive(false);
            }
        }
        if ((Nivel4 == true && Nivel4Conseguido == false) || godmode == true && Nivel4Conseguido == false)
        {

            if (enemigosDerrotadosMagia >= 200) //HACER QUE SEA CON EL ARMA
            {
                enemigosDerrotadosMagia = 0;
                Nivel4 = true;

                PlayerGM.maxHealth = PlayerGM.maxHealth + 20;
                TE.demonDamage = TE.demonDamage + 15;
                PGM.HealthBar.maxValue = PlayerGM.maxHealth;

                dañoDemonio = TE.demonDamage;
                VidaMago = PlayerGM.maxHealth;

                lvl5.SetActive(true);

                Nivel4Conseguido = true;
                NivelArma = 4;

                MisionMago.text = "";
                DescripcionMisionMago.text = "Demonio: Si... ¡SI!, ya casi estoy completo, deshazte del otro, y te otorgare un poder que no puedes imaginar".ToUpper();
                NivelActual.text = "Nivel Actual: ".ToUpper() + NivelArma;

                SubirDeNivel.SetActive(false);

            }
        }
        if ((Nivel5 == true && Nivel5Conseguido == false) || godmode == true && Nivel5Conseguido == false)
        {
            enemigosDerrotadosMagia = 0;
            Nivel5 = true;

            PlayerGM.maxHealth = PlayerGM.maxHealth + 20;
            TE.demonDamage = TE.demonDamage + 15;
            TE.magiaDamage = TE.magiaDamage + 1;

            dañoDemonio = TE.demonDamage;
            VidaMago = PlayerGM.maxHealth;
            dañoMago = TE.magiaDamage;

            Nivel5Conseguido = true;
            NivelArma = 5;
            PGM.HealthBar.maxValue = PlayerGM.maxHealth;

            MisionMago.text = "";
            DescripcionMisionMago.text = "Demonio: NEIB YUM OLRASAP A SOMAV OY Y UT, OTELPMOC ATSE REDOP IM".ToUpper();
            NivelActual.text = "Nivel Actual: " + NivelArma;

            SubirDeNivel.SetActive(false);

        }


    }

    public void GODMODE()
    {
        godmode = true;
    }

}
