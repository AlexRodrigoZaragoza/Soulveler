using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch_Meteorite : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerGM>().TakeDamage(35);
        }
        if (other.gameObject.name == "meteoriteArea(Clone)")
        {
            Invoke("DestroyMeteorite",1);
            Destroy(other.gameObject,0.5f);
        }
    }

    void DestroyMeteorite()
    {
        Destroy(gameObject);
    }
}
