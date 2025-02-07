using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastParaMagia : MonoBehaviour
{
    Camera cam;
    public LayerMask mask;
    bool hitted;
    rayConexion rConexion;
    MagiaCuración mc;
    TestingInputSystem tis;


    private void Awake()
    {
        cam = Camera.main;
        rConexion = FindObjectOfType<rayConexion>();
        tis = FindObjectOfType<TestingInputSystem>();
    }

    private void Update()
    {
        //Draw ray

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2000f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);


        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2000, mask))
        {
            mc = hit.transform.gameObject.GetComponent<MagiaCuración>();

            mc.mouseOver = true;
            rConexion.pos2 = hit.transform.gameObject;
            hitted = true;
            if(Input.GetMouseButtonDown(0))
            {
                mc.mouseDown = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                mc.mouseDown = false;
                mc.mouseDown = false;
                rConexion.chupandoVida = false;
                tis.anim.SetBool("Chuclar", false); 
                rConexion.pos2 = null;
                StartCoroutine(tis.DevolverMovimiento());
            }
        }

        else
        {
            if(hitted == true)
            {
                mc.mouseOver = false;
                hitted = false;
            }

        }
    }

}
