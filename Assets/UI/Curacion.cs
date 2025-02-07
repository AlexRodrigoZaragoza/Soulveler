using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curacion : MonoBehaviour
{

    public bool CuraSala;

    PlayerGM PGM;
    Interaccion Interact;
    // Start is called before the first frame update
    void Start()
    {
        PGM = FindObjectOfType<PlayerGM>();
        Interact = FindObjectOfType<Interaccion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CuraSala == true)
        {
            Curarse();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "CuraSala") //CURACION
        {
            CuraSala = true;
            //Interact.Cuadro_Dialogos.SetActive(true); ESTO DA ERROR, NO SE POR QUE

        }
    }

    private void OnTriggerExit(Collider other)
    {
        CuraSala = false;
        //Interact.Cuadro_Dialogos.gameObject.SetActive(false); // ESTO DA ERROR, NO SE POR QUE
    }

    void Curarse()
    {
        if (PlayerGM.health < PlayerGM.maxHealth)
        {
            PlayerGM.health = PlayerGM.health + 60;

            if (PlayerGM.health > PlayerGM.maxHealth)
            {
                PlayerGM.health = PlayerGM.maxHealth;
            }

            Debug.Log("ME HE CURADO");
            Destroy(gameObject);
        }

        else
        {
            Debug.Log("VIDA MAXIMA");
        }

        Debug.Log("YA HE TERMINADO");
    }
}
