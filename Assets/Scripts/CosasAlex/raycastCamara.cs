using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastCamara : MonoBehaviour
{
    public GameObject Player;
    GameObject _actualObstaculo;
    Color col;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.Normalize(Player.transform.position - gameObject.transform.position) * 999, out hit, 999))
        {
            if (hit.transform.gameObject.tag == "Obstaculo")
            {
                //Renderer render = hit.transform.gameObject.GetComponent<Renderer>();
                //render.material.color = Color.clear;
                _actualObstaculo = hit.transform.gameObject;

                col = hit.transform.gameObject.GetComponent<Renderer>().material.color;
                col.a=0.3f;

                hit.transform.gameObject.GetComponent<Renderer>().material.color = col;
            }
            else if (_actualObstaculo != null)
            {
                col.a = 1;
                _actualObstaculo.GetComponent<Renderer>().material.color=col;
            }
     
        }
    }
}
