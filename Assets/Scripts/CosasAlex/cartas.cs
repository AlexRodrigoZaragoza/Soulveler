using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class cartas : MonoBehaviour
{
    bool tesaliste;
    public Vector3 posicionInicial;
    Vector2 final;
    Vector2 scalefinal;
    bool enlista;
    selectorsalas selec;
    public bool correcto = false;
    public bool enSitio = false;
    public bool Colisiones = false;
    Vector2 Result;
    public bool puedeMoverse = true;
    public GameObject cartaacambiar;

    private void Start()
    {
        posicionInicial = gameObject.transform.position;

        if (gameObject.tag == "boss")
        {
            selectorsalas.posicionesocupadas.Add(gameObject);
        }

       
        selec = GameObject.FindGameObjectWithTag("map").GetComponent<selectorsalas>();
        
      
    }
 
    private void OnMouseDown()//Al clicar asignar la posición del ratón añadiendo un offset para que al clicar en la pieza esta no se teletransporte a la posición del ratón
    {
        if (puedeMoverse)
        {
            posicionInicial = gameObject.transform.position;
            enSitio = false;
            Vector2 currentMouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 currentLocation = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Result = currentLocation - currentMouseLocation; //offset
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="duel"|| other.gameObject.tag == "shop" || other.gameObject.tag == "heal" || other.gameObject.tag == "boss"&&Select.RUN== 1 || other.gameObject.tag == "hard_duel" || other.gameObject.tag == "Timer")
        {
            cartaacambiar = other.gameObject;
            enSitio = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "duel" || other.gameObject.tag == "shop" || other.gameObject.tag == "heal" || other.gameObject.tag == "boss" && Select.RUN == 1 || other.gameObject.tag == "hard_duel" || other.gameObject.tag == "Timer")
        {
            cartaacambiar =null;
            enSitio = false;
        }
    }

    private void OnMouseDrag()
    {
        if (puedeMoverse&& gameObject.tag != "boss")
        {
            Vector2 currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = currentTouchPos + Result; //Al mantener el click hacer que se mueva la pieza dependiendo de la posición del ratón y el offset previamente calculado
        }

        if (puedeMoverse && gameObject.tag == "boss"&& Select.RUN == 1)
        {
            Vector2 currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = currentTouchPos + Result; //Al mantener el click hacer que se mueva la pieza dependiendo de la posición del ratón y el offset previamente calculado
        }
    }

    private void OnMouseUp()//Al soltar
    {
        if (puedeMoverse)
        {
        
 
                Respawn();
                tesaliste = false;
 
        }
    }
    private void Update()
    {
        if (gameObject.transform.position.x == (-7.5f))
        {
            selectorsalas.posicionesocupadas[0] = gameObject;
        }
        if (gameObject.transform.position.x == (-7.5f + (2.14f)))
        {
            selectorsalas.posicionesocupadas[1] = gameObject;
        }
        if (gameObject.transform.position.x == (-7.5f + (2 * 2.14f)))
        {
            selectorsalas.posicionesocupadas[2] = gameObject;
        }
        if (gameObject.transform.position.x == (-7.5f + (3 * 2.14f)))
        {
            selectorsalas.posicionesocupadas[3] = gameObject;
        }
        if (gameObject.transform.position.x == (-7.5f + (4 * 2.14f)))
        {
            selectorsalas.posicionesocupadas[4] = gameObject;
        }
        if (gameObject.transform.position.x == (-7.5f + (5 * 2.14f)))
        {
            selectorsalas.posicionesocupadas[5] = gameObject;
        }
        if (gameObject.transform.position.x == (-7.5f + (6 * 2.14f)))
        {
            selectorsalas.posicionesocupadas[6] = gameObject;
        }
        if (gameObject.transform.position.x == 7.5f)
        {
            selectorsalas.posicionesocupadas[7] = gameObject;
        }
    }
    public void Respawn()
    {
        if (enSitio==false)
        {
            gameObject.transform.position = posicionInicial;
            //cartaacambiar.GetComponent<cartas>().enSitio = false; posible fallo
        }
        else
        {
            Vector3 nuevaposi;

            nuevaposi= cartaacambiar.GetComponent<cartas>().posicionInicial;
            cartaacambiar.transform.position = gameObject.GetComponent<cartas>().posicionInicial;
            gameObject.transform.position = nuevaposi;


            cartaacambiar.GetComponent<cartas>().posicionInicial = posicionInicial;
            cartaacambiar.GetComponent<cartas>().enSitio = false;
            posicionInicial = nuevaposi;
            enSitio = false;


        }

    }
  
}
