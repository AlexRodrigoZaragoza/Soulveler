using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class IA_VISITANTES : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("==Layers==")]
    [Tooltip("Layer en la que esta el suelo")]
    public LayerMask whatIsGround;

    [Header("==Patrullar==")]
    [Min(0)]
    public float rangoVision;
    [Min(0)]
    public float rangoMovimiento;

    public bool gizmos;

    public bool destinoMarcado;
    public bool tomarDecision;
    Vector3 destino;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Patrolling();
    }

    private void Patrolling()
    {

        //xdddddd
        //if (!destinoMarcado && !tomarDecision)
        //{
        //    int random = Random.Range(0, 2);
        //    Debug.Log(random);

        //    if (random == 0)
        //    {
        //        tomarDecision = true;

        //        agent.SetDestination(this.transform.position);
        //        StartCoroutine(Stay());
        //    }
        //    else if (random == 1)
        //    {
        //        tomarDecision = true;

        //        if (!destinoMarcado) SearchWalkPoint();

        //        if (destinoMarcado)
        //        {
        //            agent.SetDestination(destino);
        //        }
        //    }
        //}

        //if (agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    // El enemigo ha llegado a su destino
        //    destinoMarcado = false;
        //    tomarDecision = false;
        //}



        if (!destinoMarcado) SearchWalkPoint();

        if (destinoMarcado)
        {
            agent.SetDestination(destino);
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // El enemigo ha llegado a su destino
            destinoMarcado = false;
        }
    }

    IEnumerator Stay()
    {
        yield return new WaitForSeconds(100f);
        tomarDecision = false;
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
            //destinoMarcado = true;
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
        }
    }
}
