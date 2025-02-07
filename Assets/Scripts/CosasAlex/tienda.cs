using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tienda : MonoBehaviour
{
    public GameObject press;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.gameObject.name == "espada")
            {
            }
            if (this.gameObject.name == "escudo")
            {
            }

            press.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            press.SetActive(false);

        }
    }
}
