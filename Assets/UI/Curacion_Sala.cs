using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curacion_Sala : MonoBehaviour
{
    bool JugadorDentroFuente;
    // Start is called before the first frame update

    PlayerGM PGM;

    void Start()
    {
        PGM = FindObjectOfType<PlayerGM>();
    }

    // Update is called once per frame
    void Update()
    {
        if (JugadorDentroFuente == true)
        {
            CuracionFuente();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Jugador detectado");
            JugadorDentroFuente = true;
        }
    }

    void CuracionFuente()
    {

        PlayerGM.health = PlayerGM.health + 1;

        if (PlayerGM.health > PlayerGM.maxHealth)
        {
            PlayerGM.health = PlayerGM.maxHealth;
        }
    }
}
