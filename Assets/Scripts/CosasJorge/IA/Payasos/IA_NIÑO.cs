using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class IA_NIÑO : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    IA_STATS stats;

    public bool gizmos = true;

    [Header("==Layers==")]
    [Tooltip("Layer en la que esta el suelo")]
    public LayerMask whatIsGround;
    [Tooltip("Layer en la que esta el personaje")]
    public LayerMask whatIsPlayer;
    [Tooltip("Layer en la que estan los muros o obstaculos")]
    public LayerMask whatIsWall;
    [Tooltip("Layer en la que estan los payasos")]
    public LayerMask whatIsPayaso;

    [Header("==Patrullar==")]
    [Min(0)]
    public float rangoVision;
    [Min(0)]
    public float rangoMovimiento;
    public Vector3 destino;
    bool destinoMarcado;

    [Header("Attacking")]
    bool yaAtacado;
    //public float tiempoEntreAtaque;
    public GameObject lagrimas;

    [Header("States")]
    public bool payasoEnRangoVision;
    public bool enMovimiento;
    public bool asustado;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        lagrimas.SetActive(false);
    }

    private void Update()
    {
        //Comprobar si el player esta en el rango de vision
        payasoEnRangoVision = Physics.CheckSphere(transform.position, rangoVision, whatIsPayaso);

        Patrolling();

        if (payasoEnRangoVision || asustado) Attack();
    }

    //Se tiene que quitar
    private void Patrolling()
    {
        if (!destinoMarcado) SearchWalkPoint();

        if (destinoMarcado)
        {
            agent.SetDestination(destino);
            enMovimiento = true;
        }

        if (agent.remainingDistance <= 1)
        {
            // El enemigo ha llegado a su destino
            //destinoMarcado = false;
            //enMovimiento = false;
            SearchWalkPoint();
        }
    }
    private void SearchWalkPoint()
    {
        //Calcular punto random en el rango
        float randomZ = Random.Range(-rangoMovimiento, rangoMovimiento);
        float randomX = Random.Range(-rangoMovimiento, rangoMovimiento);

        destino = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        NavMeshHit hit;
        if (Physics.Raycast(destino, -transform.up, 2f, whatIsGround))
        {
            destinoMarcado = true;
            if (NavMesh.SamplePosition(destino, out hit, rangoMovimiento, NavMesh.AllAreas))
            {
                // El punto está dentro del NavMesh, puedes mover el enemigo hacia él
                destino = hit.position;
                destinoMarcado = true;
            }
            else
            {
                destinoMarcado = false; SearchWalkPoint();
            }
        }
        else
        {
            destinoMarcado = false; SearchWalkPoint();
        }
    }
    private void Attack()
    {
        asustado = true;

        if (enMovimiento && !yaAtacado)
        {
            //Codigo ataque
            lagrimas.SetActive(true);
            //

            agent.speed = agent.speed = 15f;

            //yaAtacado = true;
            //Invoke(nameof(ResetAttack), tiempoEntreAtaque);
        }
    }
    private void ResetAttack()
    {
        yaAtacado = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (gizmos == true)
        {
            //Visual rango de movimiento
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rangoMovimiento);

            //Visual rango de vision
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoVision);

            //Visual linea al destino
            Gizmos.DrawLine(transform.position, destino);
        }
    }
}
