using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IA_BOMBA : MonoBehaviour
{
    GameObject player;

    //Valores
    public float damage;
    public float rangoExplosion;
    public float tiempoExplosion;
    public float duracionExplosion;

    public LayerMask loQueEsPlayer;
    public bool golpeadoPlayer;

    public GameObject area;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        area.SetActive(false);

        Invoke(nameof(Explosion), tiempoExplosion);
    }

    void Explosion()
    {
        area.SetActive(true);
        golpeadoPlayer = Physics.CheckSphere(transform.position, rangoExplosion, loQueEsPlayer);

        if (golpeadoPlayer) player.GetComponent<PlayerGM>().TakeDamage(20);

        Invoke(nameof(DestroyBomb), duracionExplosion);
    }
    void DestroyBomb() 
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        ////Visual rango de explosion
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoExplosion);
    }
}
