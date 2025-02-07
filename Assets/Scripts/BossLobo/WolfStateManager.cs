using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WolfStateManager : MonoBehaviour
{
    public static WolfStateManager Instance;


    WolfBaseState currentState;
    public WolfBedState BedState = new WolfBedState();
    public WolfStandState StandState = new WolfStandState();

    public List<GameObject> MyBody = new List<GameObject>();
    [SerializeField] List<GameObject> BedUsableObjects = new List<GameObject>();
    [SerializeField] List<GameObject> StandUsableObjects = new List<GameObject>();

    [SerializeField] GameObject player, wolf;
    public NavMeshAgent agent;
    public Rigidbody rb;
    int currentBody = 0;
    public Slider hpSlider;

    private void Awake()
    {
        if (Instance == null) Instance = this;

    }

    void Start()
    {
        currentState = BedState;
        //BedState.MyUsableGameObjects = BedUsableObjects;
        StandState.MyUsableGameObjects = StandUsableObjects;
        hpSlider.maxValue = currentState.TotalHp;
        hpSlider.value = currentState.TotalHp;
        agent = MyBody[1].GetComponent<NavMeshAgent>();
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.target = player;
        currentState.wolfGO = this.gameObject;
        currentState.UpdateState(this);


    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    private void OnTriggerEnter(Collider collision)
    {
        currentState.OnTriggerEnter(this, collision);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(WolfBaseState state, GameObject body)
    {        

        state.Hp = currentState.Hp;
        currentState = state;
        if (body.GetComponent<NavMeshAgent>() != null)
        {
            currentState.agent = body.GetComponent<NavMeshAgent>();
        }
        body.SetActive(true);
        state.EnterState(this);
        currentBody++;
    }

    public void DestroyEverythingInside(List<GameObject> allGO, GameObject me)
    {
        foreach (GameObject go in allGO)
        {
            Destroy(go);
        }

        Destroy(me);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

}
