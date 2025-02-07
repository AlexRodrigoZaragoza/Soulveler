using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerGM.health = PlayerGM.health + 50;

        if (PlayerGM.health > PlayerGM.maxHealth)
        {
            PlayerGM.health = PlayerGM.maxHealth;
        }

        Destroy(GameObject.Find("Pocion_Drop(Clone)"));
    }
}
