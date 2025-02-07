using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    public static int RUN=1;
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(SceneManager.GetActiveScene().name == "Base")
            {
                if (PlayerGM.armas == 1 || PlayerGM.armas == 2 || PlayerGM.armas == 3)
                {
                    RUN = 1;
                    SceneLoader.Instance.LoadScene("SampleScene");
                }
                

            }
            else
            {
                GM.salas_recorridas++;
                if (GM.salas_recorridas <= GM.salas_ordenadas.Count)
                {
                    SceneManager.LoadScene(GM.salas_ordenadas[GM.salas_recorridas - 1]);
                }
                if (GM.salas_recorridas > GM.salas_ordenadas.Count)
                {
                    if (RUN == 2)
                    {
                        SceneManager.LoadScene("Base");
                    }
                    if (RUN == 1)
                    {
                        RUN = 2;
                        SceneManager.LoadScene("SampleScene");
                        SceneLoader.Instance.LoadScene("SampleScene");
                    }
                }

            }
        }
    }
    

}
