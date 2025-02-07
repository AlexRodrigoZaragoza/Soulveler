using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa_flecha : MonoBehaviour
{
    public GameObject arrow;
    public GameObject tower;
    GameObject new_arrow;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (new_arrow == null)
            {
                new_arrow = Instantiate(arrow, tower.transform.position, Quaternion.identity);
                new_arrow.SetActive(true);

               Rigidbody rb = new_arrow.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.AddForce(transform.right*60,ForceMode.Impulse);
                
                
            }
        }
        
    }

    

}
