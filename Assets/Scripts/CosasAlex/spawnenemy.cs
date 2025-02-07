using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class spawnenemy : MonoBehaviour
{
    public List<GameObject> enemigos = new List<GameObject>();
    public List<GameObject> posiciones = new List<GameObject>();
    public GameObject puerta;
    public GameObject colliderPuerta;
    public int Total_enemies;
    bool sala_dificil=false;
    public static int enemigos_derrotados;
    public static int oleadas=1;
    public bool salaTerminada;
    int positionReset;
    public GameObject lights;
    [SerializeField] Material open, closed;
    bool dontSpam;

    public Transform posicionDrop;

    public GameObject[] drops;

    private void Start()
    {
        oleadas = 1;

        lights.GetComponent<Renderer>().material = closed;
        colliderPuerta.SetActive(false);
        enemigos_derrotados = 0;
        for (int i = 0; i < selectorsalas.Hard_rooms.Count; i++)//Identifica si la sala es una sala difícil
        {
            if (GM.salas_recorridas == selectorsalas.Hard_rooms[i]+1)
            {
                sala_dificil = true;
                Total_enemies = Total_enemies + 3;
                
            }
        }

        List<int> Positions = new List<int>();
        for (int i = 0; i < posiciones.Count; i++)
        {
            Positions.Add(i);//Creamos las posiciones
        }
        

        for (int i = 0; i < Total_enemies; i++)
        {
            int randPattern = Random.Range(0, Positions.Count);
            positionReset = Positions[randPattern];
            Positions.Remove(positionReset);

            int random = Random.Range(0, enemigos.Count-1);
            GameObject enemy = Instantiate(enemigos[random], posiciones[positionReset].transform.position, Quaternion.identity);
            enemy.SetActive(true);//si hacemos prefabs cambiar

            if (Positions.Count <= 0)
            {
                Positions.Clear();
                for (int w = 0; w < posiciones.Count; w++)
                {
                    Positions.Add(w);//Creamos las posiciones
                }
            }

        }
        if (Random.Range(0, 3) >= 2)
        {
            int randPattern = Random.Range(0, Positions.Count);
            positionReset = Positions[randPattern];
            Positions.Remove(positionReset);
            GameObject _lloron = Instantiate(enemigos[enemigos.Count-1], posiciones[positionReset].transform.position, Quaternion.identity);

            if (Positions.Count <= 0)
            {
                Positions.Clear();
                for (int w = 0; w < posiciones.Count; w++)
                {
                    Positions.Add(w);//Creamos las posiciones
                }
            }

        }
        Positions.Clear();
    }
    void Update()
    {
        if (SceneManager.GetSceneByName("boss").name == "boss"&&enemigos_derrotados==1)//Cambiar para futuro 
        {
            puerta.SetActive(false);
        }
        if (enemigos_derrotados == Total_enemies)
        {
            List<int> Positions = new List<int>();
            for (int i = 0; i < posiciones.Count; i++)
            {
                Positions.Add(i);//Creamos las posiciones
            }
            
            oleadas++;
            enemigos_derrotados++;//Hacer que no vuelva a acceder al bucle <---ESTO SERIA EL MAS UNO DE ABAJO
            for (int i = 0; i < Total_enemies ; i++)
            {
                int randPattern = Random.Range(0, Positions.Count);
                positionReset = Positions[randPattern];
                Positions.Remove(positionReset);
                int random = Random.Range(0,enemigos.Count-1);
               GameObject enemy = Instantiate(enemigos[random], posiciones[positionReset].transform.position, Quaternion.identity);
               enemy.SetActive(true);//si hacemos prefabs cambiar


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
        if (enemigos_derrotados == (Total_enemies*oleadas)+1&&!dontSpam)//+1 SERIA EL NUMERO TOTAL DE OLEADAS. A CAMBIAR
        {
            dontSpam = true;
            salaTerminada = true;
            colliderPuerta.SetActive(true);
            lights.GetComponent<Renderer>().material = open;
            //puerta.SetActive(false);

            LootFinal();

        }
    }

    void LootFinal()
    {
        int i = Random.Range(0, 6);

        if (i <= 3)
        {

            for (int j = 0; i < 5; i++)
            {
                Instantiate(drops[0], posicionDrop.position, posicionDrop.rotation);
            }

                       
        }
        else
        {
            int x = Random.Range(0, 4);

            switch (x)
            {
                case 0:
                    Instantiate(drops[1], posicionDrop.position, posicionDrop.rotation);
                    break;
                case 1:
                    Instantiate(drops[2], posicionDrop.position, posicionDrop.rotation);
                    break;
                case 2:
                    Instantiate(drops[3], posicionDrop.position, posicionDrop.rotation);
                    break;
                case 3:
                    Instantiate(drops[4], posicionDrop.position, posicionDrop.rotation);
                    break;
            }
        }
    }
}
