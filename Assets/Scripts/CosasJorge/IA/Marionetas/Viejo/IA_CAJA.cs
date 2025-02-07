using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IA_STATS))]
[RequireComponent(typeof(NavMeshAgent))]
public class IA_CAJA : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    IA_STATS stats;

    [Header("==Stats==")]
    public float vida;
    public float danoAtaque;

    [Header("==Layers==")]
    [Tooltip("Layer en la que esta el suelo")]
    public LayerMask whatIsGround;
    [Tooltip("Layer en la que esta el personaje")]
    public LayerMask whatIsPlayer;

    [Header("==Materiales==")]
    [Tooltip("Material base del enemigo")]
    public Material MaterialOriginal;
    [Tooltip("Material al al ser golpeado")]
    public Material MaterialGolpeado;

    [Header("==Drops==")]
    [Tooltip("Objeto que dropea al morir")]
    public GameObject drop;

    [Header("==Patrullar==")]
    [Min(0)]
    public float rangoVision;
    [Min(0)]
    public float rangoDePatrulla;
    Vector3 destino;
    bool destinoMarcado;

    [Header("==Ataque==")]
    [Min(0)]
    public float rangoAtaque;
    [Min(0)]
    public float tiempoEntreAtaques;
    bool yaAtacado;

    [Header("==Estados==")]
    public GameObject exclamacion;
    public bool playerEnVision;
    bool primeraEnVision;
    public bool playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<IA_STATS>();

        stats.vidaMax = vida;
        stats.danoAtaque = danoAtaque;

        exclamacion.SetActive(false);

        if (player == null) Debug.Log("No tag Player");
        if (stats == null) Debug.Log("No script stats");
    }
    private void FixedUpdate()
    {
        //¿Esta en el rango de vision y de ataque?
        playerEnVision = Physics.CheckSphere(transform.position, rangoVision, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, rangoAtaque, whatIsPlayer);

        if (!playerEnVision && !playerInAttackRange) Patroling();
        if (playerEnVision && !playerInAttackRange) ChasePlayer();
        if (playerEnVision && playerInAttackRange) AttackPlayer();

    }
    private void Update()
    {
        if (stats.golpeado == true)
        {
            this.GetComponent<Renderer>().material = MaterialGolpeado;

            agent.SetDestination(transform.position);

            if (stats.vida <= 0)
            {
                DestroyEnemy();
            }
        }
        else
        {
            this.GetComponent<Renderer>().material = MaterialOriginal;
        }
    }

    void Patroling()
    {
        if (!destinoMarcado) SearchWalkPoint();

        if (destinoMarcado) agent.SetDestination(destino);

        Vector3 distanceToWalkPoint = transform.position - destino;

        //WalkPont alcanzado
        if (distanceToWalkPoint.magnitude < 1f) destinoMarcado = false;
    }
    private void SearchWalkPoint()
    {
        //Calcular punto dendro del rango
        float randomZ = Random.Range(-rangoDePatrulla, rangoDePatrulla);
        float randomX = Random.Range(-rangoDePatrulla, rangoDePatrulla);

        destino = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Comprobar si el punto esta en una superficie
        if (Physics.Raycast(destino, -transform.up, 2f, whatIsGround)) destinoMarcado = true;
    }

    void ChasePlayer()
    {
        if (primeraEnVision == false)
        {
            exclamacion.SetActive(true);
            StartCoroutine(ExclamacionOff());

            rangoVision = 100f;

            primeraEnVision = true;
        }

        agent.SetDestination(player.transform.position);

        transform.LookAt(player.transform);

        destino = player.transform.position;
    }
    IEnumerator ExclamacionOff()
    {
        yield return new WaitForSeconds(1f);
        exclamacion.SetActive(false);
    }

    void AttackPlayer()
    {
        //No mover
        agent.SetDestination(transform.position);

        transform.LookAt(player.transform);


        if (!yaAtacado)
        {
            ///Codigo ataque
            player.GetComponent<PlayerGM>().TakeDamage(((int)stats.danoAtaque));
            ///

            print("hOLA");

            yaAtacado = true;
            Invoke(nameof(ResetAttack), tiempoEntreAtaques);
        }
    }
    private void ResetAttack()
    {
        yaAtacado = false;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);

        //Codigo spawn objeto
        Instantiate(drop, transform.position, Quaternion.identity);
    }


    private void OnDrawGizmosSelected()
    {
        //Visual para el rango de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);

        //Visual para el rango de vision
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);

        //Visual para el rango de movimiento
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoDePatrulla);

        //Visual para marcar los destinos
        Gizmos.DrawLine(transform.position, destino);
    }
}
