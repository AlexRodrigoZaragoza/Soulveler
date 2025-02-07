using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa_flores : MonoBehaviour
{
    public GameObject agua;
    float CD;
    bool Can_shoot=true;
    public float Random_force;
    float rotation;
    private void Update()
    {
        if (Can_shoot == true)
        {
            StartCoroutine(Disparo_de_agua());
        }
        gameObject.transform.Rotate(new Vector3(0,rotation,0)*Time.deltaTime*5);
        if (gameObject.transform.eulerAngles.y >= 205 || gameObject.transform.eulerAngles.y <= 65)
        {
            rotation = rotation * -1;
        }
    }
    IEnumerator Disparo_de_agua()
    {
        rotation = Random.Range(-10,10);
        CD = Random.Range(5, 10);
        Random_force = Random.Range(3, 20);
        Can_shoot = false;
        GameObject Actual_water = Instantiate(agua, gameObject.transform.position, Quaternion.identity);
        Actual_water.SetActive(true);
        Rigidbody rb = Actual_water.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0,Random.Range(0,20),0) + gameObject.transform.forward*Random_force,ForceMode.Impulse);
        yield return new WaitForSeconds(CD);
        Can_shoot = true;
    }
}
