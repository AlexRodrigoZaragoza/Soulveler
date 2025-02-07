using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateWitch : MonoBehaviour
{
    [SerializeField] GameObject bruja, colliderActivar,tuberia;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            colliderActivar.SetActive(true);
            bruja.SetActive(true);
            tuberia.GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject);
        }
    }

}
