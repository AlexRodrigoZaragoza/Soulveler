using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayConexion : MonoBehaviour
{
    public LineRenderer lr;
    public GameObject pos1;
    public GameObject pos2;
    GameManager gm;
    TestingInputSystem tis;
    public bool chupandoVida;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        tis = FindObjectOfType<TestingInputSystem>();
        lr.positionCount = 2;
    }

    private void Update()
    {
        if (PlayerGM.armas == 3 && TestingInputSystem.diablo == false && gm.soulWeapon && chupandoVida)
        {
            lr.enabled = true;
            lr.SetPosition(0, new Vector3(pos1.transform.position.x, pos1.transform.position.y + 2.1f, pos1.transform.position.z));
            lr.SetPosition(1, new Vector3(pos2.transform.position.x, pos2.transform.position.y + 1.5f, pos2.transform.position.z));
            pos1.transform.LookAt(pos2.transform.position);
            pos1.transform.Rotate(new Vector3(pos1.transform.rotation.x, pos1.transform.rotation.y -70, pos1.transform.rotation.z)); 
        }

        else
        {
            lr.enabled = false;
            tis.anim.SetBool("Chuclar", false);
        }

    }
}
