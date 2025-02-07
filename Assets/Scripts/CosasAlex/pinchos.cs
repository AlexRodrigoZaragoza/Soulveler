using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinchos : MonoBehaviour
{
    bool pinchos_fuera=false;
    public bool da�o = false;
    bool cdInvulnerabilidad=false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (pinchos_fuera == false)
            {
                StartCoroutine(ataque_pincho());
            }
        }

        if (other.gameObject.tag == "Enemigo")
        {
            if (da�o == true)
            {
                other.gameObject.GetComponent<IA_STATS>().TakeDamage(5);
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        //hecho para da�ar a todo lo que pase una vez activado
        if (da�o == true)
        {
            //aqu� ir�a lo de abajo con el tag del enemigo

            if (cdInvulnerabilidad == false&&other.gameObject.tag=="Player")
            {
                StartCoroutine(TiempoInvulnerabilidad());
                other.gameObject.GetComponent<PlayerGM>().TakeDamage(5);
            }
            if (other.gameObject.tag == "Enemigo"&&other.gameObject.GetComponent<IA_STATS>().Invulnerabilidad == false)
            {
                other.gameObject.GetComponent<IA_STATS>().TakeDamage(5);
            }
        }
    }



    IEnumerator ataque_pincho()
    {
        pinchos_fuera = true;
        yield return new WaitForSeconds(0.2f);

        //aqu� meter animaci�n de pinchos saliendo
        GetComponent<Animator>().speed = 1;
        GetComponent<Animator>().SetBool("pinchos", true);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetBool("pinchos", false);
        da�o = false;  
        //aqu� animaci�n de pinchos ocult�ndose
        pinchos_fuera = false;
    }

    
    IEnumerator TiempoInvulnerabilidad()
    {
        cdInvulnerabilidad = true;
        yield return new WaitForSeconds(0.5f);
        cdInvulnerabilidad = false;
    }
}
