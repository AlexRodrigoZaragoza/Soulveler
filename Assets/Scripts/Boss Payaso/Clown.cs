using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Clown : MonoBehaviour
{
    [SerializeField] UnityEvent Flower;
    [SerializeField] UnityEvent Boxes;
    [SerializeField] UnityEvent Baloons;
    [SerializeField] UnityEvent Cars;

    [SerializeField] Transform target;
    public Vector3 posTarget;
    NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject ataqueCollider;

    public float rangoVision;
    public float rangoAtaque;

    public LayerMask capaTarget;

    public bool detectado;
    public bool atacando = false;

    public bool PayasoMuerto;

    public int maxHealth = 400;
    public static int clownCurrentHealth;

    private const string IsWalking = "IsWalking";
    private const string IsAttacking = "IsAttacking";
    private const string IsHit = "IsHit";
    private const string IsDeath = "IsDeath";

    public GameObject area;

    public bool muerto = false;
    bool dieOnce = true;

    PowerUps_Martillo PWM;
    PlayerGM PGM;

    //Para Payaso
    int SelectRandom;
    float tiempoAtt;
    bool mirame = true;
    [SerializeField] GameObject normalAtt1, normalAtt2; //ataque normal
    [SerializeField] ParticleSystem psNormalAtt2;

    //ataque cajas
    bool heAtacado = false;
    public Transform max_LeftTop;
    public Transform max_RightBot;

    [SerializeField] Transform hiddenPos, comebackPos;
    float startTimer;

    public static bool Endattacking = false;

    [SerializeField] bool once1 = true, once2 = true, once3 = true;
    public static bool onCryAttack = false, startGame;

    bool invencibility = false;

    [SerializeField] Slider sl;
    [SerializeField] IA_STATS mySTATS;
    [SerializeField] Vector3 zThrow;
    public static Vector3 flowerPos;

    [SerializeField] GameObject puente, hammer;


    [SerializeField] GameObject allObjects, ascensor;

    void Start()
    {
        startGame = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sl.maxValue = maxHealth;
        sl.value = clownCurrentHealth;
        clownCurrentHealth = maxHealth;
        startTimer = 0;
        agent = GetComponent<NavMeshAgent>();
        //ataqueCollider.SetActive(false);

        PWM = FindObjectOfType<PowerUps_Martillo>();
        PGM = FindObjectOfType<PlayerGM>();

    }

    void Update()
    {
        if (!startGame)
        {
            target.gameObject.GetComponent<TestingInputSystem>().enabled = false;
            target.gameObject.GetComponent<PlayerInput>().enabled = false;
            target.gameObject.GetComponent<Animator>().SetBool("Corriendo", false);
            target.gameObject.GetComponent<Rigidbody>().mass = 1000;
            timer += Time.deltaTime;
            if (timer >= 20)
            {
                target.gameObject.GetComponent<TestingInputSystem>().enabled = true;
                target.gameObject.GetComponent<PlayerInput>().enabled = true;
                target.gameObject.GetComponent<Rigidbody>().mass = 1;
                startGame = true;
            }
        }
        else if (startGame && !muerto)
        {
            //sl.value = clownCurrentHealth;
            posTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
            atacando = Physics.CheckSphere(transform.position, rangoAtaque, capaTarget);
            if (mirame)
            {
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            }
            if (transform.position == posTarget)
            {
                //animator.SetBool(IsWalking, false);
            }
            //habilidades super
            if (!heAtacado)
            {
                if (mySTATS.vida <= mySTATS.vidaMax / 1.5 && once1)
                {
                    animator.SetBool("Moving", false);
                    CryAbilities(1);
                }
                else if (mySTATS.vida <= mySTATS.vidaMax / 2 && once2)
                {
                    animator.SetBool("Moving", false);
                    CryAbilities(2);
                }
                else if (mySTATS.vida <= mySTATS.vidaMax / 2.5 && once3)
                {
                    animator.SetBool("Moving", false);
                    CryAbilities(3);
                }
                else if (atacando)
                {
                    animator.SetBool("Moving", false);
                    Attack();
                }
                Chase();
            }

            if (Endattacking) WalkAgain();

            if (mySTATS.vida <= 0)
            {
                //die

                if (PWM.lvl4 == true && PlayerGM.armas == 1)
                {
                    PayasoMuerto = true;
                }

                muerto = true;
                animator.SetTrigger("DEAD");

                StartCoroutine(Die());

            }
        }
    }

    public void openDoor()
    {
        puente.GetComponent<Animator>().SetTrigger("Appear");
    }

    void Attack()
    {
        transform.LookAt(target); //se quede mirando al punto donde estaba el personaje
        agent.Stop(); //se quede en su sitio
        heAtacado = true; //para que no se active otro ataque mientras hay uno en curso

        mirame = false;
        SelectRandom = Random.Range(0, 11);

        if (once1)
        {
            if (SelectRandom <= 6)
            {
                Invoke("BasicAtt_01", 0.2f);
            }
            else if (SelectRandom <= 9)
            {
                Invoke("BasicAtt_02", 0.2f);
            }
            else if (SelectRandom == 10)
            {
                Invoke("sendBoxes", 0.2f);
            }
        }
        else if (once2)
        {
            if (SelectRandom <= 5)
            {
                Invoke("BasicAtt_01", 0.1f);
            }
            else if (SelectRandom <= 9)
            {
                Invoke("BasicAtt_02", 0.1f);
            }
            else if (SelectRandom == 10)
            {
                Invoke("sendBoxes", 0.1f);
            }
        }
        else if (once3)
        {
            if (SelectRandom <= 4)
            {
                Invoke("BasicAtt_01", 0.1f);
            }
            else if (SelectRandom <= 8)
            {
                Invoke("BasicAtt_02", 0.1f);
            }
            else if (SelectRandom <= 10)
            {
                Invoke("sendBoxes", 0.1f);
            }
        }
        else
        {
            if (SelectRandom <= 3)
            {
                BasicAtt_01();
            }
            else if (SelectRandom <= 7)
            {
                BasicAtt_02();
            }
            else if (SelectRandom <= 10)
            {
                sendBoxes();
            }
        }
    }

    void BasicAtt_01()
    {
        tiempoAtt = 0;
        animator.SetTrigger("BasicAttack");
        //golpe en cono
    }

    public void QuitBasicAtt_01()
    {
        if (!onCryAttack) Endattacking = true;

    }

    void BasicAtt_02()
    {
        tiempoAtt = 0;
        //golpe onda
        animator.SetTrigger("CircleAttack");
    }
    public void QuitBasicAtt_02()
    {
        CancelInvoke();
        if (!onCryAttack) Endattacking = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }

    void Chase()
    {
        //animator.SetBool(IsAttacking, false);
        animator.SetBool("Moving", true);
        agent.SetDestination(target.position);
        //animator.SetBool(IsWalking, true);
    }


    void CryAbilities(float stage)
    {
        agent.Stop();
        mirame = false;
        onCryAttack = true;
        heAtacado = true;
        if (stage == 1)
        {
            agent.Stop(); //se quede en su sitio
            invencibility = true;
            sendFlower();

            tiempoAtt = 15;
            //Cajas
            once1 = false;

        }
        else if (stage == 2)
        {
            agent.Stop(); //se quede en su sitio
            invencibility = true;
            animator.SetTrigger("Baloon");
            //Invoke("sendBaloons", 2);
            //Invoke("Hide", 6);
            tiempoAtt = 30;
            //Flor
            once2 = false;
        }
        else if (stage == 3)
        {
            agent.Stop(); //se quede en su sitio
            invencibility = true;
            animator.SetTrigger("Car");
            tiempoAtt = 20;
            //coches
            once3 = false;
        }

    }

    public void Hide()
    {
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        hammer.SetActive(false);
        //transform.position = hiddenPos.position;
    }

    void sendBoxes()
    {
        onCryAttack = true;
        agent.Stop();
        animator.SetTrigger("JumpOnBox");

    }
    public void BoxesSent()
    {
        Boxes.Invoke();
    }
    public void sendFlower()
    {
        flowerPos = new Vector3(zThrow.x, transform.position.y, target.position.z);
        transform.LookAt(flowerPos);
        animator.SetTrigger("Flower");

    }
    public void FlowerSent()
    {
        Flower.Invoke();
    }
    public void sendCars()
    {
        Cars.Invoke();
    }
    public void sendBaloons()
    {
        Baloons.Invoke();
    }

    void WalkAgain()
    {
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        hammer.SetActive(true);
        heAtacado = false;
        atacando = false;
        mirame = true;
        if (onCryAttack)
        {
            animator.SetTrigger("Idle");
            agent.enabled = true;
            transform.position = comebackPos.position;
        }
            

        agent.Resume();
        onCryAttack = false;
        Endattacking = false;
        invencibility = false;
        //agent.isStopped = false;
    }
    public void Cry()
    {

    }
    /////////////////////////////////////////////////////////////////////////////////////

    IEnumerator Die() //llamar al morir IA_sTATS xd
    {
        FindObjectOfType<TestingInputSystem>().canceled = false;
        FindObjectOfType<TestingInputSystem>().canAttack = true;
        MagiaCuración.curando = false;
        Destroy(sl.gameObject);
        yield return new WaitForSeconds(5f);

        ascensor.GetComponent<Animator>().SetTrigger("End");
        allObjects.GetComponent<Animator>().SetTrigger("End");
        GameObject.Find("AAAAACTIVADOR DEL PUTO CANVAS").GetComponent<Bestiario>().GetKill("PINON");

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
    float timer;
}
