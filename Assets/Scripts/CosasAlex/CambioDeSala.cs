using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeSala : MonoBehaviour
{
    public GameObject entrada, pared,clown;

    private void Start()
    {
        entrada.SetActive(false);

    }
    void Update()
    {
        if (clown.gameObject.GetComponent<Clown>().muerto == true)
        {
            entrada.SetActive(true);
            pared.SetActive(false);
        }
       
    }
}
