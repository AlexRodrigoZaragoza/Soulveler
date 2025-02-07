using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala_direccional : MonoBehaviour
{
 
    Rigidbody rb;

    void OnCollisionEnter(Collision other)
    {
        
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine(borrar());
    }

    void LateUpdate()
    {
       
        this.transform.forward = rb.velocity;
    }
    IEnumerator borrar()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
