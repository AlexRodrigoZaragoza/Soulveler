using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody _rb;

    [SerializeField]
    GameObject _orientation;

    [SerializeField]
    GameObject _sowrd;

    float speed = 8;
    float dashForce = 30;
    float angle = 0;

    Vector3 direction;
    Vector3 actualDirection;

    public Text dashReset;

    public bool canMove = true;
    public bool hasMoved = true;
    public bool canDash = true;
    public bool dashing = false;
    public bool attacking = false;

    public int maxHealth = 30;
    public int currentHealth;
    public HealthBar healthBar;

    private void Start()
    {
        Debug.Log(GM.salas_recorridas);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void FixedUpdate()
    {
        Move();

        if (Input.GetKey(KeyCode.Space))
        {
            Dash();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Espada"))
        {
            TakeDamage(10);
            Debug.Log("golpeado");
            Debug.Log("AQUI!!");
        }
    }

    public void Update()
    {
        if(canDash == true)
        {
            dashReset.text = "Dash Reset: Ready";
            dashReset.color = Color.green;
        }
        else
        {
            dashReset.text = "Dash Reset: Reseting...";
            dashReset.color = Color.red;
        }

        if (dashing == true)
        {
            if (_rb.velocity == Vector3.zero)
            {
                dashing = false;
                canMove = true;

            }
            else
            {
                dashing = false;
                canMove = true;
            }
        }

        if (Input.GetMouseButtonDown(0) &&  attacking == false)
        {
            Attack();
        }


    }
    public void Move()
    {
        if(canMove == true)
        {

            
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");


            if (horizontalInput != 0 || verticalInput != 0)
            {
                hasMoved = true;

                direction = (_orientation.transform.forward * verticalInput + _orientation.transform.right * horizontalInput).normalized;
                actualDirection = direction;

                angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                _rb.velocity = (direction * speed);

                LookAt();
            }

            else
            {
                if(dashing == false)
                {
                    _rb.velocity = Vector3.zero;
                }

            }
        }
        
    }

    public void Dash()
    {
        if(canDash == true &&  hasMoved == true)
        {
            dashing = true;

            Vector3 DashImpulse = new Vector3(actualDirection.x * dashForce, 0, actualDirection.z * dashForce);
            _rb.AddForce(DashImpulse, ForceMode.Impulse);
            StartCoroutine(cdDash());
        }

    }
    public void LookAt()
    {
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void Attack()
    {
        attacking = true;
        Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        Vector3 theDirection = mouseOnScreen - positionOnScreen;
        actualDirection = theDirection.normalized;

        float theAngle = Mathf.Atan2(theDirection.y, theDirection.x) * Mathf.Rad2Deg -135f;
        transform.rotation = Quaternion.Euler(new Vector3(0, -theAngle, 0));

        StartCoroutine(Attacking());
    }
    public IEnumerator cdDash()
    {
        yield return new WaitForSeconds(0.1f);
        canDash = false;
        yield return new WaitForSeconds(0.5f);
        canDash = true;
    }

    public IEnumerator Attacking()
    {
        canMove = false;
        _rb.velocity = Vector3.zero;
        _sowrd.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _sowrd.SetActive(false);
        canMove = true;
        hasMoved = false;
        attacking = false;
    }
}
