using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(IA_STATS))]
[RequireComponent(typeof(NavMeshAgent))]
public class IA_RANGE : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    IA_STATS stats;
    Animator animator;

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
    #region Patrulla (no usar)
    [Min(0)]
    private float rangoDePatrulla;
    Vector3 destino;
    bool destinoMarcado;
    #endregion

    [Header("==Ataque==")]
    [Min(0)]
    public float rangoAtaque;
    [Min(0)]
    public float tiempoEntreAtaques;
    bool yaAtacado;
    [Space(1)]
    [Header("Ataque rango, modificar dependiendo del tipo de ataque")]
    public Transform proyectilOrigen;
    public GameObject proyectil;
    [Min(0)]
    public float fuerzaProyectil;
    [Header("Raycast")]
    float radioSphere = 0.5f;
    Vector3 raycastOrigen;
    Vector3 raycastDireccion;
    float currentHitDistance;

    [Header("==Estados==")]
    public bool playerEnVision;
    public bool playerEnRangoAtaque;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<IA_STATS>();
        animator = GetComponent<Animator>();

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
        animator.SetFloat("Speed", agent.velocity.magnitude);

        //¿Esta en el rango de vision y de ataque?
        playerEnVision = Physics.CheckSphere(transform.position, rangoVision, whatIsPlayer);
        playerEnRangoAtaque = Physics.CheckSphere(transform.position, rangoAtaque, whatIsPlayer);

        if (!playerEnVision && !playerEnRangoAtaque) Stay();
        if (playerEnVision && !playerEnRangoAtaque) ChasePlayer();
        if (playerEnVision && playerEnRangoAtaque) AttackPlayer();

        if (stats.golpeado == true)
        {
            stats.golpeado = false;

            agent.SetDestination(transform.position);

            if (stats.vida <= 0)
            {
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
                GameObject.Find("AAAAACTIVADOR DEL PUTO CANVAS").GetComponent<Bestiario>().GetKill("HAND SOLO");
                //Bestiario.Instance.GetKill("HAND SOLO");
                #endregion

                DestroyEnemy();
            }
        }
        else
        {
            //this.GetComponent<Renderer>().material = MaterialOriginal;
        }

        raycastDireccion = transform.forward;
        raycastOrigen = transform.position;
    }

    void Stay()
    {
        agent.SetDestination(transform.position);

        //StartCoroutine(RandomRotation());

        ///Animacion idle

        ///
    }
    IEnumerator RandomRotation()
    {
        this.transform.rotation = Random.rotation;
        yield return new WaitForSeconds(5);
    }

    #region Patrullar (no usar)
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
    #endregion

    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);

        rangoVision = 100f;
    }

    void AttackPlayer()
    {
        ///Smooth rotacion
        int rotationSpeed = 100;
        Vector3 _fixRotation = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        Quaternion playerRotation = Quaternion.LookRotation(_fixRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
        ///

        RaycastHit hit;
        if (Physics.SphereCast(raycastOrigen, radioSphere, raycastDireccion, out hit, rangoAtaque))
        {
            if (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("void"))
            {
                if (!yaAtacado)
                {
                    //No mover
                    agent.SetDestination(transform.position);

                    animator.SetTrigger("Attack");

                    ///Codigo ataque
                    StartCoroutine(Attack());
                    ///

                    yaAtacado = true;
                    Invoke(nameof(ResetAttack), tiempoEntreAtaques);
                }
            }
            if (hit.transform.gameObject.CompareTag("Obstaculo"))
            {
                ChasePlayer();
            }
        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.3f);
        Rigidbody rb = Instantiate(proyectil, proyectilOrigen.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(raycastDireccion * fuerzaProyectil, ForceMode.Impulse);
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
            ///Visual para el rango de ataque
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangoAtaque);

            ///Visual para el rango de vision
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoVision);

            #region Patrulla (no usar)
            ///Visual para el rango de movimiento
            //Gizmos.color = Color.blue;
            //Gizmos.DrawWireSphere(transform.position, rangoDePatrulla);

            ///Visual para marcar los destinos
            //Gizmos.DrawLine(transform.position, destino);
            #endregion
        }
    }
}
