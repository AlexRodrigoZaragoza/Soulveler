using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallestaProyectil : MonoBehaviour
{
    TestAttack TA;
    public void Start()
    {
        TA = FindObjectOfType<TestAttack>();
        StartCoroutine(VidaBala());
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemigo")
        {
            other.GetComponent<IA_STATS>().TakeDamage(TA.ballestaDamage);

            Destroy(gameObject);
        }
    }

    public IEnumerator VidaBala()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
