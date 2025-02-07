using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.VFX;

public class Tienda : MonoBehaviour
{
    PlayerGM gm;
    Interaccion Interact;
    TestAttack TAtck;
    Colliders_Objetos col;

    int tricky;

    public Transform[] Posición;

    public GameObject[] ObjetosTienda;

    public bool[] objetostiendaseleccionados;

    public bool ObjetoVida, ObjetoVidaMax, ObjetoDaño, ObjetoESnoLimit, EspectralMax;

    public GameObject PanelDeTexto, CuadroInteracción;

    public TMP_Text DescripcionObjetos;

    public VisualEffect SmokePuf, SmokePufBad;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<PlayerGM>();
        Interact = FindObjectOfType<Interaccion>();
        TAtck = FindObjectOfType<TestAttack>();
        col = FindObjectOfType<Colliders_Objetos>();


        int i = 0;

        foreach (bool activado in objetostiendaseleccionados)
        {
            objetostiendaseleccionados[i] = false;
        }

        for (i = 0; i < 3; i++)
        {

            int n = Random.Range(0, ObjetosTienda.Length);

            while (objetostiendaseleccionados[n] == true)
            {
                n = Random.Range(0, ObjetosTienda.Length);

            }

            objetostiendaseleccionados[n] = true;

            Instantiate(ObjetosTienda[n], Posición[i].localPosition, Posición[i].localRotation);


        }


    }


    // Update is called once per frame

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && ObjetoVida == true)
        {
            ComprarObjetos();


        }
        if (Input.GetKeyDown(KeyCode.E) && ObjetoVidaMax == true)
        {
            ComprarObjetos();

        }
        if (Input.GetKeyDown(KeyCode.E) && ObjetoDaño == true)
        {
            ComprarObjetos();

        }
        if (Input.GetKeyDown(KeyCode.E) && ObjetoESnoLimit == true)
        {
            ComprarObjetos();

        }
        if (Input.GetKeyDown(KeyCode.E) && EspectralMax == true)
        {
            ComprarObjetos();

        }

    }

    void ComprarObjetos()
    {
        if (ObjetoVida == true)
        {
            if (PlayerGM.Tickets >= 50 && PlayerGM.health != PlayerGM.maxHealth) //50
            {
                PlayerGM.Tickets = PlayerGM.Tickets - 50;

                tricky = Random.Range(0, 10);


                if (tricky <= 3)
                {
                    if (PlayerGM.health > 25)
                    {
                        PlayerGM.health = PlayerGM.health - 25;
                    }
                    else
                    {
                        PlayerGM.health = PlayerGM.health - 24;
                    }

                    SmokePufBad.Play();
                }
                else
                {
                    PlayerGM.health = PlayerGM.health + 50;

                    SmokePuf.Play();
                }


                if (PlayerGM.health > PlayerGM.maxHealth)
                {
                    PlayerGM.health = PlayerGM.maxHealth;
                }


                CuadroInteracción.SetActive(false);
                PanelDeTexto.SetActive(false);
                //TextoVida.gameObject.SetActive(false);
                ObjetoVida = false;

                Destroy(GameObject.Find("Pocion(Clone)"));

            }

        } //CURACION

        if (ObjetoVidaMax == true)
        {
            if (PlayerGM.Tickets >= 50) //75
            {
                PlayerGM.Tickets = PlayerGM.Tickets - 50;

                tricky = Random.Range(0, 10);

                if (tricky <= 3)
                {

                    if (PlayerGM.maxHealth > 25)
                    {
                        PlayerGM.maxHealth = PlayerGM.maxHealth - 25;
                    }
                    else
                    {
                        PlayerGM.maxHealth = PlayerGM.maxHealth - 24;
                    }

                    gm.HealthBar.maxValue = PlayerGM.maxHealth;

                    if (PlayerGM.health > PlayerGM.maxHealth)
                    {
                        PlayerGM.health = PlayerGM.maxHealth;
                    }

                    SmokePufBad.Play();
                }

                else
                {
                    PlayerGM.maxHealth = PlayerGM.maxHealth + 50; //VALOR DE LA VIDA MAX

                    gm.HealthBar.maxValue = PlayerGM.maxHealth;

                    SmokePuf.Play();

                }


                CuadroInteracción.SetActive(false);
                PanelDeTexto.SetActive(false);
                //TextoVidaMax.gameObject.SetActive(false);
                ObjetoVidaMax = false;
                Destroy(GameObject.Find("MaxVida(Clone)"));
            }

        } //AUMENTO DE VIDA MAXIMA

        if (ObjetoDaño == true)
        {
            if (PlayerGM.Tickets >= 50) //100
            {
                PlayerGM.Tickets = PlayerGM.Tickets - 50;

                tricky = Random.Range(0, 10);

                if (tricky <= 3)
                {

                    TAtck.basicAttackDamage = TAtck.basicAttackDamage - 2.5f;

                    SmokePufBad.Play();
                }

                else
                {
                    TAtck.basicAttackDamage = TAtck.basicAttackDamage + 5;
                    SmokePuf.Play();
                }



                CuadroInteracción.SetActive(false);
                PanelDeTexto.SetActive(false);

                ObjetoDaño = false;
                Destroy(GameObject.Find("Daño(Clone)"));
            }


        } //AUMENTO DE DAÑO

        if (ObjetoESnoLimit == true)
        {
            if (PlayerGM.Tickets >= 50) //300
            {
                PlayerGM.Tickets = PlayerGM.Tickets - 50;


                tricky = Random.Range(0, 10);

                if (tricky == 0)
                {
                    SmokePufBad.Play();
                }

                else
                {
                    PlayerGM.Espectral = 100;
                    gm.Infinito = true;
                    SmokePuf.Play();
                }


                Destroy(GameObject.Find("Infinito(Clone)"));
            }
        } //ARMA ESPECTRAL ILIMITADA

        if (EspectralMax == true)
        {
            if (PlayerGM.Tickets >= 50) //100
            {
                PlayerGM.Tickets = PlayerGM.Tickets - 50;

                tricky = 1;

                if (tricky <= 3)
                {

                    gm.ArmaEspectral.maxValue = gm.ArmaEspectral.maxValue - 25;

                    if (PlayerGM.Espectral > gm.ArmaEspectral.maxValue)
                    {
                        PlayerGM.Espectral = gm.ArmaEspectral.maxValue;
                    }

                    if (gm.ArmaEspectral.maxValue <= 0)
                    {
                        gm.ArmaEspectral.maxValue = 0;
                    }

                    SmokePufBad.Play();
                }

                else
                {
                    gm.ArmaEspectral.maxValue = gm.ArmaEspectral.maxValue + 50;

                    PlayerGM.Espectral = PlayerGM.Espectral + 50;

                    SmokePuf.Play();

                }


                CuadroInteracción.SetActive(false);
                PanelDeTexto.SetActive(false);
                EspectralMax = false;

                Destroy(GameObject.Find("Espectral(Clone)"));
            }
        }


    }


}
