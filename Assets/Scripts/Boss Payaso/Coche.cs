using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coche : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Finish")
        {
            GetComponent<Animator>().SetTrigger("Hit");
            Destroy();
        }
        else if (collision.gameObject.tag == "enviroment" || collision.gameObject.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Hit");
            Laugh();
        }
    }

    public void Move()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 70);
    }

    void Laugh()
    {
        //sonido risa
        //animación retirar coche
        Invoke("Destroyed", 2);
    }

    void Destroy()
    {
        //crying++
        //animación destruir
        Invoke("Destroyed", 2);
    }

    void Destroyed()
    {
        Destroy(this.gameObject);
    }

}
