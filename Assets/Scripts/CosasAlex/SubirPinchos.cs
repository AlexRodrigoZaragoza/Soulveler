using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SubirPinchos : MonoBehaviour
{
    public bool startAnim;
    Animator anim;
    [SerializeField]GameObject[] barricade;
    bool dontspam;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!startAnim&&other.gameObject.tag=="Player"&& !dontspam)
        {
            for (int i = 0; i < barricade.Length; i++)
            {
                barricade[i].SetActive(true);
            }
            dontspam = true;
            anim.SetBool("barricade", true);
            GetComponent<MeshCollider>().enabled = true;

        }
        
    }
}
