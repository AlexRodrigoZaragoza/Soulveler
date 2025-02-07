using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_MARIONETA_MELEE : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject ataqueCollider;

    public float rangoVision;
    public float distanciaAtaque;

    public LayerMask capaTarget;

    public bool detectado;
    public bool primerDetectado;
    public bool atacando;
    bool yaAtacado;
    public float tiempoEntreAtaques;

    public GameObject exclamacion;

    bool muerto = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        exclamacion.SetActive(false);

        agent = GetComponent<NavMeshAgent>();
        ataqueCollider.SetActive(false);
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        detectado = Physics.CheckSphere(transform.position, rangoVision, capaTarget);
        atacando = Physics.CheckSphere(transform.position, distanciaAtaque, capaTarget);
        if (!atacando)
        {
            animator.ResetTrigger("Attack");
        }

        Chase();
        Ataque();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
    }

    void Chase()
    {
        if (detectado == true)
        {
            if (primerDetectado == false)
            {
                exclamacion.SetActive(true);
                StartCoroutine(ExclamacionOff());

                rangoVision = 40;

                primerDetectado = true;
            }

            agent.SetDestination(target.position);
        }
    }
    IEnumerator ExclamacionOff()
    {
        yield return new WaitForSeconds(1f);
        exclamacion.SetActive(false);
    }

    void Ataque()
    {
        if (atacando == true && !yaAtacado)
        {
            animator.SetInteger("AttackIndex", Random.Range(0, 3));
            animator.SetTrigger("Attack");

            StartCoroutine(Attack());

            yaAtacado = true;
            StartCoroutine(ResetAttack());
            StartCoroutine(Stop());
        }
    }
    IEnumerator Stop()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);
        agent.isStopped = false;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        ataqueCollider.SetActive(true);
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
        ataqueCollider.SetActive(false);
        yield return new WaitForSeconds(tiempoEntreAtaques);
        yaAtacado = false;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
