using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{

    [SerializeField] AtaqueCajas_Main main;
    public string myType;
    public bool chosen = false;

    int hp;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (myType == "BigBox")
        {
            hp = 1;
        }
        else if (myType == "SmallBox")
        {
            hp = 3;
        }

        main = GameObject.Find("FullBoxes").GetComponent<AtaqueCajas_Main>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17)
        {

            hp--;
            if (hp <= 0)
            {
                Destroy();
            }

        }
    }

    public void Destroy()
    {

        if (myType == "SmallBox")
        {
            if (chosen)
            {
                main.DestroyMySmallBox();
            }
        }
        //animación romperse
        Invoke("Destroyed", 2);
    }

    void Destroyed()
    {
        Destroy(gameObject);
    }

}
