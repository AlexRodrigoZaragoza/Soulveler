using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_PROYECTIL : MonoBehaviour
{
    public float vidaProyectil = 2f;
    public float daño;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(vidaProyectil);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerGM>().TakeDamage(daño);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemigo") && this.gameObject.name == "Arrow")
        {
            other.gameObject.GetComponent<IA_STATS>().TakeDamage(daño);
            Destroy(gameObject);
        }

    }
}
