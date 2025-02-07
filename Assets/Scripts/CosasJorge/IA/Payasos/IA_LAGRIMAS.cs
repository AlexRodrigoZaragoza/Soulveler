using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_LAGRIMAS : MonoBehaviour
{
    GameObject player;

    public float original_speed;

    public float _speed;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        original_speed = player.GetComponent<TestingInputSystem>().speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _speed = other.GetComponent<TestingInputSystem>().speed;

            if (other.GetComponent<TestingInputSystem>().speed >= _speed / 2)
            {
                other.GetComponent<TestingInputSystem>().slowed = true;
                other.GetComponent<TestingInputSystem>().speed = _speed / 2;
            }
        }
    }
}
