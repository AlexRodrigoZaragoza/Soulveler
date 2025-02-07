using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeveloperMode : MonoBehaviour
{
    private static DeveloperMode instance;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            GM.salas_recorridas++;
            if (GM.salas_recorridas <= GM.salas_ordenadas.Count)
            {
                SceneManager.LoadScene(GM.salas_ordenadas[GM.salas_recorridas - 1]);
            }
            if (GM.salas_recorridas > GM.salas_ordenadas.Count)
            {
                if (Select.RUN == 2)
                {
                    SceneManager.LoadScene("Base");
                }
                if (Select.RUN == 1)
                {
                    Select.RUN = 2;
                    SceneManager.LoadScene("SampleScene");
                }
            }

        }
        //Volver al Lobby
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Base");
        }
        //Reload scene
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
