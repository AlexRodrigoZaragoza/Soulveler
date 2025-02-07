using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globo : MonoBehaviour
{
    [SerializeField] GameObject alert;

    [SerializeField] AtaqueGlobo_Main baloonManager;

    [SerializeField] bool once = false;

    private void Start()
    {
        alert = gameObject.transform.GetChild(2).gameObject;
        baloonManager = GameObject.Find("FullBaloons").GetComponent<AtaqueGlobo_Main>();
    }

    public void StartExploding()
    {
        InvokeRepeating("Explode", 0, 0.05f);
    }

    void Explode()
    {
        alert.transform.localScale += new Vector3(1, 0, 1);

        if (alert.transform.localScale.x >= 20)
        {
            CancelInvoke();
            Exploding();
        }
    }

    public void Exploding()
    {
        CancelInvoke();
        alert.GetComponent<Collider>().enabled = true;
        Invoke("End", 2);
    }

    public void Destroyed()
    {
        //animación de destruirse
        Destroy(gameObject);
    }

    void End()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17 && !once)
        {
            baloonManager.GetBaloonInfo(gameObject.name, gameObject);
            once = true;
        }
    }

}
