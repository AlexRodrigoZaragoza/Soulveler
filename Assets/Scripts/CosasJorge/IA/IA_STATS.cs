using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IA_STATS : MonoBehaviour
{
    [Min(0)]
    public float vida;
    [Min(0)]
    public float vidaMax;
    [Min(0)]
    public float danoAtaque;

    public bool golpeado;

    public Slider BarraVida;
    public Canvas Canvas_IA;

    public bool Invulnerabilidad = false;

    bool cerrado;

    rayConexion rConexion;
    TestingInputSystem tis;

    public void Start()
    {
        vida = vidaMax;
        
        BarraVida.minValue = 0;
        BarraVida.maxValue = vidaMax;

        BarraVida.value = vidaMax;
        tis = FindObjectOfType<TestingInputSystem>();
        rConexion = FindObjectOfType<rayConexion>();
    }
    public void TakeDamage(float damage)
    {
        golpeado = true;
        vida -= damage;
        BarraVida.gameObject.SetActive(true);
        BarraVida.value = vida;
        StartCoroutine(Desactivar());

        if (vida <= 0&&cerrado==false)
        {
            rConexion.chupandoVida = false;
            tis.anim.SetBool("Chuclar", false);
            cerrado = true;
            spawnenemy.enemigos_derrotados++;
            Debug.Log(spawnenemy.enemigos_derrotados);
        }
    }


    IEnumerator Desactivar()
    {
        Invulnerabilidad = true; //La trampa no haría daño si el jugador le golpea
        yield return new WaitForSeconds(0.5f);
        golpeado = false;
        Invulnerabilidad = false;

    }
}
