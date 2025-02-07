using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(IA_STATS))]
[RequireComponent(typeof(NavMeshAgent))]
public class IA_MARIONETISTA : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    IA_STATS stats;
    Animator animator;

    public bool gizmos = true;

    [Header("==Stats==")]
    public float vida;

    [Header("==Layers==")]
    [Tooltip("Layer en la que esta el suelo")]
    public LayerMask whatIsGround;
    [Tooltip("Layer en la que esta el personaje")]
    public LayerMask whatIsPlayer;
    [Tooltip("Layer en la que estan los muros y obstaculos que impiden los ataques de rango")]
    public LayerMask whatIsWall;

    //[Header("==Materiales==")]
    //[Tooltip("Material base del enemigo")]
    //public Material MaterialOriginal;
    //[Tooltip("Material al al ser golpeado")]
    //public Material MaterialGolpeado;

    [Header("==Drops==")]
    [Tooltip("Objeto que dropea al morir")]
    public GameObject drop;

    [Header("==Patrullar==")]
    [Min(0)]
    public float rangoVision;
    Vector3 destino;
    bool destinoMarcado;

    [Header("==Ataque==")]
    [Min(0)]
    public float rangoSpawn;
    [Min(0)]
    public float rangoAlarma;
    [Tooltip("Maximo de marionetas que va a spawnear, saldra un random entre 1 y el valor")]
    public int maxMarionetas;
    public int minMarionetas;
    [Tooltip("Tipos de marionetas que va a spawnear")]
    public List<GameObject> marionetas = new List<GameObject>();
    public List<GameObject> marionetasCreadas = new List<GameObject>();

    [Header("==Estados==")]
    public bool playerEnVision;
    public bool playerInAlarmaRange;
    bool primeraEnVision;
    Vector3 forwarDirection;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<IA_STATS>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stats.vidaMax = vida;

        primeraEnVision = false;

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
        //¿Esta en el rango de vision y de ataque?
        playerEnVision = Physics.CheckSphere(transform.position, rangoVision, whatIsPlayer);
        playerInAlarmaRange = Physics.CheckSphere(transform.position, rangoAlarma, whatIsPlayer);

        if (playerEnVision && !primeraEnVision) { SpawnMarionetas(); } 
        if (playerEnVision && playerInAlarmaRange) 
        {
            //SearchRunAway();
            Alarma();
        }
        forwarDirection = transform.forward;
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (stats.golpeado == true)
        {
            stats.golpeado = false;

            animator.SetTrigger("Hitted");

            StartCoroutine(Stop());

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
                GameObject.Find("AAAAACTIVADOR DEL PUTO CANVAS").GetComponent<Bestiario>().GetKill("AL.KAPONE");
                //Bestiario.Instance.GetKill("AL.KAPONE");
                #endregion

                DestroyEnemy();
            }
        }
    }
    IEnumerator Stop()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(2f);
        agent.isStopped = false;
    }

    private void SpawnMarionetas()
    {
        transform.LookAt(player.transform);

        //Calcular el numero de marionetas
        int marionetasNum = Random.Range(minMarionetas, maxMarionetas);

        for (int i = 0;i < marionetasNum; i++) 
        {
            //Calcular punto dentro del rango
            float randomZ = Random.Range(-rangoSpawn, rangoSpawn);
            float randomX = Random.Range(-rangoSpawn, rangoSpawn);

            Vector3 destinoMarioneta = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            GameObject marioneta = Instantiate(marionetas[Random.Range(0, marionetas.Count)], destinoMarioneta, Quaternion.identity);
            marionetasCreadas.Add(marioneta);
        }

        primeraEnVision = true;
    }

    void SearchRunAway()
    {
        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 newPos = transform.position + dirToPlayer;

        agent.SetDestination(newPos);
    }

    void Alarma()
    {
        Vector3 distanceToWalkPoint = transform.position - destino;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // El enemigo ha llegado a su destino
            SearchRunAway();
        }
    }

    public void DestroyEnemy()
    {
        if (SceneManager.GetActiveScene().name != "TimerDuel")
        {
            ///Codigo spawn objeto
            Instantiate(drop, transform.position, Quaternion.identity);
        }

        StartCoroutine(Destruir());

        //Codigo spawn objeto
        Instantiate(drop, transform.position, Quaternion.identity);
    }
    bool marionetasDestruidas()
    {
        for (int i = 0; i < marionetasCreadas.Count; i++)
        {
            DestroyImmediate(marionetasCreadas[i]);
        }
        return true;
    }
    IEnumerator Destruir()
    {
        while (!marionetasDestruidas())
        {
            yield return null;
        }

        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        if (gizmos == true)
        {
            //Visual para el rango de alarma
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangoAlarma);

            //Visual para el rango de vision
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoVision);

            //Visual para el rango de movimiento
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rangoSpawn);

            //Visual para marcar los destinos
            Gizmos.DrawLine(transform.position, destino);
        }
    }
}
