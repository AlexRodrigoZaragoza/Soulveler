using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    IA_STATS stats;

    [SerializeField] Transform target;
    NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject ataqueCollider;

    public float rangoVision;
    public float distanciaAtaque;
    [Range(0f, 360f)]
    public float anguloAtaque;

    public float Health;

    public LayerMask capaTarget;

    public bool detectado;
    public bool primerDetectado;
    public bool atacando;

    private const string IsWalking = "IsWalking";
    private const string IsAttacking = "IsAttacking";
    private const string IsHit = "IsHit";
    private const string IsDeath = "IsDeath";

    public GameObject area;
    public GameObject exclamacion;


    bool muerto = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        stats = GetComponent<IA_STATS>();
        stats.vidaMax = Health;
    }
    void Start()
    {
        exclamacion.SetActive(false);

        agent = GetComponent<NavMeshAgent>();
        ataqueCollider.SetActive(false);
    }

    void Update()
    {
        detectado = Physics.CheckSphere(transform.position, rangoVision, capaTarget);
        atacando = Physics.CheckSphere(transform.position, distanciaAtaque, capaTarget);

        Chase();
        Ataque();

        if (stats.golpeado == true)
        {
            animator.SetBool(IsHit, true);

            agent.SetDestination(transform.position);

            if (stats.vida <= 0)
            {
                this.GetComponent<CapsuleCollider>().enabled = false;
                target = transform;
                StartCoroutine(Die());
                animator.SetBool(IsDeath, true);
            }
        }
        else
        {
            animator.SetBool(IsHit, false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);

        //Nuevo metodo de detectar, en vez de con esferas con conos de vision pd: esta a medias son las 6 de la mañana no es buena idea irse tan tarde 
    //    if (anguloAtaque <= 0f) return;

    //    float halfAnguloAtaque = anguloAtaque * 0.5f;

    //    Vector2 p1, p2;

    //    p1 = PointForAngle(halfAnguloAtaque, distanciaAtaque);
    //    p2 = PointForAngle(-halfAnguloAtaque, distanciaAtaque);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + p1);
    //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + p2);

    //    //Gizmos.DrawRay(transform.position, transform.right * 4f);
    }
    //Vector3 PointForAngle(float angulo, float distancia)
    //{
    //    return new Vector2(Mathf.Cos(angulo * Mathf.Deg2Rad), Mathf.Sin(angulo * Mathf.Deg2Rad)) * distancia;
    //}

    void Chase()
    {
        animator.SetBool(IsAttacking, false);
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
            animator.SetBool(IsWalking, true);
            
        }
        else if (agent.velocity.magnitude < 0.1f)
        {
            animator.SetBool(IsWalking, false);
        }
    }
    IEnumerator ExclamacionOff()
    {
        yield return new WaitForSeconds(1f);
        exclamacion.SetActive(false);
    }

    void Ataque()
    {
        if (atacando == true)
        {
            area.SetActive(true);
            ataqueCollider.SetActive(true);
            animator.SetBool(IsAttacking, true);
            StartCoroutine(TerminaLaAnimacion());
        }
    }
    IEnumerator TerminaLaAnimacion()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(0.75f);
        area.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        agent.isStopped = false;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
