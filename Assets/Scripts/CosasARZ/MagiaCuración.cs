using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiaCuración : MonoBehaviour
{
    public bool mouseOver;
    public bool mouseDown;
    public static bool dejadoDeCurar;
    public static bool curando;

    GameManager gm;
    TestingInputSystem tis;
    TestAttack ta;
    rayConexion rConexion;
    raycastParaMagia rcam;

    private void Awake()
    {
        rcam = FindObjectOfType<raycastParaMagia>();
        rConexion = FindObjectOfType<rayConexion>();
        gm = FindObjectOfType<GameManager>();
        tis = FindObjectOfType<TestingInputSystem>();
        ta = FindObjectOfType<TestAttack>();
    }

    public void Update()
    {
        if (mouseOver && mouseDown && curando == false && PlayerGM.armas == 3 && TestingInputSystem.diablo == false && gm.soulWeapon)
        {
            rConexion.chupandoVida = true;
            tis.anim.SetBool("Chuclar", true);
            tis._rb.velocity = Vector3.zero;
            tis.action = true;
            curando = true;
            StartCoroutine(ChuparVida());
        }
    }


    public IEnumerator ChuparVida()
    {
        if(PlayerGM.health < PlayerGM.maxHealth)
        {
            PlayerGM.health += 1;
        }

        this.gameObject.GetComponent<IA_STATS>().TakeDamage(ta.magiaDamage);
        yield return new WaitForSeconds(0.1f);
        curando = false;
    }

}
