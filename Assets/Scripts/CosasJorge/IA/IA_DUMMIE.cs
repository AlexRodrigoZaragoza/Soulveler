using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IA_DUMMIE : MonoBehaviour
{
    Animator animator;
    IA_STATS stats;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<IA_STATS>();
    }

    public void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Hit");

        if (stats.vida <= 0)
        {
            stats.vidaMax = 300;
            stats.vida = 300;
        }
    }
}
