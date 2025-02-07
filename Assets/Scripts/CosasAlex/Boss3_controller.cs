using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boss3_controller : MonoBehaviour
{
    public Transform Player;
    public float rotSpeed;
    NavMeshAgent agent;

    public List<GameObject> espejos = new List<GameObject>();
    public bool mover_entre_espejos;//cambiar a futuro cuando se tengan todos los ataques
    Rigidbody rb;
    public Shoot shoot;
    bool reset_attacks;//Para que pueda volver a atacar al realizar otro ataque
    float velocidad_correr_entre_espejos=4;

    public bool Segunda_fase;

    public GameObject Clones_cristal;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Update()
    {
        
        if (Segunda_fase == true)
        {
            Segunda_fase_variables();
        }
        if (shoot.Is_Attacking == true)
        {
            agent.isStopped = true;
        }
   
        Vector3 direction = transform.position - Player.position;
        if (mover_entre_espejos == true)
        {
            Quaternion Looking = Quaternion.LookRotation(espejos[(int)calcular_espejo_cercano()].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Looking, rotSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, espejos[(int)calcular_espejo_cercano()].transform.position, Time.deltaTime * velocidad_correr_entre_espejos);
            shoot.Can_attack_again = false;
            reset_attacks = false;
            agent.isStopped=true;
        }
        else
        {
            if (shoot.Is_Attacking == true)
            {
                agent.isStopped=true;
            }
            else
            {
                agent.isStopped = false;
            }
            agent.SetDestination(Player.transform.position);//CAMBIAR 1000000%
            Quaternion Looking = Quaternion.LookRotation(Player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Looking, rotSpeed * Time.deltaTime);
            if (reset_attacks == true)
            {
                reset_attacks = false;
                shoot.Can_attack_again = true;
            }
        }
    }
    void Segunda_fase_variables()
    {
        shoot.shot_cd =0.5f;
        velocidad_correr_entre_espejos = 8;
    }
    #region trigger
    private void OnTriggerEnter(Collider other)
    {
        //SI ESTAN PEGADOS TRIGGER SINO COLLISION
        if (other.gameObject.CompareTag("Espejos") && mover_entre_espejos == true)
        {
            mover_entre_espejos = false;

            int valor_nuevo_espejo;
            if ((int)calcular_espejo_cercano() < 4)
            {
                valor_nuevo_espejo = (int)calcular_espejo_cercano() + 4;
            }
            else
            {
                valor_nuevo_espejo = (int)calcular_espejo_cercano() - 4;
            }
            transform.position = (espejos[valor_nuevo_espejo].transform.position);
            reset_attacks = true;
        }
    }
    #endregion

    #region clones_cristal
    IEnumerator clones_explosivos()
    {

        //casteo mostrar circulo rojo debajo del jugador
        GameObject clon_cristal = Instantiate(Clones_cristal, Player.transform.position, Quaternion.identity);//IRÁ CON ANIMACIÓN DE SPAWN
        yield return new WaitForSeconds(1);//tiempo q dure la animación de casteo. Boss quieto haciendo la animación durante ese tiempo
        reset_attacks = true;//Boss ya ha casteado asi que se puede mover
        //Animación de ataque
        yield return new WaitForSeconds(0.5f);//duracion del ataque
        //Ahora iría animación de se carga para explotar
        //Explotar y hacer daño al jugador
        yield return new WaitForSeconds(2);//Destruir tras haber explotado
        Destroy(clon_cristal);
    }
    #endregion

    #region Teleport_espejos

    float calcular_espejo_cercano()
    {
        float espejo_cercano=0;
        float distancia = 99999;
        for (int i = 0; i < espejos.Count; i++)
        {           
            Vector3 DireccionParaDistacia = (gameObject.transform.position - espejos[i].transform.position);
            float distancias= (Mathf.Abs(DireccionParaDistacia.magnitude));;
            if (Mathf.Abs(DireccionParaDistacia.magnitude) <= distancia)
            {
                distancia = Mathf.Abs(DireccionParaDistacia.magnitude);
                espejo_cercano = i;
            }
                
        }
        return espejo_cercano;
    }
    #endregion
}
