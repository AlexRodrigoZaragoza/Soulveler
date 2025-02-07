using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueCoches_Main : MonoBehaviour
{

    [SerializeField] GameObject button1, button2, button3;
    [SerializeField] GameObject car1, car2, car3;
    List<GameObject> cars = new List<GameObject>();

    int carSelected;
    public void StartAttack()
    {
        gameObject.SetActive(true);
        cars.Add(car1);
        cars.Add(car2);
        cars.Add(car3);
        Invoke("FirstPhase", 2);
    }

    void FirstPhase()
    {
        car1.SetActive(true);
        car2.SetActive(true);
        car3.SetActive(true);

        Invoke("SecondPhase", 2);
    }

    void SecondPhase()
    {
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);

        carSelected = Random.Range(0, cars.Count);

        //animación meterse en coche
        //barra peligro

        Invoke("ThirdPhase", 2);
    }

    void ThirdPhase()
    {
        cars[carSelected].GetComponent<Animator>().SetTrigger("Go");

        cars.RemoveAt(carSelected);

        if (cars.Count > 0)
        {
            Invoke("SecondPhase", 2);
        }
        else
        {
            Invoke("FourthPhase", 2);
        }
    }

    void FourthPhase()
    {
        //rotar palancas y devolver el mapa a normal
        Invoke("End", 2);
    }

    void End()
    {
        gameObject.SetActive(false);
        Clown.Endattacking = true;
    }

}
