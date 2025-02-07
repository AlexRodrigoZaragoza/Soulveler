using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampa_agua : MonoBehaviour
{
    public GameObject agua_en_suelo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor")
        {
            GameObject water= Instantiate(agua_en_suelo, gameObject.transform.position,Quaternion.identity);
            water.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
