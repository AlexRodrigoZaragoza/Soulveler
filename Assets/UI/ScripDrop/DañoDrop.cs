using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoDrop : MonoBehaviour
{
    TestAttack TAtck;
    // Start is called before the first frame update
    void Start()
    {
        TAtck = FindObjectOfType<TestAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        TAtck.basicAttackDamage = TAtck.basicAttackDamage + 5;

        Destroy(GameObject.Find("Daño_Drop(Clone)"));
    }
}
