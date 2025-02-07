using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxVidaDrop : MonoBehaviour
{
    PlayerGM gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<PlayerGM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {       
        PlayerGM.maxHealth = PlayerGM.maxHealth + 50; 
        gm.HealthBar.maxValue = PlayerGM.maxHealth;

        Destroy(GameObject.Find("MaxVida_Drop(Clone)"));
    }
}
