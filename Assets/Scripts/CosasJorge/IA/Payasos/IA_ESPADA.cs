using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_ESPADA : MonoBehaviour
{
    private IA_STATS _stats;

    private void Awake()
    {
        _stats = this.GetComponentInParent<IA_STATS>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerGM>().TakeDamage(_stats.danoAtaque);
        }
    }
}
