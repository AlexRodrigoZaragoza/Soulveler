using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallestaUlty : MonoBehaviour
{
    private float x;
    private float z;

    public Vector3 EscalaInicial;

    public static bool MareaActiva;

    Rigidbody _rb;
    TestAttack Tattck;
    TestingInputSystem tis;

    Vector3 _playerT;
    Vector3 _enemyT;

    public void Update()
    {
        if(this.gameObject.transform.localScale.x >= 25f)
        {
            StartCoroutine(DesactiavrMarea());
        }
    }

    public void SetEscala()
    {
        Tattck = FindObjectOfType<TestAttack>();
        tis = FindObjectOfType<TestingInputSystem>();
        EscalaInicial = gameObject.transform.localScale;
        x = EscalaInicial.x;
        z = EscalaInicial.z;

        StartCoroutine(tis.DevolverMovimiento()); 
        StartCoroutine(Marea());
        tis.LlamarParticulasBallesta();
    }

    public IEnumerator Marea()
    {
        while(this.gameObject.transform.localScale.x <= 25f)
        {
            x += 100f * Time.deltaTime;
            z += 100f * Time.deltaTime;

            transform.localScale = new Vector3(x, EscalaInicial.y, z);

            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator DesactiavrMarea()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.transform.localScale = EscalaInicial;
        this.gameObject.SetActive(false);
        tis.anim.SetBool("UntyBallesta", false);
        MareaActiva = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemigo")
        {
            _rb = other.gameObject.GetComponent<Rigidbody>();

            _playerT = transform.position;
            _enemyT = other.transform.position;

            Vector3 Direccion = (_enemyT - _playerT).normalized; // Direción del vecotr que se usa para empujar
            Vector3 DireccionParaDistacia = _enemyT - _playerT;

            if (MareaActiva)
            {
                _rb.AddForce(Direccion * 200, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemigo")
        {
            if (MareaActiva)
            {
                other.GetComponent<IA_STATS>().TakeDamage(Tattck.BallestaUltyDamage); //Daño ballesta
                Debug.Log(Tattck.BallestaUltyDamage);
            }
        }
    }

}
