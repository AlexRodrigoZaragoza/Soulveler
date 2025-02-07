using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfStandState : WolfBaseState
{
    bool multipleattacksonce_01 = false, multipleattacksonce_02 = false, multipleattacksonce_03 = false;

    public override void EnterState(WolfStateManager wolf)
    {
        WolfStateManager.Instance.rb.isKinematic = false;
        //agent = WolfStateManager.Instance.agent;
        //agent = wolfGO.transform.GetChild(0).GetComponent<NavMeshAgent>();
    }
    public override void UpdateState(WolfStateManager wolf)
    {

        if (!StopTimer) Timer += Time.deltaTime;

        if (!Attacking)
        {
            agent.SetDestination(target.transform.position);
        }
        if ((Timer >= 2 && !Attacking && Vector3.Distance(agent.gameObject.transform.position, target.transform.position) <=6 ) || 
            (Timer >= 5 && !Attacking) || 
            Vector3.Distance(agent.gameObject.transform.position, target.transform.position) <= 3 && !Attacking)
        {

            StopTimer = true;
            Attacking = true;
            Timer = 0;
            AttackTimer = 0;

            Attack = Random.Range(0, 10);
            if (Attack <= 3)
            {
                NormalAttack();
            }
            else if (Attack <= 7 && multipleattacksonce_01)
            {
                MultipleAttacks_01();
            }
            else
            {
                SpinAttack();
            }
        }

        if (Attacking)
        {
            AttackTimer += Time.deltaTime;


            if (Attack <= 3)
            {
                if (AttackTimer >= 1)
                {
                    EndNormalAttack();
                }
            }
            else if (Attack <= 7)
            {
                if (AttackTimer >= 0.75f && multipleattacksonce_02)
                {
                    MultipleAttacks_02();
                }
                if (AttackTimer>= 1.5f && multipleattacksonce_03)
                {
                    MultipleAttacks_03();
                }
                if (AttackTimer >= 2.25f)
                {
                    EndMultipleAttacks();
                }
            }
        }

    }

    public override void FixedUpdateState(WolfStateManager wolf)
    {

    }
    public override void OnTriggerEnter(WolfStateManager wolf, Collider collision)
    {
        if (collision.gameObject.tag == "AtaqueBasico")
        {
            GetDamage(wolf, target);
        }
    }

    public override void OnCollisionEnter(WolfStateManager wolf, Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "enviroment" && spinning)
        {
            Debug.Log("sisisiis");
            WolfStateManager.Instance.rb.velocity = PinballEffect();
        }
        if (collision.gameObject.tag == "Player" && spinning)
        {

        }
    }

    int rebound;

    Vector3 PinballEffect()
    {
        if (rebound <= 3)
        {
            Vector3 direction = new Vector3(target.transform.position.x - agent.gameObject.transform.position.x, agent.gameObject.transform.position.y, target.transform.position.z - agent.gameObject.transform.position.z); ;
            Vector3 normalDirection = Vector3.Normalize(direction) * 35;
            rebound++;
            return normalDirection;
        }
        else
        {
            EndSpinAttack();
        }
        return Vector3.zero;

    }

    void NormalAttack()
    {
        agent.isStopped = true;
        agent.gameObject.transform.LookAt(new Vector3(target.transform.position.x, agent.gameObject.transform.position.y, target.transform.position.z));
        MyUsableGameObjects[0].SetActive(true);
    }

    void EndNormalAttack()
    {
        MyUsableGameObjects[0].SetActive(false);
        agent.isStopped = false;
        StopTimer = false;
        Attacking = false;
        AttackTimer = 0;
        Timer = 0;
    }

    bool spinning = false;

    void SpinAttack()
    {
        agent.isStopped = true;
        spinning = true;
        WolfStateManager.Instance.rb.velocity = PinballEffect();

    }

    void EndSpinAttack()
    {
        spinning = false;
        agent.isStopped = false;
        StopTimer = false;
        Attacking = false;
        AttackTimer = 0;
        Timer = 0;
        rebound = 0;
    }

    void MultipleAttacks_01()
    {
        multipleattacksonce_01 = false;
        agent.isStopped = true;
        MyUsableGameObjects[0].SetActive(true);
        agent.gameObject.transform.LookAt(new Vector3(target.transform.position.x, agent.gameObject.transform.position.y, target.transform.position.z));
        Vector3 tryPos = new Vector3(target.transform.position.x - agent.gameObject.transform.position.x, agent.gameObject.transform.position.y, target.transform.position.z - agent.gameObject.transform.position.z);
        Vector3 newPos = Vector3.Normalize(tryPos);
        agent.gameObject.transform.position += newPos * 2;
    }
    void MultipleAttacks_02()
    {
        multipleattacksonce_02 = false;
        agent.gameObject.transform.LookAt(new Vector3(target.transform.position.x, agent.gameObject.transform.position.y, target.transform.position.z));
        MyUsableGameObjects[0].SetActive(true);
        Vector3 tryPos = new Vector3(target.transform.position.x - agent.gameObject.transform.position.x, agent.gameObject.transform.position.y, target.transform.position.z - agent.gameObject.transform.position.z);
        Vector3 newPos = Vector3.Normalize(tryPos);
        agent.gameObject.transform.position += newPos * 2;
    }
    void MultipleAttacks_03()
    {
        multipleattacksonce_03 = false;
        agent.gameObject.transform.LookAt(new Vector3(target.transform.position.x, agent.gameObject.transform.position.y, target.transform.position.z));
        MyUsableGameObjects[0].SetActive(true);
        Vector3 tryPos = new Vector3(target.transform.position.x - agent.gameObject.transform.position.x, agent.gameObject.transform.position.y, target.transform.position.z - agent.gameObject.transform.position.z);
        Vector3 newPos = Vector3.Normalize(tryPos);
        agent.gameObject.transform.position += newPos * 2;
    }
    void EndMultipleAttacks()
    {
        MyUsableGameObjects[0].SetActive(false);
        agent.isStopped = false;
        StopTimer = false;
        Attacking = false;
        AttackTimer = 0;
        Timer = 0;
        agent.enabled = true;
        multipleattacksonce_01 = true;
        multipleattacksonce_02 = true;
        multipleattacksonce_03 = true;
    }
    void GetDamage(WolfStateManager wolf, GameObject player)
    {
        Hp -= player.GetComponent<TestAttack>().basicAttackDamage;
        WolfStateManager.Instance.hpSlider.value = Hp;
        if (Hp <= 0)
        {
            Die(wolf);
        }
    }
    void Die(WolfStateManager wolf)
    {
        wolf.DestroyEverythingInside(MyUsableGameObjects, wolf.MyBody[1]);
        wolf.Dead();
    }
}
