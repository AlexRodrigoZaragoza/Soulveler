using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Atraer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Test;

    GameObject player;
    public float MinModifier = 100;
    public float MaxModifier = 200;
    spawnenemy spawner;
    SpawnTimer spawnertimer;
    Vector3 _velocity = Vector3.zero;
    public bool _isFollowing;
    PlayerGM Tickets;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "TimerDuel")
        {
            spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<spawnenemy>();
        }
        else
        {
  
        }
            
        Tickets = FindObjectOfType<PlayerGM>();

        GetComponent<Collider>().enabled = false;
    }

    public void StartFollowing()
    {
        _isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (SceneManager.GetActiveScene().name != "TimerDuel")
        {
            if (spawner.salaTerminada == true)
            {
                GetComponent<Collider>().enabled = true;
                transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
            }
        }
        else
        {

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerGM.Tickets++;
            Destroy(gameObject);
        }
    }
}
