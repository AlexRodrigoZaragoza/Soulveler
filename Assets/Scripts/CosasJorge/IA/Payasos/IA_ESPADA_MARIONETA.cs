using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_ESPADA_MARIONETA : MonoBehaviour
{
    public int da�o;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerGM>().TakeDamage(da�o);
        }
    }
}
