using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTimer : MonoBehaviour
{
    public List<GameObject> enemigos = new List<GameObject>();
    public List<GameObject> posiciones = new List<GameObject>();
    public GameObject colliderPuerta;
    [SerializeField] Text timer;
    public Slider timeSlider;
    public GameObject lights;
    public bool startTimer; //Por si queremos una animación o algo
    float timerTime;
    bool newEnemyTimer;
    int actualSpawn=0;
    public Material open, closed;
    GameObject[] activeEnemies;
    GameObject[] marionetas;
    int positionReset;
    public bool salaTerminada;
    void Start()
    {
        lights.GetComponent<Renderer>().material = closed;
        timerTime = 60;
        timeSlider.minValue = 0;
        timeSlider.maxValue = 60;
        timeSlider.value = timerTime;
        timer.text = timerTime+"";
    }

    void Update()
    {
        if (startTimer)
        {
            timerTime = timerTime - Time.deltaTime;
            timeSlider.value = timerTime;
            timer.text = (int)timerTime + "";

            if (!newEnemyTimer) { StartCoroutine(spawnNewEnemies()); newEnemyTimer = true; }
            if (timerTime <= 0) { startTimer = false; timer.text = 0+"";
                StopAllCoroutines();
                salaTerminada = true;
                activeEnemies = GameObject.FindGameObjectsWithTag("Enemigo");
                marionetas = GameObject.FindGameObjectsWithTag("Espejos");
                for (int i = 0; i < activeEnemies.Length; i++)
                {
                    Destroy(activeEnemies[i]);
                }
                for (int i = 0; i < marionetas.Length; i++)
                {
                    Destroy(marionetas[i]);
                }
                lights.GetComponent<Renderer>().material = open;
                colliderPuerta.SetActive(true);//Activamos que pueda pasar de nivel
            
            }
        }
    }
    IEnumerator spawnNewEnemies()
    {
        List<int> Positions = new List<int>();
        for (int i = 0; i < posiciones.Count; i++)
        {
            Positions.Add(i);//Creamos las posiciones
        }

        for (int i = 0; i < 60; i++)
        {

            int randPattern = Random.Range(0, Positions.Count);
            positionReset = Positions[randPattern];
            Positions.Remove(positionReset);

            
            GameObject enemy = Instantiate(enemigos[Random.Range(0,enemigos.Count)], posiciones[positionReset].transform.position, Quaternion.identity);
            enemy.SetActive(true);//si hacemos prefabs cambiar
            if (timerTime < 30) { yield return new WaitForSeconds(2f); }
            else { yield return new WaitForSeconds(3); }

            if (Positions.Count <= 0)
            {
                Positions.Clear();
                for (int w = 0; w < posiciones.Count; w++)
                {
                    Positions.Add(w);//Creamos las posiciones
                }
            }


            
        }
    }
}
