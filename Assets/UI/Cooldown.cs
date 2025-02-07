using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{

    [Header("Cooldown")]
    public Image Cooldown_Imagen;
    public float cooldown = 5f;

    bool isCooldown, isCooldownSC;

    [Header("Escopeta")]
    public Image Cooldown_Escopeta;
    public float cooldown_scope = 3f; //AJUSTAR TAMBIEN EN TIS

    GameManager gm;
    TestingInputSystem tis;

    // Start is called before the first frame update
    void Start()
    {
        Cooldown_Imagen.fillAmount = 0;
        Cooldown_Escopeta.fillAmount = 0;

        gm = FindObjectOfType<GameManager>();
        tis = FindObjectOfType<TestingInputSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Reactivar();
        Scope();

    }

    public void Reactivar()
    {
        if (gm.soulWeapon == false && isCooldown == false && gm.Cooldown == true)
        {
            isCooldown = true;
            Cooldown_Imagen.fillAmount = 1;
        }

        if (isCooldown)
        {
            Cooldown_Imagen.fillAmount -= 1 / cooldown * Time.deltaTime;

            if (gm.Cooldown == false)
            {
                Cooldown_Imagen.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    public void Scope()
    {

        if (gm.soulWeapon == false && tis.canSecAttack == false && isCooldownSC == false)
        {
            isCooldownSC = true;
            Cooldown_Escopeta.fillAmount = 1;
            Debug.Log("Entro aqui");
        }

        if (isCooldownSC)
        {
            Cooldown_Escopeta.fillAmount -= 1 / cooldown_scope * Time.deltaTime;
            Debug.Log("ESTOY EN ISCOOLDOWN");

            if (tis.canSecAttack == true)
            {
                Cooldown_Escopeta.fillAmount = 0;
                isCooldownSC = false;

                Debug.Log("YA PUEDO USAR ESCOPETA");
            }
        }

    }
}
