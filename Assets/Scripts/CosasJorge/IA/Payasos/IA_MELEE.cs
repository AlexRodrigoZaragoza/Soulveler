using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(IA_STATS))]
[RequireComponent(typeof(NavMeshAgent))]
public class IA_MELEE : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    IA_STATS stats;
    Animator animator;
    Bestiario bestiario;

    public bool gizmos = true;

    [Header("==Stats==")]
    public float vida;
    public float danoAtaque;

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

    [Header("==Items==")]
    [Tooltip("Objeto que dropea al morir")]
    public GameObject drop;

    [Header("==Patrullar==")]
    [Min(0)]
    public float rangoVision;
    [Min(0)]
    private float rangoDePatrulla;
    Vector3 destino;
    bool destinoMarcado;

    [Header("==Ataque==")]
    [Min(0)]
    public float rangoAtaque;
    [Min(0)]
    public float tiempoEntreAtaques;
    bool yaAtacado;
    [Space(1)]
    [Header("Ataque rango, modificar dependiendo del tipo de ataque")]
    public GameObject _collider;
    public GameObject exclamacion;

    [Header("==Estados==")]
    public bool playerEnVision;
    public bool playerInAttackRange;


    float distance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<IA_STATS>();
        animator = GetComponent<Animator>();

        stats.vidaMax = vida;
        stats.danoAtaque = danoAtaque;
    }
    private void Start()
    {
        StartCoroutine(Spawn());

        
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

    private void FixedUpdate()
    {
        playerEnVision = Physics.CheckSphere(transform.position, rangoVision, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, rangoAtaque, whatIsPlayer);

        if (!playerEnVision && !playerInAttackRange) Stay();
        if (playerEnVision && !playerInAttackRange) ChasePlayer();
        if (playerEnVision && playerInAttackRange) AttackPlayer();
    }
    private void Update()
    {
        if (spawnenemy.oleadas == 2)
        {
            rangoVision = 200;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (stats.golpeado == true)
        {
            stats.golpeado = false;

            if (!playerEnVision) { ChasePlayer(); }

            animator.SetTrigger("Hitted");

            StartCoroutine(Stop());

            if (stats.vida <= 0)
            {
                #region ARZ - MAGIA
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
                GameObject.Find("AAAAACTIVADOR DEL PUTO CANVAS").GetComponent<Bestiario>().GetKill("A.JOHNSON");
                //Bestiario.Instance.GetKill("A.JOHNSON");
                #endregion

                DestroyEnemy();
            }
        }
    }

    void Stay()
    {
        agent.SetDestination(transform.position);

        ///Animacion idle

        ///
    }

    IEnumerator Stop()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);
        _collider.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        agent.isStopped = false;
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
        //A ver como soluciono el derrape xddddddd

        distance = Mathf.Abs((player.transform.position - this.transform.position).magnitude);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            agent.SetDestination(player.transform.position + (player.transform.position * distance));
        }
        else { agent.SetDestination(player.transform.position); }

        ///Smooth rotacion
        int rotationSpeed = 100;
        Vector3 _fixRotation = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        Quaternion playerRotation = Quaternion.LookRotation(_fixRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
        ///



        //destino = player.transform.position;

        rangoVision = 100f;
    }

    void AttackPlayer()
    {
        //No mover
        agent.SetDestination(transform.position);

        ///Smooth rotacion
        int rotationSpeed = 100;
        Vector3 _fixRotation = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        Quaternion playerRotation = Quaternion.LookRotation(_fixRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
        ///

        if (!yaAtacado)
        {
            animator.SetInteger("AttackIndex", Random.Range(0, 3));
            animator.SetTrigger("Attack");

            ///Codigo ataque
            StartCoroutine(Attack());
            ///

            yaAtacado = true;
            StartCoroutine(ResetAttack());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        exclamacion.SetActive(true);
        _collider.SetActive(true);
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1);
        _collider.SetActive(false);
        exclamacion.SetActive(false);
        yield return new WaitForSeconds(tiempoEntreAtaques);
        yaAtacado = false;
    }

    public void DestroyEnemy()
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
            ///Gizmos.DrawLine(transform.position, destino);
        }
    }
}
