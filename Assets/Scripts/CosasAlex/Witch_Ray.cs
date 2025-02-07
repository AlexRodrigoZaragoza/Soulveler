using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch_Ray : MonoBehaviour
{
    bool daño = false;
    bool cdInvulnerabilidad = false;
    private void OnTriggerStay(Collider other)
    {
            if (cdInvulnerabilidad == false && other.gameObject.tag == "Player")
            {
                StartCoroutine(TiempoInvulnerabilidad());
                other.gameObject.GetComponent<PlayerGM>().TakeDamage(20);
            }
        
    }



    IEnumerator TiempoInvulnerabilidad()
    {
        cdInvulnerabilidad = true;
        yield return new WaitForSeconds(0.5f);
        cdInvulnerabilidad = false;
    }
}
