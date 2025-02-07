using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUps_Ballesta : MonoBehaviour
{

    // Start is called before the first frame update
    TestAttack TE;                                              //DAÑOS
    TestingInputSystem TI;                                      //CDS
    PlayerGM PGM;                                               //COSAS DEL PERSONAJE

    public static int enemigosDerrotadosBallesta;
    float vidaBallesta = 100;
    float CdBallesta = 0.5f;
    float DañoBallesta = 20;

    public GameObject lvl2, lvl3, lvl4, lvl5, SubirDeNivel;

    public TMP_Text MisionBuzo, DescripcionMisionBuzo, NivelActual;

    int NivelArma = 0;

    bool Nivel1, Nivel2, Nivel3, Nivel4, Nivel5, godmode;

    bool Nivel1Conseguido, Nivel2Conseguido, Nivel3Conseguido, NIvel4Conseguido, Nivel5Conseguido;

    public bool UltiBallesta;


    private void Awake()
    {
        TE = FindObjectOfType<TestAttack>();
        TI = FindObjectOfType<TestingInputSystem>();
        PGM = FindObjectOfType<PlayerGM>();
    }
    public void Start()
    {

        PlayerGM.maxHealth = vidaBallesta;

        TI.CDballesta = CdBallesta;
        TE.ballestaDamage = DañoBallesta;


        if (PlayerGM.health > vidaBallesta)
        {
            PlayerGM.health = vidaBallesta;
        }

        NivelActual.text = "Nivel Actual: " + NivelArma;

        if (Nivel5Conseguido == true)
        {
            DescripcionMisionBuzo.text = "Has completado todas las misiones que tenia, ves y acaba con tus enemigos con nuestro poder".ToUpper();
        }
    }


    public void LVL1()
    {

        if (enemigosDerrotadosBallesta > 50) enemigosDerrotadosBallesta = 50;

        MisionBuzo.text = "Enemigos Derrotados: " + enemigosDerrotadosBallesta + " / 50";
        DescripcionMisionBuzo.text = "¿CREES TENER UNA BUENA PUNTERIA? VES Y DERROTA A UNOS ENEMIGOS PARA DEMOSTRAR QUE VALES LA PENA";

        SubirDeNivel.SetActive(true);
        Nivel1 = true;


    }
    public void LVL2()
    {
        if (enemigosDerrotadosBallesta > 100) enemigosDerrotadosBallesta = 100;

        MisionBuzo.text = "Enemigos Derrotados: " + enemigosDerrotadosBallesta + " / 100"; ;
        DescripcionMisionBuzo.text = "PARECE QUE NO ERES UN COMPLETO FRACASO, PERO AUN TE FALTA CAMINO POR RECORRER. CONTINUA DERROTANDO A TUS ENEMIGOS";

        SubirDeNivel.SetActive(true);
        Nivel2 = true;

    }
    public void LVL3()
    {
        if (enemigosDerrotadosBallesta > 150) enemigosDerrotadosBallesta = 150;

        MisionBuzo.text = "Enemigos Derrotados: " + enemigosDerrotadosBallesta + " / 150";
        DescripcionMisionBuzo.text = "TU NIVEL ESTA MEJORANDO MAS RAPIDO DE LO QUE ME ESPERABA, QUE TAL SI SALES A CAZAR A TUS ENEMIGOS Y LES DEMUESTRA QUIEN MANDA";

        SubirDeNivel.SetActive(true);
        Nivel3 = true;

    }
    public void LVL4()
    {
        if (enemigosDerrotadosBallesta > 200) enemigosDerrotadosBallesta = 200;

        MisionBuzo.text = "Enemigos Derrotados: " + enemigosDerrotadosBallesta + " / 200";
        DescripcionMisionBuzo.text = "tu manejo de la ballesta es sorprendente, podrias convertirte en una gran ballestera".ToUpper();

        SubirDeNivel.SetActive(true);
        Nivel4 = true;


    }
    public void LVL5()
    {
        if (enemigosDerrotadosBallesta > 250) enemigosDerrotadosBallesta = 250;

        MisionBuzo.text = "Enemigos Derrotados: " + enemigosDerrotadosBallesta + " / 200";
        DescripcionMisionBuzo.text = "HAS ALCANZADO EL MISMO NIVEL QUE TUVE YO EN VIDA, ES HORA DE QUE APRENDAS MI MOVIMIENTO ESTRELLA. REGRESA UNA ULTIMA VEZ Y ACABA CON TODOS.";

        SubirDeNivel.SetActive(true);
        Nivel5 = true;


    }

    public void SubirNivel()
    {
        if ((Nivel1 == true && Nivel1Conseguido == false) || godmode == true && Nivel1Conseguido == false)
        {
            if (enemigosDerrotadosBallesta >= 50 || godmode == true)
            {
                enemigosDerrotadosBallesta = 0;
                Nivel1 = true;

                TI.CDballesta = TI.CDballesta - 0.1f;
                CdBallesta = TI.CDballesta;

                Nivel1Conseguido = true;
                NivelArma = 1;

                lvl2.SetActive(true);

                MisionBuzo.text = "";
                DescripcionMisionBuzo.text = "BIEN HECHO, NOVATO.";
                NivelActual.text = "Nivel Actual: " + NivelArma;

                SubirDeNivel.SetActive(false);


            }
        }
        if ((Nivel2 == true && Nivel2Conseguido == false) || godmode == true && Nivel2Conseguido == false)
        {
            if (enemigosDerrotadosBallesta >= 100 || godmode == true)
            {
                enemigosDerrotadosBallesta = 0;


                PlayerGM.maxHealth = PlayerGM.maxHealth + 10;
                TE.ballestaDamage = TE.ballestaDamage + 5;

                vidaBallesta = PlayerGM.maxHealth;
                DañoBallesta = TE.ballestaDamage;

                Nivel2Conseguido = true;
                Nivel2 = true;

                lvl3.SetActive(true);

                MisionBuzo.text = "";
                DescripcionMisionBuzo.text = "Tu punteria mejora, sigue asi".ToUpper();
                NivelActual.text = "Nivel Actual: " + NivelArma;

                SubirDeNivel.SetActive(false);

            }
        }
        if ((Nivel3 == true && Nivel3Conseguido == false) || godmode == true && Nivel3Conseguido == true)
        {
            if (enemigosDerrotadosBallesta >= 150 || godmode == true)
            {
                enemigosDerrotadosBallesta = 0;
                Nivel3 = true;

                TE.ballestaDamage = TE.ballestaDamage + 5;

                DañoBallesta = TE.ballestaDamage;

                Nivel3Conseguido = true;
                NivelArma = 3;

                lvl4.SetActive(true);

                MisionBuzo.text = "";
                DescripcionMisionBuzo.text = "Tus disparos certeros siembran el terror entre los enemigos, vas por buen camino".ToUpper();
                NivelActual.text = "Nivel Actual: " + NivelArma;

                SubirDeNivel.SetActive(false);
            }
        }
        if ((Nivel4 == true && NIvel4Conseguido == false) || godmode == true && NIvel4Conseguido == false)
        {
            if (enemigosDerrotadosBallesta >= 200 || godmode == true)
            {
                enemigosDerrotadosBallesta = 0;
                Nivel4 = true;

                TE.ballestaDamage = TE.ballestaDamage + 5;
                DañoBallesta = TE.ballestaDamage;

                NIvel4Conseguido = true;
                NivelArma = 4;

                lvl5.SetActive(true);

                MisionBuzo.text = "";
                DescripcionMisionBuzo.text = "TU PUNTERIA ES MORTAL, SABES EVITAR A TUS ENEMIGOS Y LES SORPRENDES CON TU MANEJO DE LA BALLESTA, ESTAS MUY CERCA DE ALCANZAR MI NIVEL.";
                NivelActual.text = "Nivel Actual: " + NivelArma;

                SubirDeNivel.SetActive(false);

            }
        }
        if ((Nivel5 == true && Nivel5Conseguido == false) || godmode == true && Nivel5Conseguido == false)
        {
            if ((enemigosDerrotadosBallesta >= 250 && Nivel5 == false) || godmode == true)
            {
                enemigosDerrotadosBallesta = 0;
                Nivel5 = true;

                UltiBallesta = true;

                TE.ballestaDamage = TE.ballestaDamage + 5;
                DañoBallesta = TE.ballestaDamage;

                Nivel5Conseguido = true;
                NivelArma = 5;

                MisionBuzo.text = "";
                DescripcionMisionBuzo.text = "Has completado todas las misiones que tenia, ves y acaba con tus enemigos con nuestro poder".ToUpper();
                NivelActual.text = "Nivel Actual: " + NivelArma;

                SubirDeNivel.SetActive(false);

            }
        }
  
    }

    public void GODMODE()
    {
        godmode = true;
    }


}
