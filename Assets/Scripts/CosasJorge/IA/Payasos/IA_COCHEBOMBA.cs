using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IA_COCHEBOMBA : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    IA_STATS stats;

    public bool gizmos = true;

    [Header("==Stats==")]
    public float vida;
    //public float danoAtaque;

    [Header("==Layers==")]
    [Tooltip("Layer en la que esta el suelo")]
    public LayerMask whatIsGround;
    [Tooltip("Layer en la que esta el personaje")]
    public LayerMask whatIsPlayer;
    [Tooltip("Layer en la que estan los muros o obstaculos")]
    public LayerMask whatIsWall;

    [Header("==Modelos==")]
    [Tooltip("Ruedas delanteras")]
    public Transform ruedasDelanteras_l;
    public Transform ruedasDelanteras_r;
    [Tooltip("Ruedas traseras")]
    public Transform ruedasTraseras_l;
    public Transform ruedasTraseras_r;

    [Header("==Drops==")]
    [Tooltip("Objeto que dropea al morir")]
    public GameObject drop;

    [Header("==Patrullar==")]
    [Min(0)]
    public float rangoVision;
    [Min(0)]
    public float rangoMovimiento;
    Vector3 destino;
    bool destinoMarcado;

    [Header("Attacking")]
    public float tiempoEntreAtaques;
    bool yaAtacado;
    public GameObject bomba;
    public GameObject origen;

    [Header("States")]
    public bool enMovimiento;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<IA_STATS>();

        stats.vidaMax = vida;
    }
    private void Start()
    {
        StartCoroutine(Spawn());

        if (spawnenemy.oleadas == 2)
        {
            rangoVision = 150;
        }
    }

    IEnumerator Spawn()
    {
        float rangoPrevio;
        rangoPrevio = rangoVision;

        rangoVision = 0.01f;
        this.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1f);
        rangoVision = rangoPrevio;
        this.GetComponent<Collider>().enabled = true;
    }

    private void Update()
    {
        ruedasDelanteras_l.Rotate(Vector3.back);
        ruedasDelanteras_r.Rotate(Vector3.back);
        ruedasTraseras_l.Rotate(Vector3.back);
        ruedasTraseras_r.Rotate(Vector3.back);

        Patrolling();

        if (enMovimiento) Attack();

        if (stats.golpeado == true)
        {
            stats.golpeado = false;

            if (stats.vida <= 0)
            {
                //Bestiario.Instance.GetKill("Coche Bomba");

                #region ARZ - MAGIA CURACION
                FindObjectOfType<TestingInputSystem>().canceled = false;
                FindObjectOfType<TestingInputSystem>().canAttack = true;
                MagiaCuración.curando = false;
                #endregion

                #region Miguel - POWER UP
                if (GameManager.ArmaActiva == 1)
                {
                    PowerUps_Martillo.enemigosDerrotadosMartillo++;
                }
                else if (GameManager.ArmaActiva == 2)
                {
                    PowerUps_Ballesta.enemigosDerrotadosBallesta++;
                }
                else if (GameManager.ArmaActiva == 3)
                {
                    PowerUps_Magia.enemigosDerrotadosMagia++;
                }
                #endregion

                #region DARIBU - BESTIARIO
                GameObject.Find("AAAAACTIVADOR DEL PUTO CANVAS").GetComponent<Bestiario>().GetKill("RUSHBY");
                //Bestiario.Instance.GetKill("RUSHBY");
                #endregion


                DestroyEnemy();
            }
        }
        else
        {
        }
    }
    void Stay()
    {
        agent.SetDestination(transform.position);

        //StartCoroutine(RandomRotation());

        ///Animacion idle

        ///
    }

    private void Patrolling()
    {
        //Una alternativa a que a veces se pueda mover y otras no 
        //if (!destinoMarcado)
        //{
        //    float random = Random.Range(1, 3);

        //    Debug.Log(random);

        //    if (random == 1) SearchWalkPoint();
        //    if (random == 2) { agent.SetDestination(transform.position); };
        //}

        if (!destinoMarcado) SearchWalkPoint();

        if (destinoMarcado)
        {
            agent.SetDestination(destino);
            enMovimiento = true;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // El enemigo ha llegado a su destino
            destinoMarcado = false;
            enMovimiento = false;
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
    private void Attack()
    {
        if (!yaAtacado)
        {
            //Codigo ataque
            Instantiate(bomba, origen.transform.position, Quaternion.identity);
            //

            yaAtacado = true;
            Invoke(nameof(ResetAttack), tiempoEntreAtaques);
        }
    }
    private void ResetAttack()
    {
        yaAtacado = false;
    }

    private void DestroyEnemy()
    {
        if (SceneManager.GetActiveScene().name != "TimerDuel")
        {
            ///Codigo spawn objeto
            Instantiate(drop, transform.position, Quaternion.identity);
        }

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
