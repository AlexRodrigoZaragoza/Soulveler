using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Ultys : MonoBehaviour
{
    Vector3 direccion = Vector3.zero;
    Vector2 InputVector = Vector2.zero;

    private Rigidbody _rb;

    public bool canMove = true;
    public bool canSecAttack = true;
    public bool canceled = false;
    public bool action = false;
    public float speed = 5f;
    public bool hitting;

    public GameObject _player;
    public GameObject _ultyObject;
    List<GameObject> enemigo = new List<GameObject>();
    public CinemachineVirtualCamera cam;

    TestingInputSystem tsi;
    TestAttack tattack;
    GameManager gm;
    PlayerGM PGM;
    PowerUps_Martillo PWM;


    public void Update()
    {

        if (enemigo.Count <= 0)
        {
            hitting = false;
        }
    }
    private void Awake()
    {
        tattack = FindObjectOfType<TestAttack>();
        tsi = FindObjectOfType<TestingInputSystem>();
        gm = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody>();
        PGM = FindObjectOfType<PlayerGM>();
        PWM = FindObjectOfType<PowerUps_Martillo>();

    }


    public void Movement(InputAction.CallbackContext context)
    {
        if (PlayerGM.armas == 1)
        {
            canceled = false;

            if (context.canceled)
            {
                canceled = true;
                _rb.velocity = Vector3.zero;
            }

            InputVector = context.ReadValue<Vector2>();


            if (context.performed && canMove && canceled == false)
            {
                _rb.velocity = new Vector3(InputVector.y + InputVector.x, 0, InputVector.y - InputVector.x).normalized * speed;

                direccion = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
                Rotation();
            }
        }

    }

    public void Rotation()
    {

        Vector3 RotationVector = new Vector3(InputVector.y + InputVector.x, 0f, InputVector.y - InputVector.x);
        if (InputVector != Vector2.zero)
        {
            var relative = (transform.position + RotationVector) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot;
        }
    }

    public void Attack(InputAction.CallbackContext context) //CLICK DERECHO CON ULTI ACTIVADA
    {
        if (PlayerGM.armas == 1 && PWM.UltiMartillo == true)
        {


            if (context.started && gm.soulWeapon)
            {

                if (hitting)
                {
                    foreach (GameObject enemigo in enemigo)
                    {
                        enemigo.GetComponent<IA_STATS>().TakeDamage(tattack.hamerUltyDamage);
                    }

                    enemigo.Clear();
                }



                cam.Follow = _player.transform;
                tsi.ulting = false;
                _player.SetActive(true);
                tsi.LlamarParticasMartillo();
                tsi.attacking = false;
                tsi.canMove = true;
                tsi.canSecAttack = true;
                hitting = false;
                gameObject.SetActive(false);

                PGM.StartCoroutine(PGM.RestarEspectral());

                
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemigo")
        {
            enemigo.Add(other.gameObject);
            hitting = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemigo")
        {
            enemigo.Remove(other.gameObject);
        }
    }
}