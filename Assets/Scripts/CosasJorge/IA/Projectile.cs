using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerGM>().TakeDamage(5);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemigo")&&this.gameObject.name=="Arrow")
        {
            other.gameObject.GetComponent<IA_STATS>().TakeDamage(5);
            Destroy(gameObject);
        }

    }
    IEnumerator DestruirBala()
    {
        yield return new WaitForSeconds(5);
    }
}
