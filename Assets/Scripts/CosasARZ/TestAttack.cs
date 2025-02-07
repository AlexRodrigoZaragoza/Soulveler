using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAttack : MonoBehaviour
{
    public GameObject _enemy;

    //Ataques Basicos
    public float basicAttackDamage = 5;
    public float secondaryAttackDamage = 6;
    public float escopetaClose = 4;
    public float escopetaFar = -3;

    //Ataques Martillo
    public float hamerDamage = 15;
    public float hamerUltyDamage = 40;

    //Ataques Ballesta
    public float ballestaDamage = 20;
    public float BallestaUltyDamage = 15;

    //Ataques MagiaOscura
    public float magiaDamage = 3;
    public float demonDamage = 25;


    GameObject _basicAttack;
    GameObject _secondaryAttack;

    
    PlayerGM SEnergy;
    GameManager gm;
    Rigidbody _rb;
    PowerUps_Martillo PWM;

    Vector3 _playerT;
    Vector3 _enemyT;

    private void Awake()
    {
        _basicAttack = GameObject.FindGameObjectWithTag("AtaqueBasico");
        _secondaryAttack = GameObject.FindGameObjectWithTag("AtaqueSecundario");
        SEnergy = FindObjectOfType<PlayerGM>();
        gm = FindObjectOfType<GameManager>();
        PWM = FindObjectOfType<PowerUps_Martillo>();
        
        
    }

    public void OnTriggerEnter(Collider other) //ATAQUE
    {
        if(other.gameObject.tag == "Enemigo")
        {
            _rb = other.gameObject.GetComponent<Rigidbody>();

            if (TestingInputSystem._statBasicAttack)
            {
                if(!gm.soulWeapon) //Ataque basico
                {
                    other.GetComponent<IA_STATS>().TakeDamage(basicAttackDamage);
                }

                if (PlayerGM.armas == 1 && gm.soulWeapon) //Martillo
                {
                    if(PWM.GanoArmadura == true && PlayerGM.health == PlayerGM.maxHealth)
                    {
                        other.GetComponent<IA_STATS>().TakeDamage(hamerDamage);
                        PlayerGM.shield = PlayerGM.shield + 5;
                        PWM.ArmaduraObtenida = PWM.ArmaduraObtenida + 5; //CUANDO MUERES, ESTE VALOR = 0;
                    }

                    other.GetComponent<IA_STATS>().TakeDamage(hamerDamage);
                }

                if (PlayerGM.armas == 3 && TestingInputSystem.diablo && gm.soulWeapon)
                {
                    other.GetComponent<IA_STATS>().TakeDamage(demonDamage);
                }

                if (!gm.soulWeapon)
                {
                    PlayerGM.Espectral = PlayerGM.Espectral + 5;
                }
            }

            else if (TestingInputSystem._statSecondaryAttack)
            {

                _playerT = transform.position;
                _enemyT = other.transform.position;

                Vector3 Direccion = (_enemyT - _playerT).normalized; // Direción del vecotr que se usa para empujar
                Vector3 DireccionParaDistacia = _enemyT - _playerT;

                float distancia = DireccionParaDistacia.magnitude; // Calcular distancia entre objetivo y jugador

                if(!gm.soulWeapon)
                {
                    if (distancia <= 3f)
                    {
                        other.GetComponent<IA_STATS>().TakeDamage(secondaryAttackDamage + escopetaClose);
                        _rb.AddForce(Direccion * 2500, ForceMode.Impulse);

                    }

                    else if (distancia > 3f && distancia <= 9f)
                    {
                        other.GetComponent<IA_STATS>().TakeDamage(secondaryAttackDamage);
                        _rb.AddForce(Direccion * 2000, ForceMode.Impulse);
                    }

                    else if (distancia > 9f)
                    {
                        other.GetComponent<IA_STATS>().TakeDamage(secondaryAttackDamage + escopetaFar);
                        _rb.AddForce(Direccion * 1500, ForceMode.Impulse);
                    }
                }

                if (!gm.soulWeapon)
                {
                    PlayerGM.Espectral = PlayerGM.Espectral + 5;
                }
            }
        }
    }
}
