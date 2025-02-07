using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedeTransparente : MonoBehaviour
{

    public Transform personaje;


    public RaycastHit hitpoint = new RaycastHit();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Personaje()
    {
        if (Physics.Linecast(transform.position, personaje.transform.position, out hitpoint))
        {
            Debug.DrawLine(transform.position, personaje.transform.position);
        }
    }

    void Enemigos()
    {
        if (Physics.Linecast(transform.position, GameObject.FindGameObjectWithTag("Enemigo").transform.position, out hitpoint))
        {
            Debug.DrawLine(transform.position, GameObject.FindGameObjectWithTag("Enemigo").transform.position);
        }
    }
}
