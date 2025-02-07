using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class WolfBaseState
{
    public float AttackTimer, Timer, Hp = 800, TotalHp = 800;
    public int Attack;
    public bool StopTimer,Attacking;
    public List<GameObject> MyUsableGameObjects = new List<GameObject>();
    public GameObject target, wolfGO;
    public NavMeshAgent agent;
    public abstract void EnterState(WolfStateManager wolf);

    public abstract void UpdateState(WolfStateManager wolf);

    public abstract void FixedUpdateState(WolfStateManager wolf);

    public abstract void OnTriggerEnter(WolfStateManager wolf, Collider collision);

    public abstract void OnCollisionEnter(WolfStateManager wolf, Collision collision);
}
