using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosTienda : MonoBehaviour
{

    public bool ObjetoVida, ObjetoVidaMax, ObjetoDa�o;
    public GameObject TextoTienda, TextoVida, TextoVidaMax, TextoDa�o;
    Interaccion Interact;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        Interact = FindObjectOfType<Interaccion>();
    }

    // Update is called once per frame
    void Update()
    {

    }

   
}

