using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public TestingInputSystem tis;

    public Collider col;
    public Rigidbody _rb;

    public float maxDistance;
    public float distanciaDelDash;
    public float fuerzaSalto;

    private Vector3 origin;
    private Vector3 direction;

    public void Start()
    {
        tis = GetComponent<TestingInputSystem>();
        col = gameObject.GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {

        origin = transform.position;
        direction = transform.forward;

        tis.fuerzaDash = 75;

        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {

            if(hit.transform.tag == "enviroment")
            {
                Debug.DrawRay(origin, direction * maxDistance, Color.red);
            }

            else if(hit.transform.tag == "void")
            {
                float resultado = hit.distance;

                Debug.DrawRay(origin, direction * maxDistance, Color.white);

                if(resultado <= distanciaDelDash)
                {
                    tis.fuerzaDash = fuerzaSalto;
                }

                if(resultado <= distanciaDelDash && tis.dashActive == true)
                {
                    _rb.useGravity = false;
                    col.isTrigger = true;
                    StartCoroutine(ActivarTrigger());
                }
            }
        }

        else
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.green);
        }
    }

    public IEnumerator ActivarTrigger()
    {
        yield return new WaitForSeconds(0.05f);
        _rb.useGravity = true;
        col.isTrigger = false;
    }

    public IEnumerator CanMoveNow()
    {
        yield return new WaitForSeconds(0.1f);
        tis.canMove = true;
    }

    public IEnumerator DesactivarDash()
    {
        yield return new WaitForEndOfFrame();
        tis.dashActive = false;
    }
}
