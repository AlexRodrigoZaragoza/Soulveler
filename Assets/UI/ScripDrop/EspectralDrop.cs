using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspectralDrop : MonoBehaviour
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
        gm.ArmaEspectral.maxValue = gm.ArmaEspectral.maxValue + 50;
        PlayerGM.Espectral = PlayerGM.Espectral + 50;

        Destroy(GameObject.Find("Espectral_Drop(Clone)"));
    }
}
