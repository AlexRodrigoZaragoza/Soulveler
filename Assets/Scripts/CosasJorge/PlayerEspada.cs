using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEspada : MonoBehaviour
{
    public Slider ArmaEspectral;
    int carga;

    private void Awake()
    {
        ArmaEspectral.value = 0;
        ArmaEspectral.maxValue = 100;
        carga = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Enemigo Golpeado 1");
            carga = carga + 5;

            ArmaEspectral.value = carga;



        }
    }
}
