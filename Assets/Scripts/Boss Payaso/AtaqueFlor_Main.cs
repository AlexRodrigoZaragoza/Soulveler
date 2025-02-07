using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueFlor_Main : MonoBehaviour
{

    [SerializeField] GameObject fullFlower;

    public void StartAttack()
    {
        fullFlower.SetActive(true);
    }
 
    public void MoveMeToPlayer()
    {
        fullFlower.transform.position = new Vector3(Clown.flowerPos.x, transform.position.y, Clown.flowerPos.z);
    }

    public void End()
    {
        fullFlower.SetActive(false);

        Clown.Endattacking = true;
    }

}
