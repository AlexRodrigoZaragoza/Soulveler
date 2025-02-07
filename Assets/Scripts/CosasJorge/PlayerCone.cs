using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Enemigo Golpeado 1");


        }
    }
}
