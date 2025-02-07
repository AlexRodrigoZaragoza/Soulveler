using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueGlobo_Main : MonoBehaviour
{
    [SerializeField] GameObject clownBaloon, baloonTypeA, baloonTypeB, baloonTypeC;

    [SerializeField] SpawnerArea sa;

    [SerializeField] List<GameObject> Abaloons = new List<GameObject>();
    [SerializeField] List<GameObject> Bbaloons = new List<GameObject>();
    [SerializeField] List<GameObject> Cbaloons = new List<GameObject>();

    int CrySave;

    GameObject myBaloon;

    float time;

    [SerializeField] bool startTimer, once;

    public bool crying;
    private void OnEnable()
    {
        CrySave = 0;
        time = 0;
        once = true;
        startTimer = false;
        crying = false;
    }

    private void Update()
    {   
        if (startTimer)
        {
            time += Time.deltaTime;

            if (time >= 15 && !crying && once)
            {
                GoEnd();
                once = false;
            }
        }
    }
    public void StartAttack()
    {
        gameObject.SetActive(true);
        FirstPhase();
    }

    void FirstPhase()
    {
        int i = Random.Range(1, 4);

        if (i == 1)
        {
            myBaloon = Instantiate(baloonTypeA, clownBaloon.transform);
        }
        else if (i == 2)
        {
            myBaloon = Instantiate(baloonTypeB, clownBaloon.transform);
        }
        else if (i == 3)
        {
            myBaloon = Instantiate(baloonTypeC, clownBaloon.transform);
        }
        //payaso se va
        myBaloon.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        myBaloon.transform.localPosition = Vector3.zero;
        Destroy(myBaloon.GetComponent<Collider>());
        Destroy(myBaloon.GetComponent<Rigidbody>());
        Invoke("SecondPhase", 3);
    }

    void SecondPhase()
    {
        Abaloons = sa.SpawnOnAreaGO(baloonTypeA, 5);
        Bbaloons = sa.SpawnOnAreaGO(baloonTypeB, 5);
        Cbaloons = sa.SpawnOnAreaGO(baloonTypeC, 5);

        startTimer = true;
    }

    public void GetBaloonInfo(string type, GameObject baloon)
    {
        if (clownBaloon.transform.GetChild(0).name == type )
        {

            if (type == "Balloon_TypeA(Clone)") Abaloons.Remove(baloon);

            else if (type == "Balloon_TypeB(Clone)") Bbaloons.Remove(baloon);

            else if (type == "Balloon_TypeC(Clone)") Cbaloons.Remove(baloon);

            baloon.GetComponent<Globo>().Destroyed();

            CrySave++;
        }
        else if (once)
        {
            Debug.Log("exploooison");
            ExplodeAll_1();
            once = false;
        }

        if (CrySave >= 5 && once)
        {
            Debug.Log("saaafe");
            Safe();
            once = false;
        }
    }

    void Safe()
    {
        crying = true;
        Invoke("End", 2);
    }

    void ExplodeAll_1()
    {
        foreach (GameObject baloon in Abaloons)
        {
            if (baloon != null) baloon.GetComponent<Globo>().StartExploding();
        }
        Invoke("ExplodeAll_2", 2);
    }
    void ExplodeAll_2()
    {
        foreach (GameObject baloon in Bbaloons)
        {
            if (baloon != null) baloon.GetComponent<Globo>().StartExploding();
        }
        Invoke("ExplodeAll_3", 2);
    }
    void ExplodeAll_3()
    {
        foreach (GameObject baloon in Cbaloons)
        {
            if (baloon != null) baloon.GetComponent<Globo>().StartExploding();
        }
        Invoke("GoEnd", 2);
    }

    public void GoEnd()
    {
        if (!crying)
        {
            foreach (GameObject baloons in Abaloons)
            {
                if (baloons != null) baloons.GetComponent<Globo>().StartExploding();
            }
            foreach (GameObject baloons in Bbaloons)
            {
                if (baloons != null) baloons.GetComponent<Globo>().StartExploding();
            }
            foreach (GameObject baloons in Cbaloons)
            {
                if (baloons != null) baloons.GetComponent<Globo>().StartExploding();
            }

            Invoke("End", 2);
        }
        //else End();
    }

    void End()
    {
        foreach (GameObject baloons in Abaloons)
        {
            if (baloons != null) Destroy(baloons);
        }
        foreach (GameObject baloons in Bbaloons)
        {
            if (baloons != null) Destroy(baloons);
        }
        foreach (GameObject baloons in Cbaloons)
        {
            if (baloons != null) Destroy(baloons);
        }
        Abaloons.Clear();
        Bbaloons.Clear();
        Cbaloons.Clear();
        Destroy(myBaloon);
        gameObject.SetActive(false);

        Clown.Endattacking = true;
        Debug.Log("terminoo");
    }


}
