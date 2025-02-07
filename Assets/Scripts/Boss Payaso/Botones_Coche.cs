using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botones_Coche : MonoBehaviour
{

    [SerializeField] GameObject wall;

    bool once = false;
    bool inWall = false;
    bool startTimer = false;

    float timer = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inWall && !once)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Activated");
            wall.transform.position += new Vector3(0, 10, 0);
            once = true;
            startTimer = true;
        }

        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                wall.transform.position -= new Vector3(0, 10, 0);

                //animación pared rota o algo?

                startTimer = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inWall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inWall = false;
        }
    }
}
