using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueCajas_Main : MonoBehaviour
{

    [SerializeField] GameObject bigBox, smallBox;
    [SerializeField] Transform spawnPointBigBox;
    [SerializeField] SpawnerArea sa;

    GameObject myBigBox;
    List<GameObject> myBoxes = new List<GameObject>();

    float timer;
    public void StartAttack()
    {
        gameObject.SetActive(true);
        timer = 0;
        Invoke("FirstPhase", 2);
    }

    void Healing()
    {
        if (Clown.clownCurrentHealth <= 300)
        {
            Clown.clownCurrentHealth++;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10)
        {
            End();
        }
    }

    void FirstPhase()
    {
        Destroy(myBigBox);
        myBoxes = sa.SpawnOnAreaGO(smallBox, 3);
        foreach (GameObject box in myBoxes)
        {
            box.transform.GetChild(0).GetComponent<Caja>().myType = "SmallBox";
            box.transform.position += new Vector3(0, 5, 0);
        }

        int i = Random.Range(0, 3);

        myBoxes[i].transform.GetChild(0).GetComponent<Caja>().chosen = true;

        InvokeRepeating("Healing", 1, 2);
    }

    public void DestroyMySmallBox()
    {
        CancelInvoke();
        foreach (GameObject box in myBoxes)
        {
            Destroy(box);
        }
        Invoke("End", 2);
    }

    void End()
    {

        CancelInvoke();

        if (myBigBox != null)
        {
            Destroy(myBigBox);
        }
        if (myBoxes != null)
        {
            foreach (GameObject box in myBoxes)
            {
                Destroy(box);
            }

            myBoxes.Clear();
        }

        gameObject.SetActive(false);
        Clown.Endattacking = true;
    }

}
