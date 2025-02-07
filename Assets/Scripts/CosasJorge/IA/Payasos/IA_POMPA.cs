using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_POMPA : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Golpeado");

            TestingInputSystem.stop = true;
            IA_RANGE_POMPA.playerGolpeado = true;

            other.gameObject.GetComponent<PlayerGM>().TakeDamage(daño);

            DestroyProjectile();
        }
    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
