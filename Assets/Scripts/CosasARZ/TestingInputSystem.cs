using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    Vector3 direccion = Vector3.zero;
    Vector2 InputVector = Vector2.zero;

    [SerializeField] PlayerDashTrail PDT;
    public GameObject _ataque;
    public GameObject _ataqueSecundario;
    public GameObject _ataqueSuport;
    public GameObject _ataqueDiablo;
    public GameObject VFXDiablo1;
    public GameObject VFXDiablo2;
    public GameObject _ultyMartillo;
    public GameObject _ultyBallesta;
    public GameObject _bala;
    public GameObject cañon;
    Rigidbody balaRb;
    GameObject _currentAtaque;
    GameObject _currentAtaqueSecundario;
    public GameObject _particulasMartillo;
    public GameObject _particulasBallesta;

    public Rigidbody _rb;

    GameManager gm;
    RayCaster ray;
    PowerUps_Martillo PWM;
    PowerUps_Ballesta PWB;
    public Ultys ult;
    public BallestaUlty Bulty;

    //CD's

    public float CDbasico = 0.7f;
    float CDescopeta = 0.3f;
    public float CDmartillo = 1.3f;
    public float CDMartilloUlty = 4f;
    public float CDballesta = 0.5f;
    public float CDballestaUlty = 5f;
    public float CDdemon = 0.6f;

    public bool attacking = false;
    public bool canAttack = true;
    public bool canDash = true;
    public bool canMove = true;
    public bool canSecAttack = true;
    public bool canceled = false;
    public bool action = false;
    public bool ulting;
    public static bool curando;
    public bool escopeteando;
    [SerializeField]
    public static bool diablo;
    public static bool dashing = false;
    public bool dashActive = false;
    public static bool stop = false;
    public static bool _statBasicAttack;
    public static bool _statSecondaryAttack;

    public float speed = 20f;
    public float _speed;
    public bool slowed;

    public float fuerzaDash;

    public Animator anim;

    private void Awake()
    {
        _particulasMartillo.SetActive(false);
        anim.SetBool("Muerto", false);
        _rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>();
        ray = GetComponent<RayCaster>();
        PWM = FindObjectOfType<PowerUps_Martillo>();
        PWB = FindObjectOfType<PowerUps_Ballesta>();
    }

    private void Start()
    {
        _speed = speed;
    }

    private void Update()
    {
        if(_rb.velocity.sqrMagnitude <= 0)
        {
            anim.SetBool("Corriendo", false);

        }
        else
            anim.SetBool("Corriendo", true);

        //Jorge paso por aqui
        if (stop == true)
        {

            GetComponent<PlayerInput>().enabled = false;

            if (IA_RANGE_POMPA.playerGolpeado == true)
            {
                StartCoroutine(DesStun());
            }
        }
        else
        {
            GetComponent<PlayerInput>().enabled = true;
        }

        if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D)) { 
            if (!attacking && !dashing && canMove && canceled == false && stop == false && BallestaUlty.MareaActiva == false && MagiaCuración.curando == false)
            { _rb.velocity = new Vector3(InputVector.y + InputVector.x, 0, InputVector.y - InputVector.x).normalized * speed; } }


        StartCoroutine(DesSlow());
    }

    IEnumerator DesSlow()
    {
        if (slowed)
        {
            yield return new WaitForSeconds(2f);
            speed = _speed;
            slowed = false;
        }
    }

    IEnumerator DesStun()
    {
        IA_RANGE_POMPA.playerGolpeado = false;
        yield return new WaitForSeconds(IA_RANGE_POMPA.tiempoStun);
        TestingInputSystem.stop = false;
    }

    public void Movement(InputAction.CallbackContext context)
    {
        canceled = false;

        if (context.canceled && !dashing)
        {
            canceled = true;
            _rb.velocity = Vector3.zero;
        }

        InputVector = context.ReadValue<Vector2>();


        if (context.performed &&!attacking && !dashing && canMove && canceled == false && stop == false && BallestaUlty.MareaActiva == false && MagiaCuración.curando== false)
        {
            anim.SetBool("UntyBallesta", false);
            _rb.velocity = new Vector3(InputVector.y+InputVector.x, 0, InputVector.y-InputVector.x).normalized * speed;
            
            direccion = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
            Rotation();
        }
    }

    public IEnumerator DevolverMovimiento()
    {

        if(PlayerGM.armas == 2 && !PlayerGM.espectral0)
        {
            _rb.velocity = Vector3.zero;
            action = true;
            yield return new WaitForSeconds(2);
        }

        if (PlayerGM.armas == 3 && !diablo && gm.soulWeapon && !PlayerGM.espectral0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (PlayerGM.armas == 3 && diablo && gm.soulWeapon && !PlayerGM.espectral0)
        {
            yield return new WaitForSeconds(1);
        }

        if(!PlayerGM.espectral0 && PlayerGM.vengoDeEspectral0 == true)
        {
            PlayerGM.vengoDeEspectral0 = false;
            yield return new WaitForSeconds(0.01f);
            PlayerGM.espectral0 = false;
        }

        if(BallestaUlty.MareaActiva == true)
        {
            yield return new WaitForSeconds(0.01f);
            anim.SetBool("UntyBallesta", false);
        }

        _currentAtaque.SetActive(false);
        canMove = true;
        attacking = false;
        _statBasicAttack = false;


        if (canceled)
        {
            _rb.velocity = Vector3.zero;
        }

        if (action && !canceled)
        {
            _rb.velocity = new Vector3(InputVector.y + InputVector.x, 0, InputVector.y - InputVector.x).normalized * speed;
            direccion = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
            Rotation();
        }
    }

    public void Rotation()
    {
        
        Vector3 RotationVector = new Vector3(InputVector.y + InputVector.x, 0f, InputVector.y - InputVector.x);
        if( InputVector != Vector2.zero && !attacking)
        {
            var relative = (transform.position + RotationVector) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot;
        }
    }

    public void AtaqueBasico(InputAction.CallbackContext context) //ACCION DE ATACAR
    {
        if (context.started && !attacking && canAttack && !stop && BallestaUlty.MareaActiva == false)
        {

            Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
            Vector3 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            Vector3 rotationDirection = mouseOnScreen - positionOnScreen;

            float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 45-angle, 0));

            canMove = false;
            attacking = true;
            _statBasicAttack = true;
            canAttack = false;

            if (!gm.soulWeapon)
            {
                _currentAtaque = _ataque;
                _ataque.SetActive(true);
                StartCoroutine(AtaqueBasicoCorrutina());
                StartCoroutine(DesactivarColliderPegar());

                int NumRandom = Random.Range(0, 3);

                if (NumRandom == 0)
                {
                    anim.SetBool("Ataque1", true);
                }
                if (NumRandom == 1)
                {
                    anim.SetBool("Ataque2", true);
                }
                if (NumRandom == 2)
                {
                    anim.SetBool("Ataque3", true);
                }
            }

            if(gm.soulWeapon)
            {
                if(PlayerGM.armas == 1)
                {
                    _currentAtaque = _ataqueSuport;
                    _ataqueSuport.SetActive(true);
                    StartCoroutine(AtaqueBasicoCorrutina());
                    StartCoroutine(DesactivarColliderPegar());

                    int NumRandom = Random.Range(0, 3);

                    if (NumRandom == 0)
                    {
                        anim.SetBool("Ataque1", true);
                    }
                    if (NumRandom == 1)
                    {
                        anim.SetBool("Ataque2", true);
                    }
                    if (NumRandom == 2)
                    {
                        anim.SetBool("Ataque3", true);
                    }

                }
                else if (PlayerGM.armas == 2)
                {
                    Rigidbody rb = Instantiate(_bala, cañon.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * 80, ForceMode.Impulse);
                    StartCoroutine(AtaqueBasicoCorrutina());

                    anim.SetBool("Shoot", true);
                }
                else if (PlayerGM.armas == 3)
                {
                    if(!diablo)
                    {
                        if(MagiaCuración.curando == true)
                        {
                            canMove = false;
                            attacking = true;
                            _statBasicAttack = true;
                        }

                        else
                        {
                            canMove = true;
                            attacking = false;
                            _statBasicAttack = false;
                        }
                    }

                    if(diablo)
                    {
                        this.gameObject.GetComponent<PlayerGM>().TakeDamage(10);
                        _currentAtaque = _ataqueDiablo;
                        _ataqueDiablo.SetActive(true);

                        int i = Random.Range(0, 2);

                        if(i == 0)
                        {
                            anim.SetBool("Demon1", true);
                            VFXDiablo1.SetActive(true);
                            StartCoroutine(DesactivarColliderPegar());
                        }
                        else if(i == 1)
                        {
                            anim.SetBool("Demon2", true);
                            VFXDiablo2.SetActive(true);
                            StartCoroutine(DesactivarColliderPegar());
                        }

                        StartCoroutine(AtaqueBasicoCorrutina());
                    }
                }
                else if (PlayerGM.armas == 4)
                {
                    //NADA
                }
                else if (PlayerGM.armas == 5)
                {
                    //NADA
                }
                else if (PlayerGM.armas == 6)
                {
                    //NADA
                }
            }

        }
    }

    public IEnumerator AtaqueBasicoCorrutina()
    {
        _rb.velocity = Vector3.zero;
        action = true;

        if (!gm.soulWeapon) //CD Pala
        {
            yield return new WaitForSeconds(CDbasico);
        }

        if (PlayerGM.armas == 1 && gm.soulWeapon) //CD Martillo
        {
            yield return new WaitForSeconds(CDmartillo);
        }

        if (PlayerGM.armas == 2 && gm.soulWeapon)//CD Ballesta
        {
            yield return new WaitForSeconds(CDballesta);
            anim.SetBool("Shoot", false);
        }

        if (PlayerGM.armas == 3 && gm.soulWeapon && diablo)//CD Diablo
        {
            yield return new WaitForSeconds(CDdemon);
            anim.SetBool("Demon1", false);
            anim.SetBool("Demon2", false);
            VFXDiablo1.SetActive(false);
            VFXDiablo2.SetActive(false);
        }

        _currentAtaque.SetActive(false);
        canMove = true;
        attacking = false;
        _statBasicAttack = false;

        if (canceled)
        {
            _rb.velocity = Vector3.zero;
        }

        if (action && !canceled)
        {
            _rb.velocity = new Vector3(InputVector.y + InputVector.x, 0, InputVector.y - InputVector.x).normalized * speed;
            direccion = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
            Rotation();
        }
        anim.SetBool("Ataque1", false);
        anim.SetBool("Ataque2", false);
        anim.SetBool("Ataque3", false);
        canAttack = true;
    }

    public void AtauqeSecundario(InputAction.CallbackContext context)
    {
        if(context.started && !attacking && !stop && BallestaUlty.MareaActiva == false && canSecAttack)
        {
            if (!gm.soulWeapon && canSecAttack)
            {
                anim.SetBool("Escopeta", true);
                escopeteando = true;
                gm._pala.SetActive(false);
                

                Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
                Vector3 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

                Vector3 rotationDirection = mouseOnScreen - positionOnScreen;

                float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(new Vector3(0, 45 - angle, 0));

                canSecAttack = false;
                canMove = false;
                attacking = true;
                canAttack = false;

                _statSecondaryAttack = true;
                _currentAtaqueSecundario = _ataqueSecundario;
                _ataqueSecundario.SetActive(true);

                StartCoroutine(AtaqueSecundarioCorrutina());
                StartCoroutine(AtaqueSecundarioCooldown());
                StartCoroutine(DesactivarColliderPegar());
            }

            if (gm.soulWeapon)
            {
                if (PlayerGM.armas == 1 && PWM.UltiMartillo == true)
                {
                    _currentAtaqueSecundario = _ultyMartillo;
                    _currentAtaqueSecundario.SetActive(true);
                    _currentAtaqueSecundario.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    ulting = true;
                    this.gameObject.SetActive(false);
                }
                else if (PlayerGM.armas == 2 && PWB.UltiBallesta == true)
                {
                    if(BallestaUlty.MareaActiva ==  false)
                    {
                        anim.SetBool("UntyBallesta", true);
                        canMove = false;
                        attacking = true;
                        BallestaUlty.MareaActiva = true;
                        _currentAtaqueSecundario = _ultyBallesta;
                        _currentAtaqueSecundario.SetActive(true);
                        _currentAtaqueSecundario.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+0.5f, this.transform.position.z);
                        Bulty = FindObjectOfType<BallestaUlty>();
                        Bulty.SetEscala();
                    }

                }
                else if (PlayerGM.armas == 3)
                {   
                    diablo = !diablo;
                    canAttack = true;
                }
                else if (PlayerGM.armas == 4)
                {
                    //NADA
                }
                else if (PlayerGM.armas == 5)
                {
                    //NADA
                }
                else if (PlayerGM.armas == 6)
                {
                    //NADA
                }
            }
        }
    }

    public IEnumerator AtaqueSecundarioCorrutina() // pala 1 martillo 1.5 ballesta 0.5 demonio 0.8  // CUANTO MAS GRANDE EL NUMERO, MAS LENTO.
    {
        _rb.velocity = Vector3.zero;
        action = true;

        if (!gm.soulWeapon) //CD Escopeta
        {
            yield return new WaitForSeconds(CDescopeta);
            anim.SetBool("Escopeta", false);
            escopeteando = false;
            gm._pala.SetActive(true);
        }

        if (PlayerGM.armas == 1 && gm.soulWeapon) //CD Martillo Ulty
        {
            yield return new WaitForSeconds(CDMartilloUlty);
        }

        if (PlayerGM.armas == 2 && gm.soulWeapon) //CD Ballesta Ulty
        {
            yield return new WaitForSeconds(CDballestaUlty);

        }

        _currentAtaqueSecundario.SetActive(false);
        canMove = true;
        attacking = false;
        _statSecondaryAttack = false;


        if (canceled)
        {
            _rb.velocity = Vector3.zero;
        }

        if (action && !canceled)
        {
            _rb.velocity = new Vector3(InputVector.y + InputVector.x, 0, InputVector.y - InputVector.x).normalized * speed;
            direccion = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
            Rotation();
        }
        anim.SetBool("Ataque1", false);
        anim.SetBool("Ataque2", false);
        anim.SetBool("Ataque3", false);
        canAttack = true;
    }

    public IEnumerator DesactivarColliderPegar()
    {
        yield return new WaitForSeconds(0.1f);
        _currentAtaque.SetActive(false);
    }
    public IEnumerator AtaqueSecundarioCooldown()
    {
        if (!gm.soulWeapon)
        {
            yield return new WaitForSeconds(3f);
        }

        else
            yield return new WaitForSeconds(0.01f);

        canSecAttack = true;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && canMove && canDash && !stop && BallestaUlty.MareaActiva == false)
        {
            PDT.enabled = true;
            dashing = true;
            canDash = false;
            dashActive = true;
            canMove = false;
            StartCoroutine(ray.CanMoveNow());
            StartCoroutine(ray.DesactivarDash());
            StartCoroutine(DashCorrutina());
            StartCoroutine(DashCooldown());
            StartCoroutine(DashTrail());
        }
    }

    public IEnumerator DashTrail()
    {
        yield return new WaitForSeconds(0.2f);
        PDT.enabled = false;

    }
    public IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        fuerzaDash = 50;
        canDash = true;
    }

    public IEnumerator DashCorrutina() 
    {
        _rb.velocity = Vector3.zero;
        _rb.AddForce(transform.forward * fuerzaDash, ForceMode.Impulse);
        action = true;

        yield return new WaitForSeconds(0.1f);

        dashing = false;
        _rb.velocity = Vector3.zero;

        if (canceled)
        {
            _rb.velocity = Vector3.zero;
        }

        if (action && !canceled && !dashing && canMove)
        {
            _rb.velocity = new Vector3(InputVector.y + InputVector.x, 0, InputVector.y - InputVector.x).normalized * speed;
            direccion = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);
            Rotation();
        }
    }

    public void LlamarParticasMartillo()
    {
        StartCoroutine(Particulasmartillo());
    }

    public IEnumerator Particulasmartillo()
    {
        _particulasMartillo.SetActive(true);
        _particulasMartillo.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(0.3f);
        _particulasMartillo.SetActive(false);
    }

    public void LlamarParticulasBallesta()
    {
        _particulasBallesta.GetComponent<ParticleSystem>().Play();
    }
            
    #region Test y apuntes

    // AHORA MISMO ESTA DESACTIVADA SI SE QUIERE ACTIVAR CREAR LOS INPUT ACTION Y ASIGNARLOS EN EL INSPECTOR
    public void Jump(InputAction.CallbackContext context) // Calback context en especifico "context" es la fase en la que se encuntra la ación (started, performed, reselased)
    {
        if (context.performed) // Se realiza en caso de que haya sido ejecutado
        {
            _rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    public void MegaJump(InputAction.CallbackContext context) //Esta parte del codigo testea la función hold de el input action 
    {
        if (context.performed)
        {
            _rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
        }
    }

    #endregion
}
