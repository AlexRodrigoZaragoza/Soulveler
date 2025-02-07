using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBedState : WolfBaseState
{
    GameObject SideAttack_Right, SideAttack_right, BiteAttack_01, BiteAttack_02, BiteAttack_03, Xlimit;
    bool sideAttack = false, frontAttack = false, right = false;

    public override void EnterState(WolfStateManager wolf)
    {
        MyUsableGameObjects.Add(wolf.MyBody[0].transform.GetChild(2).gameObject);
        MyUsableGameObjects.Add(wolf.MyBody[0].transform.GetChild(3).gameObject);
        MyUsableGameObjects.Add(wolf.MyBody[0].transform.GetChild(4).gameObject);
        MyUsableGameObjects.Add(wolf.MyBody[0].transform.GetChild(5).gameObject);
        MyUsableGameObjects.Add(wolf.MyBody[0].transform.GetChild(6).gameObject);
        MyUsableGameObjects.Add(wolf.MyBody[0].transform.GetChild(7).gameObject);

        SideAttack_Right =  MyUsableGameObjects[0];
        SideAttack_right = MyUsableGameObjects[1];
        BiteAttack_01 = MyUsableGameObjects[2];
        BiteAttack_02 = MyUsableGameObjects[3];
        BiteAttack_03 = MyUsableGameObjects[4];
        Xlimit = MyUsableGameObjects[5];

        Timer = 0;
    }
    public override void UpdateState(WolfStateManager wolf)
    {
        if (!StopTimer) Timer += Time.deltaTime;

        if (Timer >= 1.5f && !Attacking)
        {
            StopTimer = true;
            Attacking = true;
            Timer = 0;
            AttackTimer = 0;
            if (target.transform.position.z < wolfGO.transform.position.z) right = true;
            else right = false;
            if (target.transform.position.x < Xlimit.transform.position.x) BiteAttack_FirstPhase();
            else SideAttack();
        }

        if (Attacking)
        {
            AttackTimer += Time.deltaTime; 

            if (AttackTimer >= 0.5f && sideAttack)
            {
                EndSideAttack();
            }
            else if (AttackTimer >= 0.9f && frontAttack)
            {
                BiteAttack_End();
            }
            else if (AttackTimer >= 0.6f && frontAttack)
            {
                BiteAttack_ThirdPhase();
            }
            else if (AttackTimer >= 0.3f && frontAttack)
            {
                BiteAttack_SecondPhase();
            }
        }
    }

    void SideAttack()  //inicio de ataque
    {
        sideAttack = true;
        if (right)  //comprueba el lado en el que esté el player
        {
            //Attack right
            SideAttack_Right.SetActive(true);
            //activar animación de ataque  (se mueve de un sitio a otro)
        }
        else
        {
            //Attack right
            SideAttack_right.SetActive(true);
            //activar animación de ataque
        }
    }
    
    void EndSideAttack()  
    {
        sideAttack = false;
        SideAttack_Right.SetActive(false);
        SideAttack_right.SetActive(false);
        StopTimer = false;
        Attacking = false;
        AttackTimer = 0;
    }

    void BiteAttack_FirstPhase()  //inicio de ataque
    {
        if (right)
        {
            frontAttack = true;
            BiteAttack_03.SetActive(true);

        }
        else
        {
            frontAttack = true;
            BiteAttack_01.SetActive(true);
        }

    }
    void BiteAttack_SecondPhase()
    {
        BiteAttack_03.SetActive(false);
        BiteAttack_01.SetActive(false);
        BiteAttack_02.SetActive(true);
    }
    void BiteAttack_ThirdPhase()
    {
        if (right)
        {
            BiteAttack_02.SetActive(false);
            BiteAttack_01.SetActive(true);
        }
        else
        {
            BiteAttack_02.SetActive(false);
            BiteAttack_03.SetActive(true);

        }

    }
    void BiteAttack_End()
    {
        frontAttack = false;
        BiteAttack_03.SetActive(false);
        BiteAttack_01.SetActive(false);
        StopTimer = false;
        Attacking = false;
        AttackTimer = 0;
    }

    public override void FixedUpdateState(WolfStateManager wolf) { }  //en esta fase no se mueve, por lo que no hace falta usarlo PERO NO ELIMINAR

    public override void OnTriggerEnter(WolfStateManager wolf, Collider collision) //para los ataques
    {
        if (collision.gameObject.tag == "AtaqueBasico")
        {
            GetDamage(wolf, target);
        }
    }

    public override void OnCollisionEnter(WolfStateManager wolf, Collision collision)
    {

    }

    void GetDamage(WolfStateManager wolf, GameObject player)
    {
        Hp -= player.GetComponent<TestAttack>().basicAttackDamage;
        WolfStateManager.Instance.hpSlider.value = Hp;
        if (Hp <= TotalHp / 2)
        {
            DestroyedBed(wolf);
        }
    }

    void DestroyedBed(WolfStateManager wolf)
    {
        wolf.DestroyEverythingInside(MyUsableGameObjects, wolf.MyBody[0]);
        wolf.SwitchState(wolf.StandState, wolf.MyBody[1]);
    }
}
