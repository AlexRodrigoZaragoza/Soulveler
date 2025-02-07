using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntradaRun : MonoBehaviour
{
    public Collider entrada;
    public TextMeshPro textEntrada;
    public GameObject smookepoof, smookepoof01;

    private void Awake()
    {
        smookepoof.SetActive(false);
        smookepoof01.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator OfPoof()
    {
        smookepoof.SetActive(true);
        smookepoof01.SetActive(true);
        yield return new WaitForSeconds(3f);
        smookepoof.SetActive(false);
        smookepoof01.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerGM.armas != 0)
        {
            StartCoroutine(OfPoof());

            entrada.isTrigger = true;
            textEntrada.text = "PUEDES PASAR";
            textEntrada.color = Color.white;
        }
        else
        {
            StartCoroutine(OfPoof());

            entrada.isTrigger = false;
            textEntrada.text = "BUSCA UNA ESTATUA";
            textEntrada.color = Color.red;
        }
    }
}
