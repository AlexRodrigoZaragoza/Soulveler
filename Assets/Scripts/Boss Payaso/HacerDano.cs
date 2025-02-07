using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDano : MonoBehaviour
{
    [SerializeField] float dano;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerGM>().TakeDamage(dano);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerGM>().TakeDamage(dano);
        }
    }
}
