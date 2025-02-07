using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss_Witch : MonoBehaviour
{
    public GameObject Player;
    public float rotSpeed;
    public GameObject rayoAtaque;
    public GameObject salidaAtaque;
    public bool isCasting,canMove;
    Animator anim;
    public int cantidadMorteros;

    public bool embestida;
    Rigidbody rb;
    NavMeshAgent agent;
    Vector3 _destinoEmbestida;
    bool embestir=true;
    [Header("Objects")]
    public GameObject ExplosionArea, ExplosionZone;
    public GameObject BombArea, Meteorite;
    public GameObject endAll;

    int randomAttacks;
    bool ataqueCDcast;

    public bool tp;
    Quaternion Looking;
    [Header ("Places")]
    public List<Transform> tpPlaces = new List<Transform>();
    public List<Transform> walkPlaces = new List<Transform>();

    float porcentajeActual;
    bool lowDistance;

    public GameObject Escudo;
    public GameObject VidaEscudo;
    public GameObject HornosGM;

    bool resetEmbestida;

    float AttackSpeed;
    float distance;

    void Start()
    {
        anim = GetComponent<Animator>();
        AttackSpeed = 1;
        porcentajeActual = 75;
        randomAttacks = Random.Range(0, 21);
        rb =GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if(gameObject.GetComponent<IA_STATS>().vida*100/ gameObject.GetComponent<IA_STATS>().vidaMax<= porcentajeActual)
        {
            cambiarPorcentajeYEscudo();
        }
        

        if (!isCasting&&canMove)
        {
            //Rotacion
            Vector3 _fixRotar = new Vector3(Player.transform.position.x - transform.position.x,0, Player.transform.position.z - transform.position.z);
            Looking = Quaternion.LookRotation(_fixRotar);
            transform.rotation = Quaternion.Slerp(transform.rotation, Looking, rotSpeed * Time.deltaTime);
            distance = Mathf.Abs((Player.transform.position - transform.position).magnitude);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                agent.SetDestination(-Player.transform.position - (Player.transform.forward * distance));
            }
            else { agent.SetDestination(-Player.transform.position); }

            if (!ataqueCDcast)
            {
                StartCoroutine(ataqueCD());
            }
            
        }

        

        if (!canMove)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Looking, rotSpeed * Time.deltaTime);
            Vector3 DireccionParaDistacia = (gameObject.transform.position - Player.transform.position);
            float distancia = Mathf.Abs(DireccionParaDistacia.magnitude);//Medimos distancia enemigo - jugador
            if (distancia <= 5)
            {
                
                if(Random.Range(0, 7) <= 2&&!isCasting&& gameObject.GetComponent<IA_STATS>().vida * 100 / gameObject.GetComponent<IA_STATS>().vidaMax < 50)//A mitad de vida puede tpearse
                {
                    tp = true;
                }
                if (!isCasting&&!tp)
                {
                    StartCoroutine(AtaqueExplosionCercana());
                    
                }
                if (tp&&!isCasting)
                {
                    StartCoroutine(tpLejano());
                }
            }
            else if(!lowDistance)
            {

                agent.SetDestination(transform.position);
                if (randomAttacks <= 10)
                {
                    
                        if (embestir == true&&!isCasting)
                        {
                            isCasting = true;
                            Embestida();
                        }
                    
                        agent.SetDestination(_destinoEmbestida);
                    float distance = (Mathf.Abs((transform.position - _destinoEmbestida).magnitude));
                        if (distance<=3)//CAMBIAR POR VECTOR 2
                        {
                        anim.SetBool("IsCharge", false);
                        agent.speed = 6;//Cambiar a futuro por su debida velocidad 
                            embestir = true;
                            isCasting = false;
                            canMove = true;
                            resetEmbestida = false;
                        }
                    
                }
                if (randomAttacks < 15 && randomAttacks > 10)
                {
                    if (!isCasting)
                    {
                        AtaqueMortero();
                    }
                }
                if (randomAttacks >= 15 && randomAttacks <= 20)
                {
                    if (!isCasting)
                    {
                        AtaqueRayo();
                    }
                }


                if (gameObject.GetComponent<IA_STATS>().vida <= 0)
                {
                    GameObject.Find("AAAAACTIVADOR DEL PUTO CANVAS").GetComponent<Bestiario>().GetKill("JURIEL");
                    FindObjectOfType<TestingInputSystem>().canceled = false;
                    FindObjectOfType<TestingInputSystem>().canAttack = true;
                    MagiaCuración.curando = false;
                    endAll.GetComponent<Animator>().SetBool("endLevel", true);
                    Destroy(gameObject);
                }

            }
        }
        


    }

    void morir()
    {

    }

    void cambiarPorcentajeYEscudo()
    {
        HornosGM.GetComponent<Hornos>().Encender_Hornos = true;
        Escudo.SetActive(true);
        VidaEscudo.SetActive(true);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        porcentajeActual = porcentajeActual - 25;
        if (porcentajeActual == 25)
        {
            AttackSpeed = 2;
        }
        if (porcentajeActual == 0)
        {
            porcentajeActual = 5;
        }
    }

    IEnumerator tpLejano()//Integrar???
    {
        lowDistance = true;
        isCasting = true;
        yield return new WaitForSeconds(2 / AttackSpeed);
        transform.position = (tpPlaces[(int)calcularTPMasLejano()].transform.position);
        agent.SetDestination(transform.position);
        tp = false;
        canMove = true;
        isCasting = false;
        lowDistance = false;
    }
    float calcularPosicionMasLejana()
    {
        float zonaMasLejana = 0;
        float distancia = 0;
        for (int i = 0; i < walkPlaces.Count; i++)
        {
            Vector3 DireccionParaDistacia = (Player.transform.position - walkPlaces[i].transform.position);
            float distancias = (Mathf.Abs(DireccionParaDistacia.magnitude));
            if (Mathf.Abs(DireccionParaDistacia.magnitude) >= distancia)
            {
                distancia = Mathf.Abs(DireccionParaDistacia.magnitude);
                zonaMasLejana = i;
            }

        }
        return zonaMasLejana;
    }
    float calcularTPMasLejano()
    {
        float tpMasLejano = 0;
        float distancia = 0;
        for (int i = 0; i < tpPlaces.Count; i++)
        {
            Vector3 DireccionParaDistacia = (Player.transform.position - tpPlaces[i].transform.position);
            float distancias = (Mathf.Abs(DireccionParaDistacia.magnitude)); ;
            if (Mathf.Abs(DireccionParaDistacia.magnitude) >= distancia)
            {
                distancia = Mathf.Abs(DireccionParaDistacia.magnitude);
                tpMasLejano = i;
            }

        }
        return tpMasLejano;
    }
    IEnumerator ataqueCD()
    {
        ataqueCDcast = true;
        randomAttacks = Random.Range(0, 20);
        yield return new WaitForSeconds(3 / AttackSpeed);//Tiempo en el que vuelve a atacar
        canMove = false;
        ataqueCDcast = false;
    }

    void AtaqueMortero()
    {
        isCasting = true;
        anim.SetBool("IsMortar", true);
        StartCoroutine(ataqueMortero());

    }

    IEnumerator AtaqueExplosionCercana()
    {
        lowDistance = true;
        agent.SetDestination(transform.position);
        isCasting = true;
        anim.SetBool("IsExplosion", true);
        ExplosionArea.SetActive(true);
        yield return new WaitForSeconds(1f / AttackSpeed);
        ExplosionArea.SetActive(false);
        ExplosionZone.SetActive(true);
        agent.SetDestination(transform.position);
        yield return new WaitForSeconds(1.5f / AttackSpeed);
        anim.SetBool("IsExplosion", false);
        lowDistance = false;
        ExplosionZone.SetActive(false);
        isCasting = false;
        canMove = true;
    }

    IEnumerator ataqueMortero()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < cantidadMorteros; i++)
        {

            Vector3 Posiciones = new Vector3((Player.transform.position.x + Random.Range(-1, 1)), 0, Player.transform.position.z + Random.Range(-1, 1));
            GameObject area = Instantiate(BombArea, Posiciones, Quaternion.identity);

            Vector3 meteoPosi= new Vector3(area.transform.position.x, 20, area.transform.position.z);
            GameObject meteorito = Instantiate(Meteorite,meteoPosi,Quaternion.identity);
            meteorito.GetComponent<Rigidbody>().AddForce(-transform.up*30,ForceMode.Impulse);

            yield return new WaitForSeconds(0.8f / AttackSpeed);
        }
        anim.SetBool("IsMortar", false);
        isCasting = false;
        canMove = true;
    }


    void AtaqueRayo()
    {
        StartCoroutine(isMagicAttacking());
    }

    void Embestida()
    {
        anim.SetBool("IsCharge", true);
        embestir = false;
        _destinoEmbestida = Player.transform.position;
        agent.acceleration = 400;
        agent.speed = 200;
    }

    IEnumerator isMagicAttacking()
    {
        isCasting = true;
        // var position = gameObject.transform.position + (Player.transform.position - gameObject.transform.position)*0.5f;
        yield return new WaitForSeconds(0.5f/AttackSpeed);//haCasteado
        Vector3 spawnRay = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        GameObject ataqueDeRayo = Instantiate(rayoAtaque, spawnRay, gameObject.transform.rotation);
        //ataqueDeRayo.transform.parent = gameObject.transform;
        ataqueDeRayo.SetActive(true);
        yield return new WaitForSeconds(2 / AttackSpeed);//haCasteado
        Destroy(ataqueDeRayo);
        isCasting = false;
        canMove = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hornos")
        {
            other.tag = "Untagged";
            damageFurnace();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!embestir&&!resetEmbestida)
            {
                resetEmbestida = true;
                collision.gameObject.GetComponent<PlayerGM>().TakeDamage(20);
            }
        }
    }

    void damageFurnace()
    {
        HornosGM.GetComponent<Hornos>().Encender_Hornos = false;
        Escudo.SetActive(false);
        VidaEscudo.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

}
