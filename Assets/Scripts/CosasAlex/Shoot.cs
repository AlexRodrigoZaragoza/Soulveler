using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shoot : MonoBehaviour
{
    public GameObject projectile;   
    public GameObject shot_position;
    public GameObject target;        
    public GameObject parent;       
    public float speed = 15;            
    float turnSpeed = 2;
    bool canShoot = true;
    public float shot_cd;
    public int distance;
    public GameObject area_explosion;
    public bool Can_attack_again=true;

    public bool Is_Attacking;
    void Update()
    {
        
            RaycastHit hit;
            if (Physics.Raycast(shot_position.transform.position, Vector3.Normalize(target.transform.position - gameObject.transform.position) * distance, out hit, distance)) //Determina distancia entre jugador y boss
            {
                Vector3 DireccionParaDistacia = (gameObject.transform.position - target.transform.position);
                float distancia = Mathf.Abs(DireccionParaDistacia.magnitude);//Medimos distancia enemigo - jugador

                if (hit.transform.gameObject.name == "Player" && distancia >= 10&&Can_attack_again==true)//Si está cerca empieza con los ataques a rango
                {
                    Can_attack_again = false;
                    Bullet_hell_shoot();

                }
                if(hit.transform.gameObject.name == "Player" && distancia <= 10&&Can_attack_again==true)
                {
                    Can_attack_again = false;

                    StartCoroutine(explosion_cercana());
                    
                }

            }

        if (canShoot == false)//Si no accede es para que al disparar pueda rotar para disparar balas en distintas direcciones ya que en otra función rota.
        {
            float? angle = RotateTurret();
        }
            

    }

    #region explosion cercana
    IEnumerator explosion_cercana()
    {
        Is_Attacking=true;

        area_explosion.SetActive(true);//CAMBIAR LUEGO POR LO QUE SEA DEFINITIVO. GAME OBJECT SOLO DE PRUEBA
        yield return new WaitForSeconds(4);//Tiempo a definir
        Can_attack_again = true;
        area_explosion.SetActive(false);
        Is_Attacking = false;


    }


    #endregion

    #region Disparo_hell

    void Bullet_hell_shoot() //Instanciamos las balas
    {
        if (canShoot)

        {
            float? angle = CalculateAngle();
            List<GameObject> bullets = new List<GameObject>();
            for (int i = 0; i <= 10; i++)
            {
               
                this.transform.localEulerAngles = new Vector3(360f - (float)angle, i*4-20, 0f);//Disparo modo bullethell, hacemos q rote para disparar balas en distintas direcciones



                bullets.Add(Instantiate(projectile, shot_position.transform.position, shot_position.transform.rotation));
                bullets[i].GetComponent<Rigidbody>().velocity = speed * this.transform.forward;
                bullets[i].SetActive(true);
            }
            
            
            canShoot = false;
            Invoke("CanShootAgain", shot_cd);
        }
    }
    void CanShootAgain()
    {
        Can_attack_again=true;
        canShoot = true;
    }

    float? RotateTurret()
    {
        float? angle = CalculateAngle(); //false para el angulo superior y true para el inferior. Cambia el booleano para ver el efecto!!

        if (angle != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f); // ... rotate the turret using EulerAngles because they allow you to set angles around x, y & z.
        }
        return angle;
    }

    float? CalculateAngle() 
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
                          
        float x = targetDir.magnitude;
                                       
        float Gravedad = 9.81f; 
        float underTheSqrRoot = (Mathf.Pow(speed,4) - Gravedad * (Gravedad * Mathf.Pow(x,2) + 2 * y * (Mathf.Pow(speed,2))));

        if (underTheSqrRoot >= 0f) //asegurarnos de q sea posile
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = Mathf.Pow(speed, 2) + root;
            float lowAngle = Mathf.Pow(speed, 2) - root;

           
            return (Mathf.Atan2(lowAngle, Gravedad * x) * Mathf.Rad2Deg);
          
        }
        else
            return null;
    }

    #endregion
}
