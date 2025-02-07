using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerGM : MonoBehaviour
{
    public static float health;
    public static float maxHealth = 100;
    public static float haveShield;
    public static float shield;
    public static float maxShield;
    public static int Tickets;
    public static float Espectral;
    public static float armas;
    public static bool espectral0;
    public static bool vengoDeEspectral0;

    bool ShieldEnabled;

    public Slider HealthBar;
    public Slider ArmaEspectral;
    public Slider ShieldBar;

    public TMP_Text ContadorTickets, VidaTotal, EspectralTotal, ShieldTotal;

    Atraer TicketsC;
    GameManager Gmanager;
    TestingInputSystem tis;
    public Animator anim;
    Cooldown cd;

    PowerUps_Martillo PWM;

    public bool Infinito;
    bool muerte;

    public static float contadorEnemigos;
    [SerializeField] GameObject DeathCanvas;
    [SerializeField] List<GameObject> otrosCanvas = new List<GameObject>();

    public void Awake()
    {
        DeathCanvas.SetActive(false);
        muerte = false;
        TicketsC = FindObjectOfType<Atraer>();
        tis = FindObjectOfType<TestingInputSystem>();
        health = maxHealth;
        HealthBar.maxValue = maxHealth;
        ArmaEspectral.maxValue = 100;
        maxShield = maxHealth;
        ShieldBar.maxValue = maxShield;
       
        Gmanager = FindObjectOfType<GameManager>();
        cd = FindObjectOfType<Cooldown>();
        PWM = FindObjectOfType<PowerUps_Martillo>();

        shield = 0;
        
        PWM.ArmaduraObtenida = 0;

        
    }
    private void Update()
    {
        HealthBar.value = health;
        ContadorTickets.text = Tickets.ToString();
        ArmaEspectral.value = Espectral;
        ShieldBar.value = shield;

        float todo = health + shield;

        VidaTotal.text = todo.ToString() + "/" + maxHealth.ToString();

        if (shield > 0 && (health >= maxHealth))
        {
            VidaTotal.color = Color.yellow;
        }

        else VidaTotal.color = Color.white;

        

        EspectralTotal.text = Espectral.ToString() + "/" + ArmaEspectral.maxValue.ToString();
        

        if (Espectral >= 100) Espectral = 100;      //¿¿¡¡ESTO AQUI??!!

        if (Espectral <= 0)
        {
            Gmanager.soulWeapon = false;
        }

        if(health < 0)  health = 0;

        if (armas == 1 && PWM.GanoArmadura == true)
        {
            ShieldEnabled = true;
        }

    }

    public static bool dieOnce = true;

    public void TakeDamage(float damage)
    {
        if (ShieldEnabled == true)
        {
            if (shield > 0)
            {
                shield -= damage;

                if (shield < 0)
                {
                    shield = 0;
                }
            }
            else if(shield <= 0)
            {
                health -= damage;

            }
        }

        else
        {
            health -= damage;
        }

        PWM.dañoRecibido = PWM.dañoRecibido += damage;

        if (health <= 0 && dieOnce)
        {
            dieOnce = false;
            Invoke(nameof(DestroyEnemy), 0.5f);
        }


    }
    private void DestroyEnemy()
    {
        muerte = false;
        GetComponent<TestingInputSystem>().enabled = false;
        GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = false;
        Invoke("Die", 2);
    }

    public IEnumerator RestarEspectral()
    {
        if (Gmanager.soulWeapon)
        {
            while (Espectral != 0 && Infinito == false)
            {
                Espectral = Espectral - 5;
                yield return new WaitForSeconds(1f);
                if (Espectral <= 0)
                {

                    //
                    Gmanager.PalaActiva();
                    Gmanager.soulWeapon = false;
                    espectral0 = true;
                    vengoDeEspectral0 = true;
                    StartCoroutine(tis.DevolverMovimiento());
                }
                if (Gmanager.soulWeapon == false)
                {
                    tis.canAttack = true;
                    break;
                }
            }

        }

    }

    public IEnumerator CDEspectral()
    {
        yield return new WaitForSeconds(5f);
        Gmanager.Cooldown = false;

    }

    void Die()
    {
        DeathCanvas.SetActive(true);
        foreach (GameObject canvas in otrosCanvas)
        {
            canvas.SetActive(false);
        }
    }

}

